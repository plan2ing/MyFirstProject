using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Models
{
    [ModelMetadataType(typeof(z_metaDepartments))]
    public partial class Departments
    {
    }
}

public class z_metaDepartments
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "班級代號")]
    [Required(ErrorMessage = "班級代號不可空白!!")]
    public string? DeptNo { get; set; }
    [Display(Name = "班級名稱")]
    [Required(ErrorMessage = "班級名稱不可空白!!")]
    public string? DeptName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}