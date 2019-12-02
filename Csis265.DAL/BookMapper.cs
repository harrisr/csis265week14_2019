using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.Domain;

namespace Csis265.DAL
{
    public class BookMapper : BaseMapper
    {
        public BookMapper(SqlDataReader rdr) : base(rdr)
        { }

        public override object DoMapping()
        {
            logger.Debug("INSIDE BookMapper DoMapping() !!!");

            int id = GetInteger("id");
            int genreId = GetInteger("genre_id");
            int authorId = GetInteger("author_id");

            string name = GetString("name");
            string description = GetString("description");

            DateTime dateCreated = GetDateTime("date_created");

            Book rtnObj = new Book(id, name, dateCreated, genreId, authorId, description);
            logger.Debug($"INSIDE BookMapper DoMapping() {rtnObj.ToString()}");

            return rtnObj;
        }
    }
}
