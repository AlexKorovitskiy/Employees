using API;
using Server.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DispatcherSQL:IServerModule
    {
        private static DispatcherSQL _dispatcher;
        public static DispatcherSQL GetDispatcher()
        {
            if (_dispatcher == null)
                _dispatcher = new DispatcherSQL();
            return _dispatcher;
        }

        private DispatcherSQL()
        {
        }

        /// <summary>
        /// Выполнение запроса
        /// </summary>
        public ServerResult ExcecuteComand(ICommandContext context)
        {
            //List<ContextResult> listResult = new List<ContextResult>();
            //resultTrransaction = new Result(true, "");
            ServerResult result = new ServerResult();
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
                                Dictionary<string, object> resultParams = new Dictionary<string, object>();
                                for (int index = 0; index < dbReader.FieldCount; index++)
                                {
                                    object value = dbReader[index] == DBNull.Value ? null : dbReader[index];
                                    resultParams.Add(dbReader.GetName(index), value);
                                }
                                result.ResultValuesList.Add(resultParams);
                            }
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return new ServerResult(false, ex.Message);
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
        private SqlCommand CreateSqlCommand(ICommandContext context)
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
