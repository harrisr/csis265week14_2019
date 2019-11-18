using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Domain
{
    public abstract class BaseObject
    {
        protected int id;
        protected DateTime dateCreated;

        public BaseObject(int id, DateTime dateCreated)
        {
            SetId(id);
            SetDateCreated(dateCreated);
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public void SetDateCreated(DateTime dateCreated)
        {
            this.dateCreated = dateCreated;
        }


        public int GetId()
        {
            return id;
        }

        public DateTime GetDateCreated()
        {
            return dateCreated;
        }

        public override string ToString()
        {
            return $"BASE:  ID: {id}  DTC: {dateCreated}";
        }
    }
}
