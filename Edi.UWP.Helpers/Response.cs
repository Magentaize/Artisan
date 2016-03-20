using System;
using System.Text;

namespace Edi.UWP.Helpers
{
    public class Response
    {
        /// <summary>
        /// Whether the operation is success
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// If response blow up, should give the exception
        /// </summary>
        public Exception Exception { get; set; }

        public Response()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// Reponse type of object
        /// </summary>
        public T Item { get; set; }
    }

    public static class ResponseExtension
    {
        public static string GetExceptionDetailMessages(this Response r)
        {
            var sb = new StringBuilder();
            var ex = r.Exception;

            if (null != ex)
            {
                sb.Append(ex.Message);
                while (null != ex.InnerException)
                {
                    ex = ex.InnerException;
                    sb.Append(" | " + ex.Message);
                }
            }

            string message = sb.ToString();
            return message;
        }
    }
}
