using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class BookDAO : BaseDAO
    {
        protected string selectOneSql = "SELECT ID, NAME, DESCRIPTION, GENRE_ID, AUTHOR_ID, DATE_CREATED FROM BOOK WHERE ID = @idParm;";
        protected string selectManySql = "SELECT ID, NAME, DESCRIPTION, GENRE_ID, AUTHOR_ID, DATE_CREATED FROM BOOK WHERE NAME LIKE  @nameParm;";

        protected string insertOneSql = "INSERT INTO BOOK (NAME, DESCRIPTION, GENRE_ID, AUTHOR_ID) " +
                                        "VALUES (@nameParm, @descParm, @genreParm, @authorParm); SELECT SCOPE_IDENTITY();  ";

        protected string updateOneSql = "UPDATE BOOK SET NAME = @nameParm " +
                                        "  , DESCRIPTION =  @descParm  " +
                                        "  , GENRE_ID =  @genreParm  " +
                                        "  , AUTHOR_ID =  @authorParm  " +
                                        " WHERE ID = @idParm;";

        protected string deleteOneSql = "DELETE FROM BOOK WHERE ID = @idParm;";



        public BookDAO(string connectionKey) : base(connectionKey)
        {

        }

        public override object SelectOneObject(object obj)
        {
            try
            {
                Book filter = (Book)obj;
                Book rtnObj = null;
                int id;
                string name;
                DateTime dateTime;

                logger.Debug("INSIDE SelectOneObject !!!!!!");

                conn = new SqlConnection(connString);
                conn.Open();

                sql = selectOneSql;

                cmd = new SqlCommand(sql, conn);

                SqlParameter idParm = new SqlParameter();
                idParm.ParameterName = "@idParm";
                idParm.Value = filter.GetId();

                cmd.Parameters.Add(idParm);

                rdr = cmd.ExecuteReader();
                BookMapper mapper = new BookMapper(rdr);
                while (rdr.Read())
                {
                    rtnObj = (Book)mapper.DoMapping();
                }
                logger.Debug(rtnObj.ToString());

                return rtnObj;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
        }


        public override IList<object> SelectManyObjects(object obj)
        {
            try
            {
                IList<object> objList = new List<object>();
                Book filter = (Book)obj;
                Book rtnObj = null;
                int id;
                string name;
                DateTime dateTime;

                logger.Debug("INSIDE SelectManyObjects !!!!!!");

                conn = new SqlConnection(connString);
                conn.Open();

                sql = selectManySql;

                cmd = new SqlCommand(sql, conn);

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = '%' + filter.GetName() + '%';

                cmd.Parameters.Add(nameParm);

                rdr = cmd.ExecuteReader();
                BookMapper mapper = new BookMapper(rdr);
                while (rdr.Read())
                {
                    rtnObj = (Book)mapper.DoMapping();

                    logger.Debug($"GETTING FROM DB:  {rtnObj.ToString()}");
                    objList.Add(rtnObj);
                }
                return objList;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
        }


        public override object InsertOneObject(object obj)
        {
            try
            {
                Book rtnObj = (Book)obj;
                logger.Debug(rtnObj.ToString());

                int id;

                logger.Debug("INSIDE InsertOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = insertOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();
                
                SqlParameter descParm = new SqlParameter();
                descParm.ParameterName = "@descParm";
                descParm.Value = rtnObj.GetDescription();
                
                SqlParameter genreParm = new SqlParameter();
                genreParm.ParameterName = "@genreParm";
                genreParm.Value = rtnObj.GetGenreId();
                
                SqlParameter authorParm = new SqlParameter();
                authorParm.ParameterName = "@authorParm";
                authorParm.Value = rtnObj.GetAuthorId();

                conn.Open();
                adpt.InsertCommand = new SqlCommand(sql, conn);
                adpt.InsertCommand.Parameters.Add(nameParm);
                adpt.InsertCommand.Parameters.Add(descParm);
                adpt.InsertCommand.Parameters.Add(genreParm);
                adpt.InsertCommand.Parameters.Add(authorParm);

                id = Convert.ToInt32(adpt.InsertCommand.ExecuteScalar());
                rtnObj.SetId(id);

                logger.Debug(rtnObj.ToString());
                return rtnObj;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
        }


        public override object UpdateOneObject(object obj)
        {
            try
            {
                Book rtnObj = (Book)obj;
                logger.Debug(rtnObj.ToString());
                logger.Debug("INSIDE UpdateOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = updateOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();

                SqlParameter descParm = new SqlParameter();
                descParm.ParameterName = "@descParm";
                descParm.Value = rtnObj.GetDescription();

                SqlParameter genreParm = new SqlParameter();
                genreParm.ParameterName = "@genreParm";
                genreParm.Value = rtnObj.GetGenreId();

                SqlParameter authorParm = new SqlParameter();
                authorParm.ParameterName = "@authorParm";
                authorParm.Value = rtnObj.GetAuthorId();
                
                SqlParameter idParm = new SqlParameter();
                idParm.ParameterName = "@idParm";
                idParm.Value = rtnObj.GetId();

                conn.Open();
                adpt.UpdateCommand = new SqlCommand(sql, conn);
                adpt.UpdateCommand.Parameters.Add(nameParm);
                adpt.UpdateCommand.Parameters.Add(descParm);
                adpt.UpdateCommand.Parameters.Add(genreParm);
                adpt.UpdateCommand.Parameters.Add(authorParm);
                adpt.UpdateCommand.Parameters.Add(idParm);

                adpt.UpdateCommand.ExecuteNonQuery();

                logger.Debug(rtnObj.ToString());
                return rtnObj;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
        }




        public override object DeleteOneObject(object obj)
        {
            try
            {
                Book rtnObj = (Book)obj;
                logger.Debug(rtnObj.ToString());
                logger.Debug("INSIDE DeleteOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = deleteOneSql;

                SqlParameter idParm = new SqlParameter();
                idParm.ParameterName = "@idParm";
                idParm.Value = rtnObj.GetId();

                conn.Open();
                adpt.DeleteCommand = new SqlCommand(sql, conn);
                adpt.DeleteCommand.Parameters.Add(idParm);

                adpt.DeleteCommand.ExecuteNonQuery();

                logger.Debug(rtnObj.ToString());
                return rtnObj;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
        }



        public override void DeleteManyObjects()
        {
            throw new NotImplementedException();
        }


        public override void InsertManyObjects()
        {
            throw new NotImplementedException();
        }




        public override void UpdateManyObjects()
        {
            throw new NotImplementedException();
        }


    }
}
