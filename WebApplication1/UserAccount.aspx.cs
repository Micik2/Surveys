using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class UserAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(this.Logout_Click);

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();
            string sqlSession = "SELECT 1 from Sesja";
            SQLiteCommand sessionCommand = new SQLiteCommand(sqlSession, m_dbConnection);
            Int32 uzytkownik = Convert.ToInt32(sessionCommand.ExecuteScalar());

            if (uzytkownik > 0)
                Label1.Text = "Witaj użytkowniku.\nNie możesz zagłosować ponownie w tej ankiecie!";
            else
                Response.Redirect("Default.aspx");

        }

        void Logout_Click(object sender, System.EventArgs e)
        {
            //Button clickedButton = (Button) sender;
            string deleteSql = string.Format("DELETE from Sesja");
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand deleteCommand = new SQLiteCommand(deleteSql, m_dbConnection);
            deleteCommand.ExecuteNonQuery();
            m_dbConnection.Close();
            Response.Redirect("Default.aspx");
        }
    }
}