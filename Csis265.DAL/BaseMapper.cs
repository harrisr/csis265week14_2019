using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Csis265.DAL
{
    public abstract class BaseMapper
    {
        protected static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected SqlDataReader rdr;


        public BaseMapper(SqlDataReader rdr)
        {
            log4net.Config.XmlConfigurator.Configure();
            this.rdr = rdr;
            logger.Debug("INSIDE BaseMapper() CONSTRUCTOR!!!");
        }

        public abstract object DoMapping();
        
        protected int GetInteger(string columnName)
        {
            try
            {
                logger.Debug("INSIDE MAPPING GetInteger()");

                return Convert.ToInt32(rdr[columnName].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return -1;
                // throw;    no need to make the app crash...  
                // just supply a default "something bad happened" value
            }
        }

        protected string GetString(string columnName)
        {
            try
            {
                logger.Debug("INSIDE MAPPING GetString()");

                return rdr[columnName].ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return string.Empty;
            }
        }
               
        protected DateTime GetDateTime(string columnName)
        {
            try
            {
                logger.Debug("INSIDE MAPPING GetDateTime()");

                return Convert.ToDateTime(rdr[columnName].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return DateTime.MinValue;
            }
        }
    }
}
