using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using APP.LMM.Common;

namespace APP.LMM.DataAccess
{
    public class BaseDataAccess
    {
        private SqlConnection sqlConn
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings[CommonFunction.LMM_Connstring].ConnectionString);
            }
        }

        protected internal SqlParameter SqlParam(string paramName, object paramValue, SqlDbType paramType, ParameterDirection direction)
        {
            SqlParameter result = new SqlParameter(paramName, paramValue);
            result.SqlDbType = paramType;
            result.Direction = direction;
            return result;
        }

        protected internal SqlParameter SqlParam(string paramName, object paramValue, SqlDbType paramType, ParameterDirection direction, int size)
        {
            SqlParameter result = new SqlParameter(paramName, paramValue);
            result.SqlDbType = paramType;
            result.Size = size;
            result.Direction = direction;
            return result;
        }

        protected internal SqlCommand StoredProcedure(string storedProcedureName)
        {
            SqlCommand result = new SqlCommand();
            result.CommandText = storedProcedureName;
            result.CommandType = CommandType.StoredProcedure;
            result.Connection = sqlConn;
            return result;
        }
    }
}
