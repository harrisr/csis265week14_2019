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
    public partial class Authors : System.Web.UI.Page
    {
        private static readonly ILog logger =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ///////////////private AuthorDAO dao;
        private AuthorBO bo;



        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            ///////////////dao = new AuthorDAO("localhost");
            bo = new AuthorBO("localhost");

            if (Page.IsPostBack)
            {

            }
            else
            {
                PopulateDatControls();
                //PopulateAuthorDropdown();
                //PopulateAuthorRepeater();
                //PopulateAuthorGrid();
            }
        }

        private void PopulateDatControls()
        {
            PopulateAuthorDropdown();
            PopulateAuthorRepeater();
            PopulateAuthorGrid();
        }

        private void PopulateAuthorDropdown()
        {
            ///////IList<object> authors = dao.SelectManyObjects(new Author(-1, "%", DateTime.Now));
            IList<object> authors = bo.SelectManyObjects(new Author(-1, "%", "BLANK", DateTime.Now));

            drpAuthors.DataSource = authors;
            drpAuthors.DataValueField = "Id";
            drpAuthors.DataTextField = "Name";
            drpAuthors.DataBind();
        }



        private void PopulateAuthorRepeater()
        {
            ///////IList<object> authors = dao.SelectManyObjects(new Author(-1, "%", DateTime.Now));
            IList<object> authors = bo.SelectManyObjects(new Author(-1, "%", "BLANK", DateTime.Now));

            rptAuthors.DataSource = authors;
            rptAuthors.DataBind();
        }

        private void PopulateAuthorGrid()
        {
            ///////IList<object> authors = dao.SelectManyObjects(new Author(-1, "%", DateTime.Now));
            IList<object> authors = bo.SelectManyObjects(new Author(-1, "%", "BLANK", DateTime.Now));

            grdAuthors.DataSource = authors;
            grdAuthors.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                string authorName = txtAuthor.Text;
                string authorEmail = txtEmail.Text;
                int authorId;
                Author temp;

                if (btnSubmit.Text.ToUpper().Equals("EDIT"))
                {
                    ////////   DOING AN UPDATE
                    authorId = Convert.ToInt32(hdnAuthorId.Value);
                    temp = new Author(authorId, authorName, authorEmail, DateTime.Now);
                    //////////////dao.UpdateOneObject(temp);
                    bo.UpdateOneObject(temp);
                    lblMessage.Text = "Author successfully edited";
                    btnSubmit.Text = "Add";
                }
                else
                {
                    ////////   DOING AN INSERT
                    temp = new Author(-1, authorName, authorEmail, DateTime.Now);
                    /////////////temp = (Author)dao.InsertOneObject(temp);
                    temp = (Author)bo.InsertOneObject(temp);
                    lblMessage.Text = "Author successfully added";
                }
                PopulateDatControls();
                //PopulateAuthorDropdown();
                //PopulateAuthorRepeater();
                hdnAuthorId.Value = string.Empty;
                txtAuthor.Text = string.Empty;
                txtEmail.Text = string.Empty;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                logger.Debug($"AUTHOR: {temp.ToString()}");
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
            logger.Debug($"drpAuthors.SelectedIndex: {drpAuthors.SelectedIndex}");
            logger.Debug($"drpAuthors.SelectedValue: {drpAuthors.SelectedValue}");
            logger.Debug($"drpAuthors.SelectedItem: {drpAuthors.SelectedItem}");


            int authorId = Convert.ToInt32(drpAuthors.SelectedValue);
            Author filter = new Author(authorId, "Z", "Z", DateTime.Now);

            Author temp = (Author)bo.SelectOneObject(filter);

            txtAuthor.Text = temp.Name;
            txtEmail.Text = temp.GetEmailAddress();

            hdnAuthorId.Value = drpAuthors.SelectedValue;

            btnSubmit.Text = "Edit";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logger.Debug($"drpAuthors.SelectedIndex: {drpAuthors.SelectedIndex}");
            logger.Debug($"drpAuthors.SelectedValue: {drpAuthors.SelectedValue}");
            logger.Debug($"drpAuthors.SelectedItem: {drpAuthors.SelectedItem}");

            try
            {
                int authorId = Convert.ToInt32(drpAuthors.SelectedValue);
                Author temp = new Author(authorId, "BLANK", "BLANK", DateTime.Now);
                //////////////dao.DeleteOneObject(temp);
                bo.DeleteOneObject(temp);
                logger.Debug($"AUTHOR DELETED:  PK: {authorId}");
                lblMessage.Text = "Author successfully deleted";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                PopulateDatControls();
                //PopulateAuthorDropdown();
                //PopulateAuthorRepeater();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }


        protected void btnREdit_Click(object sender, EventArgs e)
        {
            logger.Debug($"FROM REPEATER:  CLICKED  EDIT !!!!");

            Button btnEdit = (Button)sender;
            logger.Debug($"BUTTON:  {btnEdit.ID}");

            RepeaterItem item = (RepeaterItem)btnEdit.NamingContainer;
            Label idLabel =(Label)item.FindControl("lblID");
            Label nameLabel = (Label)item.FindControl("lblName");

            logger.Debug($"ID LABEL:  {idLabel.Text}");
            logger.Debug($"NAME LABEL:  {nameLabel.Text}");


            int authorId = Convert.ToInt32(idLabel.Text);
            Author filter = new Author(authorId, "Z", "Z", DateTime.Now);

            Author temp = (Author)bo.SelectOneObject(filter);

            txtAuthor.Text = temp.Name;
            txtEmail.Text = temp.GetEmailAddress();

            hdnAuthorId.Value = idLabel.Text;

            btnSubmit.Text = "Edit";
        }

        protected void btnRDelete_Click(object sender, EventArgs e)
        {
            logger.Debug($"FROM REPEATER:  CLICKED  DELETE !!!!");

            Button btnDelete = (Button)sender;
            logger.Debug($"BUTTON:  {btnDelete.ID}");

            RepeaterItem item = (RepeaterItem)btnDelete.NamingContainer;
            Label idLabel = (Label)item.FindControl("lblID");
            Label nameLabel = (Label)item.FindControl("lblName");

            logger.Debug($"ID LABEL:  {idLabel.Text}");
            logger.Debug($"NAME LABEL:  {nameLabel.Text}");
            
            try
            {
                int authorId = Convert.ToInt32(idLabel.Text);
                Author temp = new Author(authorId, "BLANK", "BLANK", DateTime.Now);
                //////////////dao.DeleteOneObject(temp);
                bo.DeleteOneObject(temp);
                logger.Debug($"AUTHOR DELETED:  PK: {authorId}");
                lblMessage.Text = "Author successfully deleted";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                PopulateDatControls();
                //PopulateAuthorDropdown();
                //PopulateAuthorRepeater();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
               
        protected void grdAuthors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            logger.Debug($"grdAuthors_RowEditing");
            logger.Debug($"GRID INDEX:  SND: {sender}");
            logger.Debug($"GRID EVT:  E: {e.NewEditIndex}");
            grdAuthors.EditIndex = e.NewEditIndex;
            PopulateDatControls();
            //PopulateAuthorGrid();
        }

        protected void grdAuthors_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            logger.Debug($"grdAuthors_RowUpdating");
            logger.Debug($"GRID INDEX:  SND: {sender}");
            logger.Debug($"GRID EVT:  E: {e.RowIndex}");
            logger.Debug($"GRID CNX:  E: {e.Cancel}");


            GridViewRow row = (GridViewRow)grdAuthors.Rows[e.RowIndex];

            Label lblID = (Label)row.FindControl("lblID");
            TextBox txtId = (TextBox)row.Cells[1].Controls[0];
            TextBox txtName = (TextBox)row.Cells[2].Controls[0];
            TextBox txtEmail = (TextBox)row.Cells[3].Controls[0];

            logger.Debug($"ID VAL: {lblID.Text}");
            logger.Debug($"ID VAL: {txtId.Text}");
            logger.Debug($"NM VAL: {txtName.Text}");
            logger.Debug($"EML VAL: {txtEmail.Text}");

            Author temp = new Author(Convert.ToInt32(lblID.Text),
                txtName.Text, txtEmail.Text, DateTime.Now);
            logger.Debug($"UPDATING TEMP: {temp}");
            bo.UpdateOneObject(temp);

            grdAuthors.EditIndex = -1;
            PopulateDatControls();
            //PopulateAuthorGrid();
        }

        protected void grdAuthors_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            logger.Debug($"grdAuthors_RowCancelingEdit");
            logger.Debug($"GRID INDEX:  SND: {sender}");
            logger.Debug($"GRID EVT:  E: {e.RowIndex}");
            logger.Debug($"GRID CNX:  E: {e.Cancel}");
            grdAuthors.EditIndex = -1;
            PopulateDatControls();
            //PopulateAuthorGrid();
        }

        protected void grdAuthors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            logger.Debug($"grdAuthors_RowDeleting");
            logger.Debug($"GRID INDEX:  SND: {sender}");
            logger.Debug($"GRID EVT:  IDX: {e.RowIndex}");
            logger.Debug($"GRID EVT:  VALS: {e.Values}");

            GridViewRow row = (GridViewRow)grdAuthors.Rows[e.RowIndex];

            ////Label lblID = (Label)row.FindControl("lblID");
            ////TextBox txtId = (TextBox)row.Cells[1].Controls[0];
            string sId = row.Cells[1].Text;

            logger.Debug($"ID VAL: {sId}");
            ////logger.Debug($"ID VAL: {lblID.Text}");
            ////logger.Debug($"ID VAL: {txtId.Text}");

            //Author temp = new Author(Convert.ToInt32(lblID.Text),"BLANK", "BLANK", DateTime.Now);
            Author temp = new Author(Convert.ToInt32(sId), "BLANK", "BLANK", DateTime.Now);
            logger.Debug($"DELETING TEMP: {temp}");
            bo.DeleteOneObject(temp);
            PopulateDatControls();
        }
    }

}