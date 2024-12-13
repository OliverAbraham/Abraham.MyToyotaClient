using System.Runtime.Serialization;

namespace MyToyotaClient.Exceptions
{
    [Serializable]
    internal class ToyotaLoginError : Exception
    {
        public ToyotaLoginError()
        {
        }

        public ToyotaLoginError(string? message) : base(message)
        {
        }

        public ToyotaLoginError(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ToyotaLoginError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}