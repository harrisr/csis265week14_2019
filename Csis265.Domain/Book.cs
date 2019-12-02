using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csis265.Domain
{
    public class Book : BaseObject
    {
        protected string name;
        protected string description;
        protected int genreId;
        protected int authorId;

        protected Genre genre;
        protected Author author;

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

        public Genre Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public Author Author
        {
            get { return author; }
            set { author = value; }
        }

        public string GenreName
        {
            get { return genre.Name; }
            set { genre.Name = value; }
        }

        public string AuthorName
        {
            get { return author.Name; }
            set { author.Name = value; }
        }


        public Book(int id, string name, DateTime dateCreated,
            int genreId, int authorId, string description)
            : base(id, dateCreated)
        {
            SetName(name);
            SetDescription(description);
            SetGenreId(genreId);
            SetAuthorId(authorId);
        }

        public void SetName(string name)
        {
            if (name.Trim().Length <= 0)
            {
                throw new LibraryException("Genre name must not be blank");
            }
            this.name = name;
        }



        public void SetDescription(string description)
        {
            this.description = description;
        }

        public void SetGenreId(int genreId)
        {
            this.genreId = genreId;
        }

        public void SetAuthorId(int authorId)
        {
            this.authorId = authorId;
        }


        public string GetName()
        {
            return name;
        }


        public string GetDescription()
        {
            return description;
        }

        public int GetGenreId()
        {
            return genreId;
        }

        public int GetAuthorId()
        {
            return authorId;
        }

        public override string ToString()
        {
            return $"BOOK:  ID: {id}  NM: {name}  DSC: {description}  DTC: {dateCreated}";
        }
    }
}
