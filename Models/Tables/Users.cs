using System;
using System.Collections.Generic;

namespace csproject.Models;

public partial class Users
{
    /// <summary>
    /// 索引
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 合法性
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 登入帳號
    /// </summary>
    public string? UserNo { get; set; }

    /// <summary>
    /// 登入名稱
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 登入密碼
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 身分代號
    /// </summary>
    public string? RoleNo { get; set; }

    /// <summary>
    /// 性別代號
    /// </summary>
    public string? GenderNo { get; set; }

    /// <summary>
    /// 班級代號
    /// </summary>
    public string? DeptNo { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 起訓日期
    /// </summary>
    public DateTime? OnboardDate { get; set; }

    /// <summary>
    /// 結訓日期
    /// </summary>
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 電子信箱
    /// </summary>
    public string? ContactEmail { get; set; }

    /// <summary>
    /// 行動電話
    /// </summary>
    public string? ContactTel { get; set; }

    /// <summary>
    /// 驗證碼
    /// </summary>
    public string? ValidateCode { get; set; }

    /// <summary>
    /// 通知Token
    /// </summary>
    public string? NotifyPassword { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    public string? Remark { get; set; }
}
