using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Infrastructure.Logging
{
    public static partial class Logging
    {
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Warning,
            Message = "Update is null. {locationInfo}")]
        static partial void LogUpdateIsNull(ILogger logger,string locationInfo);
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Warning,
            Message = "User is null. updateId: {updateId} {locationInfo}")]
        static partial void LogUserIsNull(ILogger logger, int updateId, string locationInfo);
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Information,
            Message = "Method don't exist. updateId: {updateId}")]
        static partial void LogMethodNotExist(ILogger logger, int updateId);

        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Message text don't exist. updateId: {updateId}")]
        static partial void LogMessageTextNotExist(ILogger logger, int updateId);

        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Entities cound < 1. update_id: {updateId} {locationInfo}")]
        static partial void LogNotEntities(ILogger logger, int updateId, string locationInfo);
        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Parse int exection: {updateId} {locationInfo}")]
        static partial void LogIntParseExection(ILogger logger, int updateId, string locationInfo);
        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Message is null. updateId: {updateId} {locationInfo}")]
        static partial void LogMessageIsNull(ILogger logger, int updateId, string locationInfo);
        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Chat member is null. updateId: {updateId} {locationInfo}")]
        static partial void LogChatMemberIsNull(ILogger logger, int updateId, string locationInfo);
        
        
        
        
        public static void MethodNotExist(this ILogger logger, int updateId)
        {
            LogMethodNotExist(logger, updateId);
        }

        public static void MessageTextNotExist(this ILogger logger, int updateId)
        {
            LogMessageTextNotExist(logger, updateId);
        }

        public static void NotEntities(
            this ILogger logger, int updateId, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogNotEntities(logger, updateId, locationInfo);  
        }

        public static void IntParseExection(
            this ILogger logger, int updateId,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogIntParseExection(logger, updateId, locationInfo);
        }
        public static void UpdateIsNull(
            this ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogUpdateIsNull(logger, locationInfo);
        }
        public static void MessageIsNull(
            this ILogger logger, int updateId,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogMessageIsNull(logger, updateId, locationInfo);
        }
        public static void UserIsNull(
            this ILogger logger, int updateId,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogUserIsNull(logger, updateId, locationInfo);
        }
        public static void ChatMemberIsNull(
            this ILogger logger, int updateId,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogChatMemberIsNull(logger, updateId, locationInfo);
        }
        
    }
}
