using System;
using System.Linq;
using TipoCambio.Api.Backend.Data.Code.Helpers;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;

namespace TipoCambio.Api.Backend.Data.Data.v1
{
    public class TbCurrencyDO : BaseData
    {
        protected string _schema;

        public TbCurrencyDO(ServiceConfig environment)
        {
            _schema = "Exchange";

            Connect(environment);
        }

        public TbCurrency Insert(TbCurrency objInput)
        {
            TbCurrency lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbCurrency_ins";
                var lparams = new object[] { objInput.StCodCurrency, objInput.StDescription, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbCurrency Update(TbCurrency objInput)
        {
            TbCurrency lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbCurrency_upd";
                var lparams = new object[] { objInput.StCodCurrency, objInput.StDescription, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public bool Delete(TbCurrency objInput)
        {
            bool lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbCurrency_del";
                var lparams = new object[] { objInput.StCodCurrency, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbCurrency[] Select()
        {
            TbCurrency[] lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbCurrency_sel";

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure);

                lrespuesta = lreader.HasRows ? lreader.HydrateRows<TbCurrency>().ToArray() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbCurrency SelectBy(TbCurrency objInput)
        {
            TbCurrency lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbCurrency_selby";
                var lparams = new object[] { objInput.StCodCurrency };

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure, lparams);

                lrespuesta = lreader.HasRows ? lreader.HydrateFields<TbCurrency>() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }
    }
}