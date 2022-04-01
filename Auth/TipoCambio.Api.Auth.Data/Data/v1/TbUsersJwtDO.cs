using System;
using System.Linq;
using TipoCambio.Api.Auth.Data.Code.Helpers;
using TipoCambio.Api.Auth.Entity.Entities.Models;
using TipoCambio.Api.Auth.Entity.Entities.v1;

namespace TipoCambio.Api.Auth.Data.Data.v1
{
    public class TbUsersJwtDO:BaseData
	{
		protected string _schema;

        public TbUsersJwtDO(ServiceConfig environment)
        {
            _schema = "Authorization";

            Connect(environment);
        }

		public TbUsersJwt Insert(TbUsersJwt objInput)
		{
			TbUsersJwt lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbUsersJwt_ins";
                var lparams = new object[] { objInput.StCodUserJwt, objInput.StUserName, objInput.StPassword, objInput.StSalt, objInput.BlIsActive, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
		}

		public TbUsersJwt Update(TbUsersJwt objInput)
		{
            TbUsersJwt lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbUsersJwt_upd";
                var lparams = new object[] { objInput.StCodUserJwt, objInput.StUserName, objInput.StPassword, objInput.StSalt, objInput.BlIsActive, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0 ? SelectBy(objInput) : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
		}

		public bool Delete(TbUsersJwt objInput)
		{
			bool lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbUsersJwt_del";
                var lparams = new object[] { objInput.StCodUserJwt, null };

                SqlUtils.ExecuteNonQueryByProcedure(_connection, lprocedure, lparams);

                lrespuesta = (int)lparams.LastOrDefault() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
		}

		public TbUsersJwt[] Select()
		{
			TbUsersJwt[] lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbUsersJwt_sel";

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure);

                lrespuesta = lreader.HasRows ? lreader.HydrateRows<TbUsersJwt>().ToArray() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
		}

		public TbUsersJwt SelectBy(TbUsersJwt objInput)
		{
			TbUsersJwt lrespuesta;

            try
            {
                var lprocedure = $"{_schema}.sp_TbUsersJwt_selby";
                var lparams = new object[] { objInput.StCodUserJwt };

                var lreader = SqlUtils.ExecuteReaderByProcedure(_connection, lprocedure, lparams);

                lrespuesta = lreader.HasRows ? lreader.HydrateFields<TbUsersJwt>() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
		}
	}
}