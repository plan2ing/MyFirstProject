using System;
using System.Collections.Generic;

namespace csproject.Models;

public partial class Money
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 登入帳號
    /// </summary>
    public string? UserNo { get; set; }

    /// <summary>
    /// 早餐天數
    /// </summary>
    public int Breakfast { get; set; }

    /// <summary>
    /// 午餐天數
    /// </summary>
    public int Lunch { get; set; }

    /// <summary>
    /// 晚餐天數
    /// </summary>
    public int Dinner { get; set; }

    /// <summary>
    /// 總金額
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
