namespace ZClient.Abstract
{
    /// <summary>
    /// Exception thrown when invalid query has been send to server
    /// </summary>
    public class InvalidQueryException : ZoomImplementationException
    {
        public InvalidQueryException(string message) : base(message)
        {
        }
    }
}
