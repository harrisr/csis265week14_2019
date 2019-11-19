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

namespace csis265week11
{
    public partial class Library : System.Web.UI.Page
    {
        private static readonly ILog logger = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected string connString;
        protected SqlConnection conn;
        protected SqlCommand cmd;
        protected SqlDataReader rdr;
        protected SqlDataAdapter adpt;
        protected string sql;



        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            connString = ConfigurationManager.ConnectionStrings["localhost"].ConnectionString;


            //  this is the fancy-schmancy STRING-INTERPOLATION way...
            logger.Debug($"CONN STRING: {connString}");

            logger.Debug("YYYYYYYEEEEEEEEEEEEEE  HHHHHAAAAWWWWW  !!!!!!!");

            PopulateGenreDropdown();

            /*
            TestConnectionString();
            //TestInsert();
            TestSelect();
            TestUpdate();
            TestSelect();
            TestDelete();
            TestSelect();
            */
        }

        private void PopulateGenreDropdown()
        {
            GenreDAO dao = new GenreDAO("localhost");
            IList<object> genres = dao.SelectManyObjects(new Genre(-1, "%", DateTime.Now));

            drpGenres.DataSource = genres;
            drpGenres.DataValueField = "Id";
            drpGenres.DataTextField = "Name";
            drpGenres.DataBind();

            //throw new NotImplementedException();
        }

        private void TestConnectionString()
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                sql = "SELECT COUNT(*) FROM GENRE;";
                cmd = new SqlCommand(sql, conn);

                string strCount = cmd.ExecuteScalar().ToString();
                int count = Convert.ToInt32(strCount);
                Response.Write("<br/><br/>" + count);

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write("<br/><br/>" + ex.Message);
                Response.Write("<br/><br/>" + ex.StackTrace);
                logger.Error(ex);
            }

            //throw new NotImplementedException();
        }


        private void TestSelect()
        {
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                sql = "SELECT ID, NAME, DATE_CREATED FROM GENRE;";
                cmd = new SqlCommand(sql, conn);

                rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Response.Write("<br/><br/>" + rdr["Id"] +
                        "<br/>" + rdr["Name"] +
                        "<br/>" + rdr["Date_Created"]
                        );
                }
                rdr.Close();

                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write("<br/><br/>" + ex.Message);
                Response.Write("<br/><br/>" + ex.StackTrace);
                logger.Error(ex);
            }

            //throw new NotImplementedException();
        }



        private void TestInsert()
        {
            try
            {
                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = "INSERT INTO GENRE (Name) VALUES ('Biography') ;";

                conn.Open();
                adpt.InsertCommand = new SqlCommand(sql, conn);
                adpt.InsertCommand.ExecuteNonQuery();
                
                adpt.Dispose();                
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write("<br/><br/>" + ex.Message);
                Response.Write("<br/><br/>" + ex.StackTrace);
                logger.Error(ex);
            }

        }






        private void TestUpdate()
        {
            try
            {
                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = "UPDATE GENRE SET Name = 'Aardvark' WHERE Id = 1 ;";

                conn.Open();
                adpt.UpdateCommand = new SqlCommand(sql, conn);
                adpt.UpdateCommand.ExecuteNonQuery();

                adpt.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write("<br/><br/>" + ex.Message);
                Response.Write("<br/><br/>" + ex.StackTrace);
                logger.Error(ex);
            }

        }



        private void TestDelete()
        {
            try
            {
                conn = new SqlConnection(connString);
                adpt = new SqlDataAdapter();

                sql = "DELETE FROM GENRE  WHERE Id = 1 ;";

                conn.Open();
                adpt.DeleteCommand = new SqlCommand(sql, conn);
                adpt.DeleteCommand.ExecuteNonQuery();

                adpt.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write("<br/><br/>" + ex.Message);
                Response.Write("<br/><br/>" + ex.StackTrace);
                logger.Error(ex);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                string genreName = txtGenre.Text;
                Genre temp = new Genre(-1, genreName, DateTime.Now);
                lblMessage.Text = temp.ToString();
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
    }
}