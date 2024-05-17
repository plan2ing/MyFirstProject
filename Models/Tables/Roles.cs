using System;
using System.Collections.Generic;

namespace csproject.Models;

public partial class Roles
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 身分代號
    /// </summary>
    public string? RoleNo { get; set; }

    /// <summary>
    /// 身分名稱
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
