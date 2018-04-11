namespace ZClient.Abstract.Exception
{
    /// <summary>
    /// Exception thrown when server refuses to accept Init Package
    /// </summary>
    public class InitRejectedException : ZoomImplementationException
    {
        public InitRejectedException(string message) : base(message)
        {
        }		
    }
}
