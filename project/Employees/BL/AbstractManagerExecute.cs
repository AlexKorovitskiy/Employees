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
        #region SaveAction

        protected virtual IServerModule RelatedServer { get; set; } = DispatcherSQL.GetDispatcher();

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
                    var context = PreparerSave(/*param*/);
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

        protected abstract ICommandContext PreparerSave();

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
            try
            {
                Result<T> returnResult = new Result<T>();

                if (entity == null)
                    return new Result<T>(false, "Не задан объект для удаления!");
                {
                    Result<T> result = BeforeDeleteAction(entity);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                }

                {
                    ICommandContext context = PreparerDelete(entity);
                    ServerResult result = RelatedServer.ExcecuteComand(context);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }

                }

                {
                    Result<T> result = AfterDeleteeAction(entity);
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

        public Result<T> Delete(int? id)
        {
            if (id == null)
                return new Result<T>(false, "Не задан объект для удаления");
            var result = LoadEntity(id);
            if (!result.Success)
                return new Result<T>(false, "Не удалось получить объект перед удалением");
            return Delete(result.ResultEntity);
        }

        protected abstract ICommandContext PreparerDelete(T entity);

        /// <summary>
        /// Действия перед Удалением
        /// </summary>
        /// <returns></returns>
        protected virtual Result<T> BeforeDeleteAction(T entity)
        {
            return new Result<T>();
        }

        /// <summary>
        /// Действия после удаления
        /// </summary>
        /// <returns></returns>
        protected virtual Result<T> AfterDeleteeAction(T entity)
        {
            return new Result<T>();
        }

        #endregion
    }
}
