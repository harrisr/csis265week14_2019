using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.DAL;
using Csis265.Domain;

namespace Csis265.BL
{
    public class AuthorBO : BaseBO
    {
        protected AuthorDAO dao;

        public AuthorBO() : this(DEFAULT_CONNECTION_KEY)
        {

        }

        public AuthorBO(string connectionKey) : base(connectionKey)
        {
            dao = new AuthorDAO(this.connectionKey);
        }


        public override IList<object> SelectManyObjects(object obj)
        {
            return dao.SelectManyObjects(obj);
        }

        public override object SelectOneObject(object filter)
        {
            return dao.SelectOneObject(filter);
        }

        public override object InsertOneObject(object obj)
        {
            return dao.InsertOneObject(obj);
        }

        public override object UpdateOneObject(object obj)
        {
            return dao.UpdateOneObject(obj);
        }

        public override object DeleteOneObject(object obj)
        {
            return dao.DeleteOneObject(obj);
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
