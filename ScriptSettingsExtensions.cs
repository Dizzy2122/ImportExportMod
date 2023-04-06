// ScriptSettingsExtensions.cs
using GTA;
using System;

namespace ImportExportModNamespace
{
    public static class ScriptSettingsExtensions
    {
        public static bool ContainsSetting(this ScriptSettings settings, string section, string name)
        {
            try
            {
                string value = settings.GetValue<string>(section, name, null);
                return value != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
