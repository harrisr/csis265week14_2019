using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Domain
{
    public class Genre : BaseObject
    {
        protected string name;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Genre(int id, string name, DateTime dateCreated)
            : base(id, dateCreated)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (name.Trim().Length <= 0)
            {
                throw new LibraryException("Genre name must not be blank");
            }
            this.name = name;
        }
        
        public string GetName()
        {
            return name;
        }

        public override string ToString()
        {
            return $"GENRE:  ID: {id}  NM: {name}  DTC: {dateCreated}";
            //return base.ToString();)
        }
    }
}
