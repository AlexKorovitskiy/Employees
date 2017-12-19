using API;
using Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Result<T> : IResult where T:IEntity 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T ResultEntity { get; set; }
        public List<T> Entitys { get; set; }

        public Result()
        {
            Success = true;
            Message = null;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
