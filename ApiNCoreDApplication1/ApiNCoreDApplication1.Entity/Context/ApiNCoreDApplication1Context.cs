
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ApiNCoreDApplication1.Entity.Context
{

    public interface IDbConnectionProvider
    {
        IDbConnection Connection { get; }
    }

    public class ApiNCoreDApplication1Context : IDbConnectionProvider
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public ApiNCoreDApplication1Context(string connection)
        {
            _connection = new SqlConnection(connection);
        }

        public IDbConnection Connection { get => _connection; }
        public IDbTransaction Transaction { get => _transaction; set => _transaction = value; }

    }
}





