namespace SBA.BOL.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ClearRecv(this string recvData) =>
            recvData.Replace("\0", string.Empty);
    }
}