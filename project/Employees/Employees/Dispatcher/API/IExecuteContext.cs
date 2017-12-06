using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Dispatcher.API
{
    public interface IExecuteContext
    {
        /// <summary>
        /// Выполнение запроса в БД
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resultTrransaction"></param>
        /// <returns></returns>
        List<ContextResult> ProcessCommand(IContext context, out Result resultTrransaction);
    }
}
