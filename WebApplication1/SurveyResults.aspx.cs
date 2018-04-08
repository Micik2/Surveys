using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SurveyResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(this.Logout_Click);

            //string sql2 = "CREATE TABLE Sesja (login varchar(45) PRIMARY KEY, admin TINYINT(1));";
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();
            string sqlSession = "SELECT 1 from Sesja";
            SQLiteCommand sessionCommand = new SQLiteCommand(sqlSession, m_dbConnection);
            Int32 uzytkownik = Convert.ToInt32(sessionCommand.ExecuteScalar());

            if (uzytkownik > 0)
            {
                SQLiteDataReader sessionSqlReader = sessionCommand.ExecuteReader();
                int admin = 0;
                while (sessionSqlReader.Read())
                {
                    admin = sessionSqlReader.GetInt32(0);
                }

                if (admin > 0)
                {
                    Chart1.ChartAreas[0].AxisX.Minimum = 0;
                    Chart1.ChartAreas[0].AxisX.Maximum = 3;

                    string sql = "SELECT opcja_pierwsza, opcja_druga, opcja_trzecia, glosy_pierwsza, glosy_druga, glosy_trzecia, data, nazwa from Ankiety where aktywna = 1;";
                    SQLiteCommand selectResultsCommand = new SQLiteCommand(sql, m_dbConnection);
                    //"CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), glosy_pierwsza INTEGER, opcja_druga VARCHAR(20), glosy_druga INTEGER, opcja_trzecia VARCHAR(20), glosy_trzecia INTEGER, aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1), nazwa VARCHAR(45));";
                    SQLiteDataReader sqlReader = selectResultsCommand.ExecuteReader();

                    string opcjaPierwsza = "";
                    string opcjaDruga = "";
                    string opcjaTrzecia = "";
                    int glosyPierwsza = 0;
                    int glosyDruga = 0;
                    int glosyTrzecia = 0;
                    string data = "";
                    string nazwa = "";

                    while (sqlReader.Read())
                    {
                        opcjaPierwsza = sqlReader.GetString(0);
                        opcjaDruga = sqlReader.GetString(1);
                        opcjaTrzecia = sqlReader.GetString(2);
                        glosyPierwsza = sqlReader.GetInt32(3);
                        glosyDruga = sqlReader.GetInt32(4);
                        glosyTrzecia = sqlReader.GetInt32(5);
                        data = sqlReader.GetString(6);
                        nazwa = sqlReader.GetString(7);
                    }
                    sqlReader.Close();
                    m_dbConnection.Close();

                    Series S1 = new Series();
                    Title title = new Title();
                    title.Name = nazwa;
                    title.Text = nazwa + "\n" + data;
                    S1.Points.AddXY(1, glosyPierwsza);
                    S1.Points.AddXY(2, glosyDruga);
                    S1.Points.AddXY(3, glosyTrzecia);
                    ListBox1.Items.Add("1: " + opcjaPierwsza);
                    ListBox1.Items.Add("2: " + opcjaDruga);
                    ListBox1.Items.Add("3: " + opcjaTrzecia);
                    Chart1.Series.Add(S1);
                    Chart1.Titles.Add(title);
                }
                else
                    Response.Redirect("UserAccount.aspx");
            }
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