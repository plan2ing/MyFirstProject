using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace csproject.Models
{
    [ModelMetadataType(typeof(z_metaRoles))]
    public partial class Roles
    {
    }
}

public class z_metaRoles
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "身分代號")]
    [Required(ErrorMessage = "身分代號不可空白!!")]
    public string? RoleNo { get; set; }
    [Display(Name = "身分名稱")]
    [Required(ErrorMessage = "身分名稱不可空白!!")]
    public string? RoleName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}