using System;

namespace Artisan.Toolkit.Exceptions
{
    /// <summary>
    /// 工具集内部错误，一般来说是不会直接出现此异常，但是在极少情况下，可能仍然会出现此异常。
    /// </summary>
    public class ToolkitInternalException : Exception
    {
        /// <summary>
        /// 初始化<see cref="ToolkitInternalException"/>实例。
        /// </summary>
        public ToolkitInternalException()
        {
        }

        /// <summary>
        /// 用指定的错误消息初始化<see cref="ToolkitInternalException"/>实例。
        /// </summary>
        /// <param name="message"></param>
        public ToolkitInternalException(String message) : base(message)
        {
        }

        /// <summary>
        /// 用指定的错误消息和导致此异常的内部异常初始化<see cref="ToolkitInternalException"/>实例。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ToolkitInternalException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}