using System;

namespace TipoCambio.Api.Core.Utility.Transfer.Request
{
    public class Request : IDisposable
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
                {
                    Application = null;
                    Tracking = null;
                }
            }

            ldisposed = true;
        }

        ~Request()
        {
            Dispose(false);
        }

        public string Application { set; get; }
        public string Tracking { set; get; }
    }
}