using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class AuthorMapper : BaseMapper
    {
        public AuthorMapper(SqlDataReader rdr) : base(rdr)
        { }

        public override object DoMapping()
        {
            logger.Debug("INSIDE AuthorMapper DoMapping() !!!");

            int id = GetInteger("id");
            string name = GetString("name");
            string email = GetString("email_address");
            DateTime dateCreated = GetDateTime("date_created");

            Author rtnObj = new Author(id, name, email, dateCreated);
            logger.Debug($"INSIDE AuthorMapper DoMapping() {rtnObj.ToString()}");

            return rtnObj;
        }
    }
}
