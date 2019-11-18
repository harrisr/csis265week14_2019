using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class GenreDAO : BaseDAO
    {
        protected string selectOneSql = "SELECT ID, NAME, DATE_CREATED FROM GENRE WHERE ID = @idParm;";
        protected string selectManySql = "SELECT ID, NAME, DATE_CREATED FROM GENRE WHERE NAME LIKE  @nameParm;";

        protected string insertOneSql = "INSERT INTO GENRE (NAME) VALUES (@nameParm); SELECT SCOPE_IDENTITY();  ";

        protected string updateOneSql = "UPDATE GENRE SET NAME = @nameParm WHERE ID = @idParm;";

        protected string deleteOneSql = "DELETE FROM GENRE WHERE ID = @idParm;";



        public GenreDAO(string connectionKey) : base(connectionKey)
        {

        }

        public override object SelectOneObject(object obj)
        {
            try
            {
                Genre filter = (Genre)obj;
                Genre rtnObj = null;
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
                GenreMapper mapper = new GenreMapper(rdr);
                while (rdr.Read())
                {
                    rtnObj = (Genre)mapper.DoMapping();

                    //id =  Convert.ToInt32(rdr["Id"].ToString());
                    //name = rdr["name"].ToString();
                    //dateTime =  Convert.ToDateTime(rdr["date_created"].ToString());
                    //rtnObj = new Genre(id, name, dateTime);

                    //rtnObj.SetId(id);
                    //rtnObj.SetName(name);
                    //rtnObj.SetDateCreated(dateTime);
                }
                //rdr.Close();
                //cmd.Dispose();
                //conn.Close();
                //conn.Dispose();

                logger.Debug(rtnObj.ToString());

                return rtnObj;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            finally
            {
                Cleanup();
            }
            //throw new NotImplementedException();
        }


        public override IList<object> SelectManyObjects(object obj)
        {
            try
            {
                IList<object> objList = new List<object>();
                Genre filter = (Genre)obj;
                Genre rtnObj = null;
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
                nameParm.Value =   '%' + filter.GetName() + '%';

                cmd.Parameters.Add(nameParm);

                rdr = cmd.ExecuteReader();
                GenreMapper mapper = new GenreMapper(rdr);
                while (rdr.Read())
                {
                    //id = Convert.ToInt32(rdr["Id"].ToString());
                    //name = rdr["name"].ToString();
                    //dateTime = Convert.ToDateTime(rdr["date_created"].ToString());
                    //rtnObj = new Genre(id, name, dateTime);

                    rtnObj = (Genre)mapper.DoMapping();

                    logger.Debug($"GETTING FROM DB:  {rtnObj.ToString()}");
                    objList.Add(rtnObj);
                }
                //rdr.Close();
                //cmd.Dispose();
                //conn.Close();
                //conn.Dispose();
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


            //throw new NotImplementedException();
        }


        public override object InsertOneObject(object obj)
        {
            try
            {
                Genre rtnObj = (Genre)obj;
                logger.Debug(rtnObj.ToString());

                int id;

                logger.Debug("INSIDE InsertOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = insertOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();

                conn.Open();
                adpt.InsertCommand = new SqlCommand(sql, conn);
                adpt.InsertCommand.Parameters.Add(nameParm);

                id = Convert.ToInt32(adpt.InsertCommand.ExecuteScalar());
                rtnObj.SetId(id);
                               
                //adpt.Dispose();
                //conn.Close();
                //conn.Dispose();

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

            //throw new NotImplementedException();
        }


        public override object UpdateOneObject(object obj)
        {
            try
            {
                Genre rtnObj = (Genre)obj;
                logger.Debug(rtnObj.ToString());
                logger.Debug("INSIDE UpdateOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = updateOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();

                SqlParameter idParm = new SqlParameter();
                idParm.ParameterName = "@idParm";
                idParm.Value = rtnObj.GetId();

                conn.Open();
                adpt.UpdateCommand = new SqlCommand(sql, conn);
                adpt.UpdateCommand.Parameters.Add(nameParm);
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
                Genre rtnObj = (Genre)obj;
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
