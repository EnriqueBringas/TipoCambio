using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TipoCambio.Api.Auth.Data.Code.Helpers
{
    public static class SqlUtils
    {
        public static SqlDataReader ExecuteReaderByProcedure(SqlConnection connection, string procedure, params object[] parametersValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure");

            if (parametersValues != null && parametersValues.Any())
            {
                var lparameters = GetParametersByProcedure(connection, procedure);

                AssignParametersToValues(lparameters, parametersValues);

                return ExecuteReader(connection, procedure, lparameters);
            }
            else
                return ExecuteReader(connection, procedure);
        }

        public static int ExecuteNonQueryByProcedure(SqlConnection connection, string procedure, params object[] parametersValues)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure");

            if (parametersValues != null && parametersValues.Any())
            {
                var lparameters = GetParametersByProcedure(connection, procedure);

                AssignParametersToValues(lparameters, parametersValues);

                var lrowsaffected = ExecuteNonQuery(connection, procedure, lparameters);

                for (int i = 0; i < lparameters.Length; i++)
                {
                    if (lparameters[i].Direction == ParameterDirection.InputOutput)
                        parametersValues[i] = lparameters[i].Value;
                }

                return lrowsaffected;
            }
            else
                return ExecuteNonQuery(connection, procedure);
        }

        private static SqlParameter[] GetParametersByProcedure(SqlConnection connection, string procedure)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure");

            SqlParameter[] lparameters;

            var lfactory = SqlClientFactory.Instance;
            var lcmdbuilder = lfactory.CreateCommandBuilder();

            using (var lcommand = new SqlCommand(procedure, connection))
            {
                lcommand.CommandType = CommandType.StoredProcedure;
                lcommand.CommandTimeout = 60;

                if (!connection.State.Equals(ConnectionState.Open))
                    connection.Open();
                else
                {
                    connection.Close();
                    connection.Open();
                }

                var lmethod = lcmdbuilder.GetType().GetMethod("DeriveParameters", BindingFlags.Public | BindingFlags.Static);

                if (lmethod != null)
                {
                    try
                    {
                        lmethod.Invoke(null, new object[] { lcommand });
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                connection.Close();

                lcommand.Parameters.RemoveAt(0);

                lparameters = new SqlParameter[lcommand.Parameters.Count];

                lcommand.Parameters.CopyTo(lparameters, 0);
            }

            foreach (SqlParameter lparameter in lparameters)
            {
                lparameter.Value = DBNull.Value;
            }

            return CloneParameters(lparameters);
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            var lparameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                lparameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return lparameters;
        }

        private static void AssignParametersToValues(SqlParameter[] parameters, object[] values)
        {
            if (parameters == null || values == null)
                return;

            if (parameters.Length != values.Length)
                throw new ArgumentException("La cantidad parametros del procedimiento no coincide con el números de campos de entrada enviados.");

            for (int i = 0, j = parameters.Length; i < j; i++)
            {
                if (values[i] == null)
                    parameters[i].Value = DBNull.Value;
                else
                    parameters[i].Value = values[i];
            }
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] parameters)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                            p.Value = DBNull.Value;

                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, string procedure, SqlParameter[] parameters)
        {
            if (!connection.State.Equals(ConnectionState.Open))
                connection.Open();

            command.Connection = connection;
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 60;

            if (parameters != null && parameters.Any())
                AttachParameters(command, parameters);

            return;
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, string procedure, SqlParameter[] parameters = null)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure");

            SqlDataReader lrespuesta;

            try
            {
                var lcommand = new SqlCommand();

                PrepareCommand(lcommand, connection, procedure, parameters);

                lrespuesta = lcommand.ExecuteReader();

                bool canClear = true;

                foreach (SqlParameter commandParameter in lcommand.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                    lcommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lrespuesta;
        }

        private static int ExecuteNonQuery(SqlConnection connection, string procedure, SqlParameter[] parameters = null)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");

            if (string.IsNullOrEmpty(procedure))
                throw new ArgumentNullException("procedure");

            int lrespuesta;

            try
            {
                var lcommand = new SqlCommand();

                PrepareCommand(lcommand, connection, procedure, parameters);

                lrespuesta = lcommand.ExecuteNonQuery();

                lcommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return lrespuesta;
        }
    }
}