using TipoCambio.Api.Core.Models;
using System;

namespace TipoCambio.Api.Core.Utility.Transfer.Response
{
    public class ResponseWith<TResult> : Response
    {
        bool ldisposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!ldisposed)
            {
                if (disposing)
                {
                    Result = default;
                    base.Dispose(disposing);
                }
            }

            ldisposed = true;
        }

        ~ResponseWith()
        {
            Dispose(false);
        }

        public ResponseWith(Exception ex)
        {
            Status = new Status(ex);
            Result = default;
        }

        public ResponseWith()
        {
            Status = new Status(false);
            Result = default;
        }

        public TResult Result { set; get; }
    }
}