using System;

namespace ZClient.Abstract
{
    /// <summary>     
    /// Packages, of type 'itemorder', 'update', or of one of the
    /// extention types 'create', 'drop', 'commit'.  
    /// </summary>
    /// <remarks>
    /// These packages can be configures through setting appropriate 
    /// Zoom.Net::IPackageOptionsCollection Options
    /// </remarks>
    public interface IPackage : IDisposable
    {
        IPackageOptionsCollection Options { get; }

        /// <summary>
        /// Sending Package off to server for processing.
        /// </summary>
        void Send();
    }
}
