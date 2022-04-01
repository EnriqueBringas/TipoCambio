using Microsoft.Data.SqlClient;
using System.Data;
using TipoCambio.Api.Auth.Entity.Entities.Models;

namespace TipoCambio.Api.Auth.Data.Data
{
    public abstract class BaseData
    {
        protected SqlConnection _connection;

        protected void Connect(ServiceConfig environment)
        {
            _connection = new SqlConnection($"Server={environment.Server};Initial Catalog={environment.Database};Persist Security Info=False;User ID={environment.User};Password={environment.Password};MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");

            if (_connection.State.Equals(ConnectionState.Open))
                _connection.Close();

            _connection.Open();
        }
    }
}