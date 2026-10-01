using System.Collections.Generic;
using System.Data;

namespace DAL.Helper.Interfaces
{
    public interface IDatabaseHelper
    {
        void SetConnectionString(string connectionString);

        // Kiểu mới (Dapper) - HoaDonRepository dùng kiểu này
        int Execute(string commandText, object parameters = null, CommandType commandType = CommandType.Text);
        T ExecuteScalar<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text);
        IEnumerable<T> Query<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text);
        T QueryFirstOrDefault<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text);

        // Kiểu cũ (ADO.NET thủ công) - CustomerRepository, CafeTableRepository... đang dùng kiểu này
        DataTable ExecuteQuery(string spName, out string msgError, params object[] parameterNamesAndValues);
        int ExecuteNonQuery(string spName, out string msgError, params object[] parameterNamesAndValues);
        object ExecuteScalar(string spName, out string msgError, params object[] parameterNamesAndValues);
    }
}