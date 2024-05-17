using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Models
{
    [ModelMetadataType(typeof(z_metaMoney))]
    public partial class Money
    {
    }
}

public class z_metaMoney
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "登入帳號不可空白!!")]
    public string? UserNo { get; set; }
    [Display(Name = "早餐天數")]
    public int Breakfast { get; set; }
    [Display(Name = "午餐天數")]
    public int Lunch { get; set; }
    [Display(Name = "晚餐天數")]
    public int Dinner { get; set; }
    [Display(Name = "總金額")]
    public int Price { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}