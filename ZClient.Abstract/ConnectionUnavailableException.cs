namespace ZClient.Abstract
{
    /// <summary>
    /// Exception thrown when server does not accept a live connection
    /// </summary>
    public class ConnectionUnavailableException : ZoomImplementationException
    {
        public ConnectionUnavailableException(string message) : base(message)
        {
        }	
    }
}
