using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;
namespace Server
{
    public class ServerResult: IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Список данных, пришедших из запроса. key - имя, пришедшее из запроса, value - само значение
        /// </summary>
        public List<Dictionary<string, object>> ResultValuesList { get; set; } = new List<Dictionary<string, object>>();

        public ServerResult()
        {
            Success = true;
            Message = null;
        }
        public ServerResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
