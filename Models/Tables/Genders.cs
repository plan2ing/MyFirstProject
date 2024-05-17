using System;
using System.Collections.Generic;

namespace csproject.Models;

public partial class Genders
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 性別代號
    /// </summary>
    public string? GenderNo { get; set; }

    /// <summary>
    /// 性別名稱
    /// </summary>
    public string? GenderName { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
