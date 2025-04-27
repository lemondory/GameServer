public static class LoggerHelper
{
    public static void LOGI(string message) => Serilog.Log.Information(message);
    public static void LOGW(string message) => Serilog.Log.Warning(message);
    public static void LOGE(string message, Exception? ex = null)
    {
        if (ex != null) Serilog.Log.Error(ex, message);
        else Serilog.Log.Error(message);
    }
}