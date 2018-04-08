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
    public partial class AdminAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*string sql1 = string.Format("SELECT * from Uzytkownicy where login = {0} and haslo = {1};", login, hash);
            string sql2 = string.Format("SELECT czy_admin from Uzytkownicy where login = {0} and haslo = {1};", login, hash);

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=baza.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand selectCheckCommand = new SQLiteCommand(sql1, m_dbConnection);
            SQLiteCommand selectUserCommand = new SQLiteCommand(sql2, m_dbConnection);

            Int32 count = (Int32)selectCheckCommand.ExecuteScalar();
            SQLiteDataReader sqlReader = selectUserCommand.ExecuteReader();
            Int32 czy_admin = 0;
            while (sqlReader.Read())
            {
                czy_admin = sqlReader.GetByte(0);
            }
            sqlReader.Close();*/
            LogoutButton.Click += new EventHandler(this.Logout_Click);
            Button1.Click += new EventHandler(this.Button1_Click);

            string sql = string.Format("SELECT * from Ankiety where aktywna = 1");
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand selectAnkietaCommand = new SQLiteCommand(sql, m_dbConnection);
            Int32 count = Convert.ToInt32(selectAnkietaCommand.ExecuteScalar());

            if (count > 0)
                Response.Redirect("SurveyResults.aspx");
            //Server.Transfer("SurveyResults.aspx", true);
                
            
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            //Button clickedButton = (Button) sender;
            string nazwaAnkiety = TextBox1.Text;
            string opcja1 = TextBox2.Text;
            string opcja2 = TextBox3.Text;
            string opcja3 = TextBox4.Text;
            string data = Calendar1.SelectedDate.ToShortDateString();

            //string sql3 = "CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), glosy_pierwsza INTEGER, opcja_druga VARCHAR(20), glosy_druga INTEGER, opcja_trzecia VARCHAR(20), glosy_trzecia INTEGER, aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1));";
            string insertSql = string.Format("INSERT INTO Ankiety (opcja_pierwsza, glosy_pierwsza, opcja_druga, glosy_druga, opcja_trzecia, glosy_trzecia, aktualna_liczba_glosow, koncowa_liczba_glosow, data, aktywna, nazwa) values ('{0}', 0, '{1}', 0, '{2}', 0, 0, 10, '{3}', 1, '{4}')", opcja1, opcja2, opcja3, data, nazwaAnkiety);
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand insertCommand = new SQLiteCommand(insertSql, m_dbConnection);
            insertCommand.ExecuteNonQuery();
            m_dbConnection.Close();

            Response.Redirect("SurveyResults.aspx");
            //Server.Transfer("Survey.aspx", true);
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