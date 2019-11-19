using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Domain
{
    public class Author : BaseObject
    {
        protected string name;
        protected string emailAddress;

        public Author(int id, string name, string emailAddress, DateTime dateCreated)
            : base(id, dateCreated)
        {
            SetName(name);
            SetEmailAddress(emailAddress);
        }

        public void SetEmailAddress(string emailAddress)
        {
            this.emailAddress = emailAddress;
        }

        public void SetName(string name)
        {
            if (name.Trim().Length <= 0)
            {
                throw new LibraryException("Author name must not be blank");
            }
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public string GetEmailAddress()
        {
            return emailAddress;
        }

        public override string ToString()
        {
            return $"AUTHOR:  ID: {id}  NM: {name}   EM: {emailAddress}  DTC: {dateCreated}";
            //return base.ToString();)
        }
    }
}
