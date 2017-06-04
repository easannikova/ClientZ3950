using System;

namespace ZClient.Abstract
{
    /// <summary>
    /// Returning Z39.50 Bib1 fatal error codes as exeptions  
    /// </summary>
    public class Bib1Exception : Exception
    {
        public Bib1Exception(Bib1Diagnostic code, string message) 
             : base(message)
        {
            DiagnosticCode = code;
        }

        /// <summary>
        /// Getting the diagnostic code number of this exception
        /// </summary>
        public Bib1Diagnostic DiagnosticCode { get; }
    }
}
