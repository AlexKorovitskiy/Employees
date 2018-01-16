using API.Model;
using Employees.Dispatcher;
using Employees.Dispatcher.API;
using Model.API;
using System.Collections.Generic;

namespace Employees.BL
{
    /// <summary>
    /// Абстрактный клас для BL(сюда я вынес общую логику для объектов)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ManagerExecute<T> where T:IEntity
    {
        /// <summary>
        /// Объект, выполняющий работу с базой
        /// </summary>
        internal static IExecuteContext ExecuterDBAction
        {
            get
            {
                return Dispatcher.Dispatcher.GetDispatcher();
            }
        }

        public virtual void Update(T entity)
        {
            //IContext context = entity.PrepareSave();
            //Result result;
            //List<ContextResult> resultCollection = ExecuterDBAction.ProcessCommand(context, out result);
            //if (result.Success)
            //{

            //}
        }

        public virtual void Delete(T entity)
        {
            //IContext context = entity.PrepareDelete();
            //Result result;
            //List<ContextResult> resultCollection = ExecuterDBAction.ProcessCommand(context, out result);
            //if (result.Success)
            //{

            //}
        }
    }
}