using System;
using System.Collections.Generic;

namespace csproject.Models;

public partial class Departments
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 班級代號
    /// </summary>
    public string? DeptNo { get; set; }

    /// <summary>
    /// 班級名稱
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
