using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class AuthorDAO : BaseDAO
    {
        protected string selectOneSql = "SELECT ID, NAME, EMAIL_ADDRESS, DATE_CREATED FROM AUTHOR WHERE ID = @idParm;";
        protected string selectManySql = "SELECT ID, NAME, EMAIL_ADDRESS, DATE_CREATED FROM AUTHOR WHERE NAME LIKE  @nameParm;";

        protected string insertOneSql = "INSERT INTO AUTHOR (NAME, EMAIL_ADDRESS) VALUES (@nameParm, @emailParm); SELECT SCOPE_IDENTITY();  ";

        protected string updateOneSql = "UPDATE AUTHOR SET NAME = @nameParm, EMAIL_ADDRESS = @emailParm WHERE ID = @idParm;";

        protected string deleteOneSql = "DELETE FROM AUTHOR WHERE ID = @idParm;";



        public AuthorDAO(string connectionKey) : base(connectionKey)
        {

        }

        public override object SelectOneObject(object obj)
        {
            try
            {
                Author filter = (Author)obj;
                Author rtnObj = null;
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
                AuthorMapper mapper = new AuthorMapper(rdr);
                while (rdr.Read())
                {
                    rtnObj = (Author)mapper.DoMapping();

                    //id =  Convert.ToInt32(rdr["Id"].ToString());
                    //name = rdr["name"].ToString();
                    //dateTime =  Convert.ToDateTime(rdr["date_created"].ToString());
                    //rtnObj = new Author(id, name, dateTime);

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


        public override IList<object> SelectManyObjects(object obj)
        {
            try
            {
                IList<object> objList = new List<object>();
                Author filter = (Author)obj;
                Author rtnObj = null;
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
                AuthorMapper mapper = new AuthorMapper(rdr);
                while (rdr.Read())
                {
                    //id = Convert.ToInt32(rdr["Id"].ToString());
                    //name = rdr["name"].ToString();
                    //dateTime = Convert.ToDateTime(rdr["date_created"].ToString());
                    //rtnObj = new Author(id, name, dateTime);

                    rtnObj = (Author)mapper.DoMapping();

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
                Author rtnObj = (Author)obj;
                logger.Debug(rtnObj.ToString());

                int id;

                logger.Debug("INSIDE InsertOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = insertOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();

                SqlParameter emailParm = new SqlParameter();
                emailParm.ParameterName = "@emailParm";
                emailParm.Value = rtnObj.GetEmailAddress();

                conn.Open();
                adpt.InsertCommand = new SqlCommand(sql, conn);
                adpt.InsertCommand.Parameters.Add(nameParm);
                adpt.InsertCommand.Parameters.Add(emailParm);

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
                Author rtnObj = (Author)obj;
                logger.Debug(rtnObj.ToString());
                logger.Debug("INSIDE UpdateOneObject !!!!!!");

                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = updateOneSql;

                SqlParameter nameParm = new SqlParameter();
                nameParm.ParameterName = "@nameParm";
                nameParm.Value = rtnObj.GetName();

                SqlParameter emailParm = new SqlParameter();
                emailParm.ParameterName = "@emailParm";
                emailParm.Value = rtnObj.GetEmailAddress();

                SqlParameter idParm = new SqlParameter();
                idParm.ParameterName = "@idParm";
                idParm.Value = rtnObj.GetId();

                conn.Open();
                adpt.UpdateCommand = new SqlCommand(sql, conn);
                adpt.UpdateCommand.Parameters.Add(nameParm);
                adpt.UpdateCommand.Parameters.Add(emailParm);
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
                Author rtnObj = (Author)obj;
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
