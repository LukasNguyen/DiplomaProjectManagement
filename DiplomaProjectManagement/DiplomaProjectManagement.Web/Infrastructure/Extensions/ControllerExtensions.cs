using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiplomaProjectManagement.Web.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string PreparedMessageListKey = "PreparedMessageListKey";

        public static void PrepareInfoMessage(this Controller controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Info, message);
        }

        public static void PrepareSuccessMessage(this Controller controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Success, message);
        }

        public static void PrepareWarningMessage(this Controller controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Warning, message);
        }

        public static void PrepareWarningMessages(this Controller controller, IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                controller.PrepareWarningMessage(message);
            }
        }

        public static void PrepareErrorMessage(this Controller controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Error, message);
        }

        public static void PrepareErrorMessages(this Controller controller, IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                controller.PrepareErrorMessage(message);
            }
        }

        private static void PrepareMessage(this Controller controller, PreparedMessageType type, string message)
        {
            var preparedMessageList = controller.TempData[PreparedMessageListKey] as List<PreparedMessage>;

            if (preparedMessageList == null)
            {
                preparedMessageList = new List<PreparedMessage>();
            }

            preparedMessageList.Add(new PreparedMessage
            {
                Type = type,
                Message = message
            });

            controller.TempData[PreparedMessageListKey] = preparedMessageList;
        }

        public enum PreparedMessageType : byte
        {
            Info,
            Success,
            Warning,
            Error
        }

        [Serializable]
        public class PreparedMessage
        {
            public PreparedMessageType Type { get; set; }

            public string Message { get; set; }
        }
    }
}