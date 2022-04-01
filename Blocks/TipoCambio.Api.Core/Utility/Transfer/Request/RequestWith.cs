namespace TipoCambio.Api.Core.Utility.Transfer.Request
{
    public class RequestWith<TParameters> : Request
    {
        bool ldisposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!ldisposed)
            {
                if (disposing)
                {
                    Parameters = default;
                    base.Dispose(disposing);
                }
            }

            ldisposed = true;
        }

        ~RequestWith()
        {
            Dispose(false);
        }

        public TParameters Parameters { set; get; }
    }
}