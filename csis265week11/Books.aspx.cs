using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Csis265.Domain;
using Csis265.DAL;
using Csis265.BL;

namespace csis265week11
{
    public partial class Books : System.Web.UI.Page
    {
        private static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ///////////////private BookDAO dao;
        private BookBO bo;
        private GenreBO genreBO;
        private AuthorBO authorBO;


        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            ///////////////dao = new BookDAO("localhost");
            bo = new BookBO("localhost");
            genreBO = new GenreBO("localhost");
            authorBO = new AuthorBO("localhost");

            if (Page.IsPostBack)
            {

            }
            else
            {
                PopulateDataControls();
            }
        }
        private void PopulateDataControls()
        {
            PopulateBookDropdown();
            PopulateGenreDropdown();
            PopulateAuthorDropdown();
        }

        private void PopulateGenreDropdown()
        {
            ///////IList<object> genres = dao.SelectManyObjects(new Genre(-1, "%", DateTime.Now));
            IList<object> genres = genreBO.SelectManyObjects(new Genre(-1, "%", DateTime.Now));

            drpGenres.DataSource = genres;
            drpGenres.DataValueField = "Id";
            drpGenres.DataTextField = "Name";
            drpGenres.DataBind();
        }
        private void PopulateAuthorDropdown()
        {
            ///////IList<object> authors = dao.SelectManyObjects(new Author(-1, "%", DateTime.Now));
            IList<object> authors = authorBO.SelectManyObjects(new Author(-1, "%", "BLANK", DateTime.Now));

            drpAuthors.DataSource = authors;
            drpAuthors.DataValueField = "Id";
            drpAuthors.DataTextField = "Name";
            drpAuthors.DataBind();
        }

        private void PopulateBookDropdown()
        {
            ///////IList<object> books = dao.SelectManyObjects(new Book(-1, "%", DateTime.Now));
            IList<object> books = bo.SelectManyObjects(new Book(-1, "%", DateTime.Now, -1, -1, "Z"));

            drpBooks.DataSource = books;
            drpBooks.DataValueField = "Id";
            drpBooks.DataTextField = "Name";
            drpBooks.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                string bookName = txtBookName.Text;
                string bookDesc = txtBookDesc.Text;
                int bookId;
                int genreId = Convert.ToInt32(drpGenres.SelectedValue);
                int authorId = Convert.ToInt32(drpAuthors.SelectedValue);
                Book temp;

                if (btnSubmit.Text.ToUpper().Equals("EDIT"))
                {
                    ////////   DOING AN UPDATE
                    bookId = Convert.ToInt32(hdnBookId.Value);
                    temp = new Book(bookId, bookName, DateTime.Now, genreId, authorId, bookDesc);
                    //////////////dao.UpdateOneObject(temp);
                    bo.UpdateOneObject(temp);
                    lblMessage.Text = "Book successfully edited";
                    btnSubmit.Text = "Add";
                }
                else
                {
                    ////////   DOING AN INSERT
                    temp = new Book(-1, bookName, DateTime.Now, genreId, authorId, bookDesc);
                    /////////////temp = (Book)dao.InsertOneObject(temp);
                    temp = (Book)bo.InsertOneObject(temp);
                    lblMessage.Text = "Book successfully added";
                }
                PopulateDataControls();
                //PopulateBookDropdown();
                hdnBookId.Value = string.Empty;
                txtBookName.Text = string.Empty;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                logger.Debug($"GENRE: {temp.ToString()}");
            }
            catch (LibraryException lex)
            {
                lblMessage.Text = lex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                logger.Error(lex);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                logger.Error(ex);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            logger.Debug($"drpBooks.SelectedIndex: {drpBooks.SelectedIndex}");
            logger.Debug($"drpBooks.SelectedValue: {drpBooks.SelectedValue}");
            logger.Debug($"drpBooks.SelectedItem: {drpBooks.SelectedItem}");

            Book temp = (Book)bo.SelectOneObject(new Book(Convert.ToInt32(drpBooks.SelectedValue), "Z", DateTime.Now, 1, 1, "Z"));
            txtBookName.Text = temp.GetName();
            txtBookDesc.Text = temp.GetDescription();
            hdnBookId.Value = temp.GetId().ToString();

            drpGenres.SelectedValue = temp.GetGenreId().ToString();
            drpAuthors.SelectedValue = temp.GetAuthorId().ToString();

            //txtBookName.Text = drpBooks.SelectedItem.Text;
            //hdnBookId.Value = drpBooks.SelectedValue;

            btnSubmit.Text = "Edit";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logger.Debug($"drpBooks.SelectedIndex: {drpBooks.SelectedIndex}");
            logger.Debug($"drpBooks.SelectedValue: {drpBooks.SelectedValue}");
            logger.Debug($"drpBooks.SelectedItem: {drpBooks.SelectedItem}");

            try
            {
                int bookId = Convert.ToInt32(drpBooks.SelectedValue);
                Book temp = new Book(bookId, "BLANK", DateTime.Now, -1, -1, "Z");
                //////////////dao.DeleteOneObject(temp);
                bo.DeleteOneObject(temp);
                logger.Debug($"GENRE DELETED:  PK: {bookId}");
                lblMessage.Text = "Book successfully deleted";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                PopulateDataControls();
                //PopulateBookDropdown();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }



    }
}