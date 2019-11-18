using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;


namespace Csis265.DAL
{
    public abstract class BaseDAO
    {
        protected static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string DEFAULT_CONNECTION_KEY = "defaultConnection";

        protected string connString;
        protected SqlConnection conn;
        protected SqlCommand cmd;
        protected SqlDataReader rdr;
        protected SqlDataAdapter adpt;
        /////////////////////protected SqlParameter sqlParm;
        protected string sql;

        public BaseDAO() : this(DEFAULT_CONNECTION_KEY)
        {

        }

        public BaseDAO(string connectionKey)
        {
            log4net.Config.XmlConfigurator.Configure();

            SetConnectionString(connectionKey);
        }

        protected void SetConnectionString(string connectionKey)
        {
            if (connectionKey.Trim().Length <= 0)
            {
                throw new DALException("DAL Connection Key cannot be blank");
            }

            try
            {
                connString = ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw new DALException("DAL Connection Key is missing from the CONFIG file");
            }

            if (connString.Trim().Length <= 0)
            {
                throw new DALException("DAL Connection String does not exist in the CONFIG file");
            }

            logger.Debug($"CONNSTR:   {connString}");

            //throw new NotImplementedException();
        }

        public void Cleanup()
        {
            if (rdr != null)
            {
                rdr.Close();
            }

            if (cmd != null)
            {
                cmd.Dispose();
            }

            if (adpt != null)
            {
                adpt.Dispose();
            }

            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }


        public abstract object SelectOneObject(object filter);
        public abstract IList<object> SelectManyObjects(object obj);
        public abstract object InsertOneObject(object obj);
        public abstract void InsertManyObjects();
        public abstract object UpdateOneObject(object obj);
        public abstract void UpdateManyObjects();
        public abstract object DeleteOneObject(object obj);
        public abstract void DeleteManyObjects();

    }
}
