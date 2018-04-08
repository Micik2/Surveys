using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox2.Attributes["type"] = "password";
            Button1.Click += new EventHandler(this.Button_Click);
        }
        
        void Button_Click(object sender, EventArgs e)
        {
            //string login = this.Login1.UserName;
            //string haslo = this.Login1.Password;
            string login = this.TextBox1.Text;
            string haslo = this.TextBox2.Text;
            
            //byte[] hash;
            //using (MD5 md5 = MD5.Create())
            //{
            //    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(haslo));
            //}
            //string haslo2 = hash.ToString();
            //string sql1 = string.Format("SELECT COUNT(*) from Uzytkownicy where login = {0} and haslo = {1};", login, hash);
            string sql1 = string.Format("SELECT 1 from Uzytkownicy where login = '{0}' and haslo = '{1}';", login, haslo);
            string sql2 = string.Format("SELECT czy_glosowal, czy_admin from Uzytkownicy where login = '{0}' and haslo = '{1}';", login, haslo);
            //string sql3 = "CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), glosy_pierwsza INTEGER, opcja_druga VARCHAR(20), glosy_druga INTEGER, opcja_trzecia VARCHAR(20), glosy_trzecia INTEGER, aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1), nazwa VARCHAR(45));";
            string sql3 = string.Format("SELECT 1 from Ankiety where aktywna = 1");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();

            SQLiteCommand selectCheckCommand = new SQLiteCommand(sql1, m_dbConnection);
            SQLiteCommand selectUserCommand = new SQLiteCommand(sql2, m_dbConnection);
            SQLiteCommand selectAnkietaCommand = new SQLiteCommand(sql3, m_dbConnection);

            int count = Convert.ToInt32(selectCheckCommand.ExecuteScalar());
            SQLiteDataReader sqlReader = selectUserCommand.ExecuteReader();
            Int32 czy_admin = 0;
            Int32 czy_glosowal = 0;
            while (sqlReader.Read())
            {
                czy_glosowal = sqlReader.GetByte(0);
                czy_admin = sqlReader.GetByte(1);
            }
            sqlReader.Close();
            int ankieta = 0;
            ankieta  = Convert.ToInt32(selectAnkietaCommand.ExecuteScalar());

            if (count == 1 && czy_admin == 1)
            {
                SQLiteCommand sessionCommand = new SQLiteCommand(string.Format("INSERT OR REPLACE INTO Sesja (login, admin) values ('{0}', '{1}')", login, czy_admin), m_dbConnection);
                sessionCommand.ExecuteNonQuery();
                m_dbConnection.Close();
                if (ankieta == 1)
                    Response.Redirect("SurveyResults.aspx");
                else
                    Response.Redirect("AdminAccount.aspx");
            }
            else if (count == 1 && czy_admin == 0)
            {
                SQLiteCommand sessionCommand = new SQLiteCommand(string.Format("INSERT OR REPLACE INTO Sesja (login, admin) values ('{0}', '{1}')", login, czy_admin), m_dbConnection);
                sessionCommand.ExecuteNonQuery();
                m_dbConnection.Close();
                if (czy_glosowal == 0)
                    Response.Redirect("Survey.aspx");
                else if (czy_glosowal == 1)
                    Response.Redirect("UserAccount.aspx");
            }
        }
    }
}