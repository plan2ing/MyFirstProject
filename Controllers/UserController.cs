using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 登出系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            using var user = new z_sqlUsers();
            user.Logout();
            return RedirectToAction("Login", "User", new { area = "" });
        }

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            ActionService.SetActionData("登入", "", "使用者");
            vmLogin model = new vmLogin();
            return View(model);
        }

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(vmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();
            if (!user.CheckLogin(model))
            {
                ModelState.AddModelError("UserNo", "登入帳號或密碼輸入不正確!!");
                model.UserNo = "";
                model.Password = "";
                return View(model);
            }

            //判斷使用者角色，進入不同的首頁
            var data = user.GetData(model.UserNo);
            if (data.RoleNo == "Mis" || data.RoleNo == "Vendor" || data.RoleNo == "Demo")
                return RedirectToAction("Index", "Home", new { area = "Admin" });    // 後台
            if (data.RoleNo == "User" || data.RoleNo == "Other")
                return RedirectToAction("Index", "Home", new { area = "" });   // 前台

            //角色不正確,引發自定義錯誤,並重新輸入
            ModelState.AddModelError("UserNo", "登入帳號角色設定不正確!!");
            model.UserNo = "";
            model.Password = "";
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous()]
        // 訪客
        public IActionResult Register()
        {
            ActionService.SetActionData("註冊", "", "使用者");
            vmRegister model = new vmRegister();
            model.RoleNo = "使用者";
            model.GenderNo = "M";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Register(vmRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            //檢查登入帳號及電子信箱是否有重覆
            using var user = new z_sqlUsers();
            if (!user.CheckRegisterUserNo(model.UserNo))
            {
                ModelState.AddModelError("UserNo", "登入帳號重覆註冊!!");
                return View(model);
            }
            if (!user.CheckRegisterEmail(model.Email))
            {
                ModelState.AddModelError("Email", "電子信箱重覆註冊!!");
                return View(model);
            }
            //新增未審核的使用者記錄
            model.RoleNo = "User";
            string str_code = user.RegisterNewUser(model);

            //寄出驗證信
            using var sendEmail = new SendMailService();
            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/User/RegisterValidate/{str_code}";

                str_message = sendEmail.UserRegister(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您的註冊資訊已建立，請記得收信完成驗證流程!!";
                }
            }
            //顯示註冊訊息
            TempData["MessageText"] = str_message;
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult RegisterValidate(string id)
        {
            using var user = new z_sqlUsers();
            TempData["MessageText"] = user.RegisterConfirm(id);
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult MessageResult()
        {
            ViewBag.MessageText = (TempData["MessageText"] == null) ? "" : TempData["MessageText"].ToString();
            return View();
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Forget()
        {
            ActionService.SetActionData("忘記密碼", "", "使用者");
            vmForget model = new vmForget();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Forget(vmForget model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();

            //2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
            string str_code = user.Forget(model.UserNo);
            if (string.IsNullOrEmpty(str_code))
            {
                ModelState.AddModelError("UserNo", "查無帳號或電子信箱資訊!!");
                return View(model);
            }

            //3.寄出忘記密碼驗證信
            using var sendEmail = new SendMailService();
            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.Password = userData.Password;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/User/ForgetValidate/{str_code}";

                str_message = sendEmail.UserForget(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                }
            }

            //顯示註冊訊息
            TempData["MessageText"] = str_message;
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult ForgetValidate(string id)
        {
            using var user = new z_sqlUsers();
            //更新使用者狀態為已審核
            string str_message = user.ForgetConfirm(id);
            //顯示重設密碼訊息
            TempData["MessageText"] = str_message;
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [Login()]   // 設定權限
        public IActionResult ResetPassword()
        {
            ActionService.SetActionData("重設密碼", "", "使用者");
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public IActionResult ResetPassword(vmResetPassword model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();

            //2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
            string str_code = user.ResetPassword(model);
            if (string.IsNullOrEmpty(str_code))
            {
                ModelState.AddModelError("UserNo", "目前密碼不正確!!");
                return View(model);
            }

            //3.寄出忘記密碼驗證信
            using var sendEmail = new SendMailService();

            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.Password = userData.Password;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/User/ResetPasswordValidate/{str_code}";

                str_message = sendEmail.UserResetPassword(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                }
            }

            //3.登出使用者
            SessionService.IsLogin = false;
            SessionService.UserNo = "";
            SessionService.UserName = "";

            //顯示註冊訊息
            TempData["MessageText"] = str_message;
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public ActionResult ResetPasswordValidate(string id)
        {
            using var user = new z_sqlUsers();
            //更新使用者狀態為已審核
            string str_message = user.ResetPasswordConfirm(id);
            //顯示重設密碼訊息
            TempData["MessageText"] = str_message;
            return RedirectToAction("MessageResult", "User", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public ActionResult Profile()
        {
            ActionService.SetActionData("我的帳號", "", "使用者");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        [Login()]
        public ActionResult PhotoUpload()
        {
            ActionService.SetActionData("上傳照片", "", "我的帳號");
            return View();
        }

        [HttpPost]
        [Login()]
        public ActionResult PhotoUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 取得目前專案資料夾目錄路徑
                string FileNameOnServer = Directory.GetCurrentDirectory();
                // 專案目錄路徑
                string WebFolder = Path.Combine(FileNameOnServer , "wwwroot\\images\\users");
                // 存入的檔案名稱, 以使用者名稱.jpg 存入
                string FileName = $"{SessionService.UserNo}.jpg";
                // 專案路徑加入要存入的資料夾路徑
                string FilePathName = Path.Combine(WebFolder, FileName);
                try
                {
                    // 刪除已存在檔案
                    if (System.IO.File.Exists(FilePathName)) System.IO.File.Delete(FilePathName);
                }
                catch (Exception ex)
                {
                    // 無法刪除時顯示錯誤訊息
                    TempData["MessageText"] = ex.Message;
                    return RedirectToAction("MessageResult", "User", new { area = "" });
                }

                // 建立一個串流物件
                using var stream = System.IO.File.Create(FilePathName);
                // 將檔案寫入到此串流物件中
                file.CopyTo(stream);
            }
            return RedirectToAction("Profile", "User", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public ActionResult EditProfile()
        {
            ActionService.SetActionData("修改個人資料", "", "我的帳號");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public ActionResult EditProfile(Users model)
        {
            ModelState.Remove("LeaveDate");
            ModelState.Remove("DeptNo");
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();
            user.UpdateUserProfile(model);
            return RedirectToAction("Profile", "User", new { area = "" });
        }
    }
}