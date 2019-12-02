using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Csis265.BL
{
    public abstract class BaseBO
    {
        protected static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static readonly string DEFAULT_CONNECTION_KEY = "defaultConnection";

        protected string connectionKey;

        public BaseBO() : this(DEFAULT_CONNECTION_KEY)
        {

        }

        public BaseBO(string connectionKey)
        {
            log4net.Config.XmlConfigurator.Configure();
            SetConnectionKey(connectionKey);
        }

        public void SetConnectionKey(string connectionKey)
        {
            if (connectionKey.Trim().Length <= 0)
            {
                throw new BLException("BL Connection Key cannot be blank");
            }
            this.connectionKey = connectionKey;
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
