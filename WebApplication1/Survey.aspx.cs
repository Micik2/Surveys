using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(this.Check_Click);
            LogoutButton.Click += new EventHandler(this.Logout_Click);

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();
            string sqlSession = "SELECT login from Sesja";
            SQLiteCommand sessionCommand = new SQLiteCommand(sqlSession, m_dbConnection);
            //Int32 uzytkownik = Convert.ToInt32(sessionCommand.ExecuteScalar());
            SQLiteDataReader reader = sessionCommand.ExecuteReader();
            string login = "";
            while (reader.Read())
            {
                login = reader.GetString(0);
            }
            reader.Close();

            SQLiteCommand userCommand = new SQLiteCommand(string.Format("SELECT czy_glosowal from Uzytkownicy where login = '{0}'", login), m_dbConnection);
            SQLiteDataReader sQLiteDataReader = userCommand.ExecuteReader();
            int czy_glosowal = 1;
            while (sQLiteDataReader.Read())
            {
                czy_glosowal = sQLiteDataReader.GetInt32(0);
            }
            sQLiteDataReader.Close();

            //if (uzytkownik < 1)
            if (login == "" || login == null)
                Response.Redirect("Default.aspx");
            else if (czy_glosowal == 0)
            {
                //string sql3 = "CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), glosy_pierwsza INTEGER, opcja_druga VARCHAR(20), glosy_druga INTEGER, opcja_trzecia VARCHAR(20), glosy_trzecia INTEGER, aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1), nazwa VARCHAR(45));";
                string selectSql = "SELECT opcja_pierwsza, opcja_druga, opcja_trzecia, nazwa from Ankiety where aktywna = 1";
                SQLiteCommand selectCommand = new SQLiteCommand(selectSql, m_dbConnection);
                SQLiteDataReader sqlReader = selectCommand.ExecuteReader();

                string opcjaPierwsza = "";
                string opcjaDruga = "";
                string opcjaTrzecia = "";
                string nazwaAnkiety = "";
                while (sqlReader.Read())
                {
                    opcjaPierwsza = sqlReader.GetString(0);
                    opcjaDruga = sqlReader.GetString(1);
                    opcjaTrzecia = sqlReader.GetString(2);
                    nazwaAnkiety = sqlReader.GetString(3);
                }
                CheckBoxList1.Items.Add(opcjaPierwsza);
                CheckBoxList1.Items.Add(opcjaDruga);
                CheckBoxList1.Items.Add(opcjaTrzecia);
                Label1.Text = nazwaAnkiety;
            }
            else if (czy_glosowal == 1)
                Response.Redirect("UserAccount.aspx");
        }

        void Check_Click(object sender, EventArgs e)
        {
            //CheckBoxList1.
            //CheckBoxList checklist = (CheckBoxList) FindControl("CheckBoxList1");
            string[] values = new string[3];
            //int j = 0;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    values[i] = CheckBoxList1.Items[i].Text;
                    //values[j] = checklist.Items[i].Text;
                    //j++;
                }
            }

            int glosy_pierwsza = 0;
            int glosy_druga = 0;
            int glosy_trzecia = 0;

            if (values[0] != null && values[0] != "")
                glosy_pierwsza = 1;
            if (values[1] != null && values[1] != "")
                glosy_druga = 1;
            if (values[2] != null && values[2] != "")
                glosy_trzecia = 1;

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();
                
            //string sql3 = "CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), opcja_druga VARCHAR(20), opcja_trzecia VARCHAR(20), aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1), nazwa VARCHAR(45));";
            SQLiteCommand selectAnkietyCommand = new SQLiteCommand("SELECT id, aktualna_liczba_glosow from Ankiety where aktywna = 1", m_dbConnection);
            SQLiteDataReader sqlReader = selectAnkietyCommand.ExecuteReader();
            int idAnkiety = -1;
            int aktualnaLiczbaGlosow = 0;
            while (sqlReader.Read())
            {
                idAnkiety = sqlReader.GetInt32(0);
                aktualnaLiczbaGlosow = sqlReader.GetInt32(1);
            }
            sqlReader.Close();
            //aktualnaLiczbaGlosow += glosy_pierwsza + glosy_druga + glosy_trzecia;
            aktualnaLiczbaGlosow += 1;

            //UPDATE Products SET Price = Price + 50 WHERE ProductID = 1
            SQLiteCommand updateFirstCommand = new SQLiteCommand(string.Format("UPDATE Ankiety SET glosy_pierwsza = glosy_pierwsza + '{0}' where id = '{1}'", glosy_pierwsza, idAnkiety), m_dbConnection);
            SQLiteCommand updateSecondCommand = new SQLiteCommand(string.Format("UPDATE Ankiety SET glosy_druga = glosy_druga + '{0}' where id = '{1}'", glosy_druga, idAnkiety), m_dbConnection);
            SQLiteCommand updateThirdCommand = new SQLiteCommand(string.Format("UPDATE Ankiety SET glosy_trzecia = glosy_trzecia + '{0}' where id = '{1}'", glosy_trzecia, idAnkiety), m_dbConnection);
            updateFirstCommand.ExecuteNonQuery();
            updateSecondCommand.ExecuteNonQuery();
            updateThirdCommand.ExecuteNonQuery();

            //string sql = string.Format("INSERT OR REPLACE Ankiety (id, opcja_pierwsza, opcja_druga, opcja_trzecia, aktualna_liczba_glosow, koncowa_liczba_glosow, data, aktywna) values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", idAnkiety, hash, email, tajnePytanie, tajnaOdpowiedz, 1);
            string insertAnkietySql = string.Format("UPDATE Ankiety SET aktualna_liczba_glosow = '{0}' where id = '{1}'", aktualnaLiczbaGlosow, idAnkiety);

            SQLiteCommand selectUzytkownicyCommand = new SQLiteCommand("SELECT login from Sesja", m_dbConnection);
            SQLiteDataReader sQLiteDataReader = selectUzytkownicyCommand.ExecuteReader();
            string login = "";
            while (sQLiteDataReader.Read())
            {
                login = sQLiteDataReader.GetString(0);
            }
            sQLiteDataReader.Close();

            SQLiteCommand insertAnkietyCommand = new SQLiteCommand(insertAnkietySql, m_dbConnection);
            string insertUzytkownicySql = string.Format("UPDATE Uzytkownicy SET czy_glosowal = 1 where login = '{0}'", login);
            insertAnkietyCommand.ExecuteNonQuery();
            SQLiteCommand insertUzytkownicyCommand = new SQLiteCommand(insertUzytkownicySql, m_dbConnection);
            insertUzytkownicyCommand.ExecuteNonQuery();
            new SQLiteCommand("DELETE from Sesja", m_dbConnection).ExecuteNonQuery();
            m_dbConnection.Close();
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