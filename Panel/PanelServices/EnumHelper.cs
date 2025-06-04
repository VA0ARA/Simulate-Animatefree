using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Panel.PanelServices;

// در پوشه Helpers یا هر پوشه دلخواه شما
public static class EnumHelper
{
    public static string GetEnumDisplayName(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DisplayAttribute>();
        return attribute?.Name ?? value.ToString();
    }
}
