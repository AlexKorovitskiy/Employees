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
    public abstract class AbstractManagerExecute<T> where T : Entity, new()
    {
        //protected delegate Result ExecutingDelegate(Context);
        //protected virtual Result Execute(ExecutingDelegate action)
        //{
        //    try
        //    {
        //        return action();
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Result(false, ex.Message);
        //    }
        //}
        #region SaveAction

        protected virtual IServerModule RelatedServer { get; set; } = DispatcherSQL.GetDispatcher();

        /// <summary>
        /// Сохранение сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected Result Save(T entity, Dictionary<string, object> param)
        {
            try
            {
                Result returnResult = new Result();

                if (entity == null)
                    entity = new T();

                {
                    Result result = BeforeSaveAction(entity, param);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                }

                {
                    var context = entity.PreparerSave(param);
                    ServerResult result = RelatedServer.ExcecuteComand(context);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                    
                }

                {
                    Result result = AfterSaveAction(entity, param);
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
                return new Result(false, ex.Message);
            }

        }

        /// <summary>
        /// Действия перед сохранением
        /// </summary>
        /// <returns></returns>
        protected virtual Result BeforeSaveAction(T entity, Dictionary<string, object> param)
        {
            return new Result();
        }

        /// <summary>
        /// Действия после сохранения
        /// </summary>
        /// <returns></returns>
        protected virtual Result AfterSaveAction(T entity, Dictionary<string, object> param)
        {
            return new Result();
        }
        #endregion

        #region LoadEntityList

        private BaseCollection<T> RelatedCollection = new BaseCollection<T>();

        public virtual Result<T> LoadEntityList(IFilter filter)
        {
            Result<T> returnResult = new Result<T>();
            ICommandContext context = RelatedCollection.PreparerLoad(filter);
            ServerResult loadResult = RelatedServer.ExcecuteComand(context);
            if (!loadResult.Success)
            {
                returnResult.Success = false;
                returnResult.Message = loadResult.Message;
                return returnResult;
            }
            return returnResult;
        }


        #endregion

        #region 

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected Result Delete(T entity)
        {
            try
            {
                Result returnResult = new Result();

                if (entity == null)
                    entity = new T();

                {
                    Result result = BeforeDeleteAction(entity);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }
                }

                {
                    var context = entity.PreparerDelete();
                    ServerResult result = RelatedServer.ExcecuteComand(context);
                    if (!result.Success)
                    {
                        returnResult.Success = false;
                        returnResult.Message = result.Message;
                        return returnResult;
                    }

                }

                {
                    Result result = AfterDeleteeAction(entity);
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
                return new Result(false, ex.Message);
            }

        }

        /// <summary>
        /// Действия перед Удалением
        /// </summary>
        /// <returns></returns>
        protected virtual Result BeforeDeleteAction(T entity)
        {
            return new Result();
        }

        /// <summary>
        /// Действия после удаления
        /// </summary>
        /// <returns></returns>
        protected virtual Result AfterDeleteeAction(T entity)
        {
            return new Result();
        }

        #endregion
    }
}
