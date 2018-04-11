namespace ZClient.Abstract.Exception
{
    /// <summary>
    /// Exception for specific Zoom implementation class errors
    /// </summary>
    public class ZoomImplementationException : System.Exception
    {
        public ZoomImplementationException(string message) : base(message)
        {
        }
    }
}