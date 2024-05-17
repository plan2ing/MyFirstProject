using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace csproject.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly dbEntities db;
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UserController(IConfiguration configuration, dbEntities entities)
        {
            db = entities;
            Configuration = configuration;
        }

        /// <summary>
        /// 客戶資料初始事件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Area("Admin")]
        public IActionResult Init()
        {
            // 初始化
            SessionService.SearchText = "";
            SessionService.SortColumn = "";
            SessionService.SortDirection = "asc";
            //返回客戶列表
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        /// <summary>
        /// 員工資料列表
        /// </summary>
        /// <param name="page">目前頁數</param>
        /// <param name="pageSize">每頁筆數</param>
        /// <returns></returns>
        [HttpGet]
        [Area("Admin")]
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            //取得員工資料列表集合
            using var sqlU = new z_sqlUsers();
            var model = sqlU.GetDataList(SessionService.SearchText).ToPagedList(page, pageSize);
            ViewBag.PageInfo = $"第 {page} 頁，共 {model.PageCount}頁";
            ViewBag.SearchText = SessionService.SearchText;
            return View(model);
        }

        /// <summary>
        /// 員工資料新增或修改輸入 (id = 0 為新增 , id > 0 為修改)
        /// </summary>
        /// <param name="id">要修改的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Area("Admin")]
        public IActionResult CreateEdit(int id = 0)
        {
            //取得新增或修改的員工資料結構及資料
            using var sqlU = new z_sqlUsers();
            var model = sqlU.GetData(id);
            return View(model);
        }

        /// <summary>
        /// 員工資料新增或修改存檔
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [Area("Admin")]
        public IActionResult CreateEdit(Users model)
        {
            //檢查是否有違反 Metadata 中的 Validation 驗證
            if (!ModelState.IsValid) return View(model);
            //檢查是否重覆輸入登入(UserNo)值
            using var dpr = new DapperRepository();
            if (dpr.IsDuplicated(model, "UserNO"))
            {
                ModelState.AddModelError("UserNO", "登入帳號重覆!!");
                return View(model);
            }
            //執行新增或修改資料
            using var sqlU = new z_sqlUsers();
            sqlU.CreateEdit(model, model.Id);
            //返回員工資料列表
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        /// <summary>
        /// 員工資料刪除
        /// </summary>
        /// <param name="id">要刪除的Key值</param>
        /// <returns></returns>
        [HttpGet]
        [Login()]
        [Area("Admin")]
        public IActionResult Delete(int id = 0)
        {
            //執行刪除資料
            using var sqlU = new z_sqlUsers();
            sqlU.Delete(id);
            //返回員工資料列表
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Area("Admin")]
        public IActionResult Search()
        {
            object obj_text = Request.Form["SearchText"];
            SessionService.SearchText = (obj_text == null) ? string.Empty : obj_text.ToString();
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        /// <summary>
        /// 欄位排序
        /// </summary>
        /// <param name="id">指定排序的欄位</param>
        /// <returns></returns>
        [HttpGet]
        [Area("Admin")]
        public IActionResult Sort(string id)
        {
            if (SessionService.SortColumn == id)    // 如果上一次跟這一次是按同一個欄位
            {
                SessionService.SortDirection = (SessionService.SortDirection == "asc") ? "desc" : "asc";
            }
            else
            {
                SessionService.SortColumn = id;     // 如果上一次跟這一次是不同欄位就改變欄位
                SessionService.SortDirection = "asc";
            }
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }
    }
}