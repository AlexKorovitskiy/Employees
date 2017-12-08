using API;
using Server;
using Server.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.API
{
    interface IServerModule
    {
        /// <summary>
        /// Выполнение запроса
        /// </summary>
        ServerResult ExcecuteComand(ICommandContext context);
    }
}
