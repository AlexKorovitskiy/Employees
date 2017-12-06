using Employees.Dispatcher.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employees.Dispatcher
{
    public class Dispatcher: IExecuteContext
    {
        private static Dispatcher _dispatcher;
        public static Dispatcher GetDispatcher()
        {
            if (_dispatcher == null)
                _dispatcher = new Dispatcher();
            return _dispatcher;
        }
        /// <summary>
        /// Выполнение запроса
        /// </summary>
        public List<ContextResult> ProcessCommand(IContext context, out Result resultTrransaction)
        {
            List<ContextResult> listResult = new List<ContextResult>();
            resultTrransaction = new Result(true,"");
            using (SqlCommand cmd = CreateSqlCommand(context))
            {
                SqlConnection conn = new SqlConnection();
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.ConnectionString = connectionString;
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    SqlDataReader dbReader = cmd.ExecuteReader();
                    using (dbReader)
                    {
                        if (dbReader.HasRows)
                        {
                            while (dbReader.Read())
                            {
                                var contextResult = new ContextResult();
                                for (int index = 0; index < dbReader.FieldCount; index++)
                                {
                                    object value = dbReader[index] == DBNull.Value ? null : dbReader[index];
                                    contextResult.Params.Add(dbReader.GetName(index), value);
                                }
                                listResult.Add(contextResult);
                            }
                        }
                    }
                    return listResult;
                }
                catch (Exception ex)
                {
                    resultTrransaction = new Result(false, "Проблемы при получении данных из БД");
                    return null;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Создаем объект sqlCommand
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private SqlCommand CreateSqlCommand(IContext context)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = context.ProcedureName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var callParameter in context.Params)
            {
                var dbParameter = new SqlParameter(callParameter.Key, callParameter.Value);
                cmd.Parameters.Add(dbParameter);
            }

            return cmd;
        }

    }
}