namespace ZClient.Abstract.Exception
{
    /// <summary>
    /// Exception thrown when server connnection search or scan times out  
    /// </summary>
    public class ConnectionTimeoutException : System.Exception
    {
        public ConnectionTimeoutException(string message) : base(message)
        {
        }		
    }
}
