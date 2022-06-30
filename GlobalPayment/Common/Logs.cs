using NLog;

namespace GlobalPayment.Tools
{
    public static class OjLog
    {
        private static readonly Logger ATLog = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Inicia una nueva instancia de un archivo de registro
        /// </summary>
        /// <param name="processName">Nombre del proceso</param>
        public static void Init(string processName)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $@"\\pathHere\{processName}\file.txt", };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
        }

        /// <summary>
        /// Ingresa una linea en el registro
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logMessage"></param>
        public static void WriteLine(LogLevel logLevel, string logMessage)
        {
            ATLog.Log(logLevel, logMessage);
        }

        /// <summary>
        /// Ingresa una linea en el registro
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logMessage"></param
        public static void WriteLine(LogLevel logLevel, Exception exception)
        {
            ATLog.Log(logLevel, exception);
        }
    }
}
