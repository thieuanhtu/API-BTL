using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using DAL.Helper.Interfaces;

namespace DAL.Helper
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private string _connectionString;

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ===== Kiểu mới (Dapper) =====
        public int Execute(string commandText, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Execute(commandText, parameters, commandType: commandType);
            }
        }

        public T ExecuteScalar<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.ExecuteScalar<T>(commandText, parameters, commandType: commandType);
            }
        }

        public IEnumerable<T> Query<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.Query<T>(commandText, parameters, commandType: commandType).ToList();
            }
        }

        public T QueryFirstOrDefault<T>(string commandText, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return conn.QueryFirstOrDefault<T>(commandText, parameters, commandType: commandType);
            }
        }

        // ===== Kiểu cũ (ADO.NET thủ công) - giữ nguyên như bạn đã viết =====
        public DataTable ExecuteQuery(string spName, out string msgError, params object[] parameterNamesAndValues)
        {
            msgError = "";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameterNamesAndValues != null)
                {
                    for (int i = 0; i < parameterNamesAndValues.Length; i += 2)
                        cmd.Parameters.AddWithValue(parameterNamesAndValues[i].ToString(), parameterNamesAndValues[i + 1] ?? DBNull.Value);
                }
                try
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
                catch (Exception ex) { msgError = ex.Message; }
            }
            return dt;
        }

        public int ExecuteNonQuery(string spName, out string msgError, params object[] parameterNamesAndValues)
        {
            msgError = "";
            int result = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameterNamesAndValues != null)
                {
                    for (int i = 0; i < parameterNamesAndValues.Length; i += 2)
                        cmd.Parameters.AddWithValue(parameterNamesAndValues[i].ToString(), parameterNamesAndValues[i + 1] ?? DBNull.Value);
                }
                try
                {
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { msgError = ex.Message; }
            }
            return result;
        }

        public object ExecuteScalar(string spName, out string msgError, params object[] parameterNamesAndValues)
        {
            msgError = "";
            object result = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameterNamesAndValues != null)
                {
                    for (int i = 0; i < parameterNamesAndValues.Length; i += 2)
                        cmd.Parameters.AddWithValue(parameterNamesAndValues[i].ToString(), parameterNamesAndValues[i + 1] ?? DBNull.Value);
                }
                try
                {
                    conn.Open();
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex) { msgError = ex.Message; }
            }
            return result;
        }
    }
}