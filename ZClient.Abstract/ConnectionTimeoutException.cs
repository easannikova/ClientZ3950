using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// Exception thrown when server connnection search or scan times out  
    /// </summary>
    public class ConnectionTimeoutException : Exception
    {
        public ConnectionTimeoutException(string message) : base(message)
        {
        }		
    }
}
