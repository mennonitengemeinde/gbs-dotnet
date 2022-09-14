using System.ComponentModel;

namespace gbs.Client.Enums.Button;

public enum BtnColor
{
    [Description("btn-primary")]
    Primary,
    [Description("btn-success")]
    Success,
    [Description("btn-error")]
    Error,
    [Description("btn-warning")]
    Warning,
    [Description("btn-info")]
    Info,
}