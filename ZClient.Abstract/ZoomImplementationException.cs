using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// Exception for specific Zoom implementation class errors
    /// </summary>
    public class ZoomImplementationException : Exception
    {
        public ZoomImplementationException(string message) : base(message)
        {
        }
    }
}