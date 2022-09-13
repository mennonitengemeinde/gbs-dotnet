using System.ComponentModel;

namespace gbs.Shared.Enums;

public enum AlertType
{
    [Description("alert-error")]
    Error,
    [Description("alert-warning")]
    Warning,
    [Description("alert-info")]
    Info,
    [Description("alert-success")]
    Success
}