using TipoCambio.Api.Core.Utility.Helpers;
using System;
using System.Linq;

namespace TipoCambio.Api.Core.Models
{
    public sealed class Status : IDisposable
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
                    Messages = null;
                    Tracking = null;
                }
            }

            ldisposed = true;
        }

        ~Status()
        {
            Dispose(false);
        }

        public Status(Exception ex) : this()
        {
            Ok = false;
            Messages = new[] { new Message(ex) };
        }

        public Status(bool ok) : this()
        {
            Ok = ok;
        }

        public Status()
        {
            Ok = false;
            Messages = null;
        }

        public bool Ok { set; get; }
        public Message[] Messages { set; get; }
        public string Tracking { set; get; }

        public void AddMessage(string value, bool isError = false)
        {
            var lmessages = Messages;

            Tools.AddToArray(ref lmessages, new Message(value, isError));

            Ok = !isError && Ok;
            Messages = lmessages;
        }

        public void AddMessage(string[] values, bool isError = false)
        {
            var lmessages = Messages;

            Tools.AddToArray(ref lmessages, values.Select(c => new Message(c, isError)).ToArray());

            Ok = !isError && Ok;
            Messages = lmessages;
        }
    }
}