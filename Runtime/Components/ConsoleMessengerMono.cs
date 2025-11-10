using UnityEngine;

namespace NaxtorGames.Utilities.Components
{
    /// <summary>
    /// A component to log messaged to the console. (Useful for unity events)
    /// </summary>
    [AddComponentMenu(Constants.ComponentMenu.Author.MISC + "Console Messegner")]
    public sealed class ConsoleMessengerMono : MonoBehaviour
    {
        private enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        [Header(Constants.Script.SETTINGS)]
        [Tooltip("When enabled, prepends the log message with a bold, colorized prefix indicating the log level.")]
        [SerializeField] private bool _showLogLevelPrefix = true;
        [Tooltip("The color used to highlight the 'Info' log level prefix in log messages.")]
        [SerializeField] private Color _infoColor = Color.green;
        [Tooltip("The color used to highlight the 'Warning' log level prefix in log messages.")]
        [SerializeField] private Color _warningColor = Color.yellow;
        [Tooltip("The color used to highlight the 'Error' log level prefix in log messages.")]
        [SerializeField] private Color _errorColor = Color.red;

        public void LogInfo(string message) => SendToConsole(LogLevel.Info, message);
        public void LogInfo(string message, Object context) => SendToConsole(LogLevel.Info, message, context);
        public void LogWarning(string message) => SendToConsole(LogLevel.Warning, message);
        public void LogWarning(string message, Object context) => SendToConsole(LogLevel.Warning, message, context);
        public void LogError(string message) => SendToConsole(LogLevel.Error, message);
        public void LogError(string message, Object context) => SendToConsole(LogLevel.Error, message, context);

        private string UpdateMessage(string message, LogLevel logLevel)
        {
            if (message.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            string prefix = string.Empty;
            if (_showLogLevelPrefix)
            {
                prefix = logLevel switch
                {
                    LogLevel.Info => "Info".Colorize(_infoColor),
                    LogLevel.Warning => "Warning".Colorize(_warningColor),
                    LogLevel.Error => "Error".Colorize(_errorColor),
                    _ => string.Empty,
                };

                prefix = $"<b>[{prefix}]</b> ";
            }

            return prefix + message;
        }

        private void SendToConsole(LogLevel logLevel, string message, Object context = null)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    Debug.Log(UpdateMessage(message, logLevel), context);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(UpdateMessage(message, logLevel), context);
                    break;
                case LogLevel.Error:
                    Debug.LogError(UpdateMessage(message, logLevel), context);
                    break;
                default:
                    return;
            }
        }
    }
}