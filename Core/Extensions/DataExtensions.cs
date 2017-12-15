#pragma warning disable 1591
using System;
using System.Data.SqlClient;
using System.Data;

namespace Core.Extensions
{
    public static class DataExtensions
    {
        public static TResult GetValue<TResult>(this DataRow row, string column)
        {
            if (row == null)
                return default(TResult);

            if (row.Table.Columns[column] == null)
                return default(TResult);

            if (row.IsNull(column))
                return default(TResult);

            return (TResult)row[column];
        }

        public static TResult GetValue<TResult>(this DataRowView row, string column)
        {
            if (row == null)
                return default(TResult);

            if (row.Row.Table.Columns[column] == null)
                return default(TResult);

            if (row.Row.IsNull(column))
                return default(TResult);

            return (TResult)row[column];
        }

        public static void Fill(this DataTable table, string connectString, string commandText, Action<SqlParameterCollection> setParamsFunc)
        {
            table.Fill(connectString, commandText, CommandType.StoredProcedure, setParamsFunc);
        }

        public static void Fill(this DataTable table, string connectString, string commandText, CommandType commandType,
                                Action<SqlParameterCollection> setParamsFunc)
        {
            using (var lConnection = new SqlConnection(connectString))
            using (var lCommand = new SqlCommand())
            {
                lConnection.Open();
                lCommand.Connection = lConnection;
                lCommand.CommandType = commandType;
                lCommand.CommandTimeout = 3000;
                lCommand.CommandText = commandText;
                if (setParamsFunc != null)
                    setParamsFunc(lCommand.Parameters);
                using (var da = new SqlDataAdapter(lCommand))
                {
                    da.Fill(table);
                }
            }
        }

    }
}
#pragma warning restore 1591
