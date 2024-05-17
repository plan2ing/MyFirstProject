using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Models
{
    [ModelMetadataType(typeof(z_metaGenders))]
    public partial class Genders
    {
    }
}

public class z_metaGenders
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "性別代號")]
    [Required(ErrorMessage = "性別代號不可空白!!")]
    public string? GenderNo { get; set; }
    [Display(Name = "性別名稱")]
    [Required(ErrorMessage = "性別名稱不可空白!!")]
    public string? GenderName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}