using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class GenreMapper : BaseMapper
    {
        public GenreMapper(SqlDataReader rdr) : base(rdr)
        {   }

        public override object DoMapping()
        {
            logger.Debug("INSIDE GenreMapper DoMapping() !!!");

            int id = GetInteger("id");
            string name = GetString("name");
            DateTime dateCreated = GetDateTime("date_created");

            Genre rtnObj = new Genre(id, name, dateCreated);
            logger.Debug($"INSIDE GenreMapper DoMapping() {rtnObj.ToString()}");

            return rtnObj;
            //throw new NotImplementedException();
        }
    }
}
