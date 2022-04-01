using TipoCambio.Api.Core.Models;
using System;

namespace TipoCambio.Api.Core.Utility.Transfer.Response
{
    public class Response : IDisposable
    {
        bool ldisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!ldisposed)
            {
                if (disposing)
                    Status = null;
            }

            ldisposed = true;
        }

        ~Response()
        {
            Dispose(false);
        }

        public Response(Status status)
        {
            Status = status;
        }

        public Response(Exception ex) : this(new Status(ex))
        {
        }

        public Response(bool ok) : this(new Status(ok))
        {
        }

        public Response() : this(true)
        {
        }

        public Status Status { set; get; }
    }
}