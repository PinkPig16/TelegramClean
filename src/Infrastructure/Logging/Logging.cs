using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public static partial class Logging
    {
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Information,
            Message = "methot don't exist. update_id: {update_id}")]
        static partial void LogMethotNotExist(ILogger logger, int update_id);

        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Message text don't exist. update_id: {update_id}")]
        static partial void LogMessageTextNotExist(ILogger logger, int update_id);

        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Entities cound < 1. update_id: {update_id} {locationInfo}")]
        static partial void LogNotEntities(ILogger logger, int update_id, string locationInfo);
        [LoggerMessage(
            EventId = 1,
            Level = LogLevel.Warning,
            Message = "Parse int exection: {update_id} {locationInfo}")]
        static partial void LogIntParseExection(ILogger logger, int update_id, string locationInfo);
        public static void MethotNotExist(this ILogger logger, int update_id)
        {
            LogMethotNotExist(logger, update_id);
        }

        public static void MessageTextNotExist(this ILogger logger, int update_id)
        {
            LogMessageTextNotExist(logger, update_id);
        }

        public static void NotEntities(
            this ILogger logger, int update_id, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogNotEntities(logger, update_id, locationInfo);  
        }

        public static void IntParseExection(
            this ILogger logger, int update_id,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            string locationInfo = $"Method: {memberName}, File: {filePath}, Line: {lineNumber}";
            LogIntParseExection(logger, update_id, locationInfo);
        }
    }
}
