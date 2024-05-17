using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Models
{
    [ModelMetadataType(typeof(z_metaUsers))]
    public partial class Users
    {
        [NotMapped]
        [Display(Name = "早餐天數")]
        public int? Breakfast { get; set; }
        [NotMapped]
        [Display(Name = "午餐天數")]
        public int? Lunch { get; set; }
        [NotMapped]
        [Display(Name = "晚餐天數")]
        public int? Dinner { get; set; }
        [NotMapped]
        [Display(Name = "總金額")]
        public int? Price { get; set; }
        [NotMapped]
        [Display(Name = "身分名稱")]
        public string? RoleName { get; set; }
        [NotMapped]
        [Display(Name = "性別名稱")]
        public string? GenderName { get; set; }
        [NotMapped]
        [Display(Name = "班級名稱")]
        public string? DeptName { get; set; }
    }
}

public class z_metaUsers
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "合法性")]
    public bool IsValid { get; set; }
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "登入帳號不可空白!!")]
    public string? UserNo { get; set; }
    [Display(Name = "登入名稱")]
    [Required(ErrorMessage = "登入名稱不可空白!!")]
    public string? UserName { get; set; }
    [Display(Name = "登入密碼")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Display(Name = "身分代號")]
    public string? RoleNo { get; set; }
    [Display(Name = "性別")]
    public string? GenderCode { get; set; }
    [Display(Name = "班級代號")]
    [Required(ErrorMessage = "班級代號不可空白!!")]
    public string? DeptNo { get; set; }
    [Display(Name = "出生日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? Birthday { get; set; }
    [Display(Name = "起訓日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? OnboardDate { get; set; }
    [Display(Name = "結訓日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? LeaveDate { get; set; }
    [Display(Name = "電子信箱")]
    [EmailAddress(ErrorMessage = "電子信箱格式不正確!!")]
    public string? ContactEmail { get; set; }
    [Display(Name = "行動電話")]
    public string? ContactTel { get; set; }
    [Display(Name = "驗證碼")]
    public string? ValidateCode { get; set; }
    [Display(Name = "通知Token")]
    public string? NotifyPassword { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}