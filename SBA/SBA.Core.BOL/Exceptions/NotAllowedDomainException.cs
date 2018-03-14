using System;

namespace SBA.Core.BOL.Exceptions
{
    public class NotAllowedDomainException : Exception
    {
        public NotAllowedDomainException(string bannedIpAddress) 
            : base($"SocketSocketThread recived data from banned domain. [{bannedIpAddress}]")
        { }
    }
}