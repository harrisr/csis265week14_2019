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
    public partial class Genres : System.Web.UI.Page
    {
        private static readonly ILog logger = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ///////////////private GenreDAO dao;
        private GenreBO bo;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            ///////////////dao = new GenreDAO("localhost");
            bo = new GenreBO("localhost");
            
            if (Page.IsPostBack)
            {

            }
            else
            {
                PopulateGenreDropdown();
            }
        }

        private void PopulateGenreDropdown()
        {
            ///////IList<object> genres = dao.SelectManyObjects(new Genre(-1, "%", DateTime.Now));
            IList<object> genres = bo.SelectManyObjects(new Genre(-1, "%", DateTime.Now));

            drpGenres.DataSource = genres;
            drpGenres.DataValueField = "Id";
            drpGenres.DataTextField = "Name";
            drpGenres.DataBind();
        }
                     
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                string genreName = txtGenre.Text;
                int genreId;
                Genre temp;

                if (btnSubmit.Text.ToUpper().Equals("EDIT"))
                {
                    ////////   DOING AN UPDATE
                    genreId = Convert.ToInt32(hdnGenreId.Value);
                    temp = new Genre(genreId, genreName, DateTime.Now);
                    //////////////dao.UpdateOneObject(temp);
                    bo.UpdateOneObject(temp);
                    lblMessage.Text = "Genre successfully edited";
                    btnSubmit.Text = "Add";
                }
                else
                {
                    ////////   DOING AN INSERT
                    temp = new Genre(-1, genreName, DateTime.Now);
                    /////////////temp = (Genre)dao.InsertOneObject(temp);
                    temp = (Genre)bo.InsertOneObject(temp);
                    lblMessage.Text = "Genre successfully added";
                }
                PopulateGenreDropdown();
                hdnGenreId.Value = string.Empty;
                txtGenre.Text = string.Empty;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                logger.Debug($"GENRE: {temp.ToString()}");
            }
            catch (LibraryException lex)
            {
                lblMessage.Text = lex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                logger.Error(lex);
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                logger.Error(ex);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            logger.Debug($"drpGenres.SelectedIndex: {drpGenres.SelectedIndex}");
            logger.Debug($"drpGenres.SelectedValue: {drpGenres.SelectedValue}");
            logger.Debug($"drpGenres.SelectedItem: {drpGenres.SelectedItem}");

            txtGenre.Text = drpGenres.SelectedItem.Text;
            hdnGenreId.Value = drpGenres.SelectedValue;
            btnSubmit.Text = "Edit";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logger.Debug($"drpGenres.SelectedIndex: {drpGenres.SelectedIndex}");
            logger.Debug($"drpGenres.SelectedValue: {drpGenres.SelectedValue}");
            logger.Debug($"drpGenres.SelectedItem: {drpGenres.SelectedItem}");

            try
            {
                int genreId = Convert.ToInt32(drpGenres.SelectedValue);
                Genre temp = new Genre(genreId, "BLANK", DateTime.Now);
                //////////////dao.DeleteOneObject(temp);
                bo.DeleteOneObject(temp);
                logger.Debug($"GENRE DELETED:  PK: {genreId}");
                lblMessage.Text = "Genre successfully deleted";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                PopulateGenreDropdown();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }



    }
}