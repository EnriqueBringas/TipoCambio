using System;
using System.Linq;
using TipoCambio.Api.Backend.Data.Code.Helpers;
using TipoCambio.Api.Backend.Entity.Entities.Models;
using TipoCambio.Api.Backend.Entity.Entities.v1;
using TipoCambio.Api.Backend.Entity.Models;

namespace TipoCambio.Api.Backend.Data.Data.v1
{
    public class TbExchangeRateDO : BaseData
    {
        protected string _schema;

        public TbExchangeRateDO(ServiceConfig environment)
        {
            _schema = "Exchange";

            Connect(environment);
        }

        public TbExchangeRate Insert(TbExchangeRate objInput)
        {
            TbExchangeRate lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_ins";
                var lparams = new object[] { objInput.StCodExchangeRate, objInput.StDate, objInput.StCodCurrencyOrigin, objInput.StCodCurrencyDestination, objInput.DcAmountBuy, objInput.DcAmountSale, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbExchangeRate Update(TbExchangeRate objInput)
        {
            TbExchangeRate lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_upd";
                var lparams = new object[] { objInput.StCodExchangeRate, objInput.StDate, objInput.StCodCurrencyOrigin, objInput.StCodCurrencyDestination, objInput.DcAmountBuy, objInput.DcAmountSale, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public bool Delete(TbExchangeRate objInput)
        {
            bool lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_del";
                var lparams = new object[] { objInput.StCodExchangeRate, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbExchangeRate[] Select()
        {
            TbExchangeRate[] lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_sel";

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure);

                lrespuesta = lreader.HasRows ? lreader.HydrateRows<TbExchangeRate>().ToArray() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbExchangeRate SelectBy(TbExchangeRate objInput)
        {
            TbExchangeRate lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_selby";
                var lparams = new object[] { objInput.StCodExchangeRate };

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure, lparams);

                lrespuesta = lreader.HasRows ? lreader.HydrateFields<TbExchangeRate>() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        public TbExchangeRate SelectByDate(ChangeMoneyRQ objInput)
        {
            TbExchangeRate lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbExchangeRate_selbyDate";
                var lparams = new object[] { objInput.StDate, objInput.StCodCurrencyOrigin, objInput.StCodCurrencyDestination };

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure, lparams);

                lrespuesta = lreader.HasRows ? lreader.HydrateFields<TbExchangeRate>() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }
    }
}