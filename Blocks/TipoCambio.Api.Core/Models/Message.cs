using System;

namespace TipoCambio.Api.Core.Models
{
    public sealed class Message : IDisposable
    {
        bool ldisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!ldisposed)
            {
                if (disposing)
                {
                    IsError = false;
                    Value = null;
                }
            }

            ldisposed = true;
        }

        ~Message()
        {
            Dispose(false);
        }

        public Message(Exception ex) : this(ex.Message, true)
        {
        }

        public Message(string value, bool isError = false)
        {
            IsError = isError;
            Value = value;
        }

        public bool IsError { get; set; }
        public string Value { set; get; }
    }
}