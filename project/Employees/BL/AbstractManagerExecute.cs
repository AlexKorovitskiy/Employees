using API;
using Model;
using Model.API;
using Server;
using Server.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public abstract class AbstractManagerExecute<T> where T : IEntity//, new()
    {
        protected abstract string LoadProcedureName{ get; }
        protected abstract string DeleteProcedureName { get; }
        protected virtual IServerModule RelatedServer { get; set; } = DispatcherSQL.GetDispatcher();


        #region SaveAction

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Result<T> Save(T entity/*, Dictionary<string, object> param*/)
        {
            try
            {
                Result<T> returnResult = new Result<T>();

                if (entity == null)
                    return new Result<T>(false, "Не задан объект для сохрания");//entity = new T();

                {
                    Result<T> result = BeforeSaveAction(entity/*, param*/);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                }

                {
                    var context = PreparerSave(entity/*param*/);
                    ServerResult result = RelatedServer.ExcecuteComand(context);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                    
                }

                {
                    Result<T> result = AfterSaveAction(entity/*, param*/);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                }
                return returnResult;
            }
            catch (Exception ex)
            {
                return new Result<T>(false, ex.Message);
            }
        }

        protected abstract ICommandContext PreparerSave(T entity);

        /// <summary>
        /// Действия перед сохранением
        /// </summary>
        /// <returns></returns>
        protected virtual Result<T> BeforeSaveAction(T entity/*, Dictionary<string, object> param*/)
        {
            return new Result<T>();
        }

        /// <summary>
        /// Действия после сохранения
        /// </summary>
        /// <returns></returns>
        protected virtual Result<T> AfterSaveAction(T entity/*, Dictionary<string, object> param*/)
        {
            return new Result<T>();
        }
        #endregion

        #region LoadEntity

        public Result<T> LoadEntity(int? id)
        {
            if (id == null || id < 0)
                return new Result<T>(false, "Не задан Id");

            ICommandContext context;
            {
                context = new CommandContext();
                context.ProcedureName = LoadProcedureName;
                context.Params.Add("Id", id);
            }

            ServerResult loadResult;
            {
                loadResult = RelatedServer.ExcecuteComand(context);
                if (!loadResult.Success)
                    return new Result<T>(false, loadResult.Message);
                if (loadResult.ResultValuesList.Count != 1)
                    return new Result<T>(false, "Объект не найден");
            }
            { 
                T resultObject = ParseLoadEntity(loadResult.ResultValuesList[0]);
                if (resultObject == null)
                    return new Result<T>(false, "Объект не найден");

                return new Result<T>() { ResultEntity = resultObject };
            }
        }

        protected abstract T ParseLoadEntity(Dictionary<string, object> param);

        #endregion

        #region LoadEntityList
        
        public Result<T> LoadEntityList(IFilter filter)
        {
            Result<T> returnResult = new Result<T>();
            ICommandContext context = PreparerLoadList(filter);
            ServerResult loadResult = RelatedServer.ExcecuteComand(context);
            if (!loadResult.Success)
            {
                returnResult.Success = false;
                returnResult.Message = loadResult.Message;
                return returnResult;
            }
            foreach (Dictionary<string,object> item in loadResult.ResultValuesList)
            {
                T entity = ParseLoadEntity(item);
                if (item != null)
                    returnResult.Entitys.Add(entity);
            }
            return returnResult;
        }
        protected abstract ICommandContext PreparerLoadList(IFilter filter);

        #endregion

        #region Delete 

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Result<T> Delete(T entity)
        {
            if (entity == null)
                return new Result<T>(false, "Не задан объект для удаления");
            return Delete(entity.Id);
        }

        public Result<T> Delete(int? id)
        {
            if (id == null || id < 0)
                return new Result<T>(false, "Не задан Id");

            ICommandContext context;
            {
                context = new CommandContext();
                context.ProcedureName = DeleteProcedureName;
                context.Params.Add("Id", id);
            }

            ServerResult deleteResult;
            {
                deleteResult = RelatedServer.ExcecuteComand(context);
                if (!deleteResult.Success)
                    return new Result<T>(false, deleteResult.Message);
            }
            return new Result<T>();
        }
        
        #endregion
    }
}
