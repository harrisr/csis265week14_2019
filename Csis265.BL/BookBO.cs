using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csis265.DAL;
using Csis265.Domain;

namespace Csis265.BL
{
    public class BookBO : BaseBO
    {
        protected BookDAO dao;
        protected GenreDAO genreDAO;
        protected AuthorDAO authorDAO;

        public BookBO() : this(DEFAULT_CONNECTION_KEY)
        {

        }

        public BookBO(string connectionKey) : base(connectionKey)
        {
            dao = new BookDAO(this.connectionKey);
            genreDAO = new GenreDAO(this.connectionKey);
            authorDAO = new AuthorDAO(this.connectionKey);
        }


        public override IList<object> SelectManyObjects(object obj)
        {
            return dao.SelectManyObjects(obj);
        }

        public override object SelectOneObject(object filter)
        {
            Book book = (Book)dao.SelectOneObject(filter);
            book.Author = (Author)authorDAO.SelectOneObject(new Author(book.GetAuthorId(), "Z","Z",DateTime.Now));
            book.Genre = (Genre)genreDAO.SelectOneObject(new Genre(book.GetGenreId(), "Z", DateTime.Now));

            logger.Debug($"{book.ToString()}");
            return book;
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
