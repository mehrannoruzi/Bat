using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Bat.Dapper
{
    public class TableValueParameter : SqlMapper.ICustomQueryParameter
    {
        private string _typeName;
        private DataTable _dataTable;

        public TableValueParameter(DataTable dataTable)
        {
            _dataTable = dataTable;
            _typeName = dataTable.TableName;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = (SqlParameter)command.CreateParameter();

            parameter.ParameterName = name;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = _dataTable;
            parameter.TypeName = _typeName;

            command.Parameters.Add(parameter);
        }
    }
}
