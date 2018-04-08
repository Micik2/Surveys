using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;


namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!File.Exists("J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite")) {
                SQLiteConnection.CreateFile("J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite");
                SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
                m_dbConnection.Open();
                string sql1 = "CREATE TABLE Uzytkownicy (id INTEGER PRIMARY KEY AUTOINCREMENT, login VARCHAR(20), haslo VARCHAR(20), tajne_pytanie VARCHAR(45), tajna_odpowiedz VARCHAR(45), email VARCHAR(45), czy_glosowal INTEGER, czy_admin INTEGER);";
                string sql2 = "CREATE TABLE Sesja (login varchar(45) PRIMARY KEY, admin TINYINT(1));";
                string sql3 = "CREATE TABLE Ankiety (id INTEGER PRIMARY KEY AUTOINCREMENT, opcja_pierwsza VARCHAR(20), glosy_pierwsza INTEGER, opcja_druga VARCHAR(20), glosy_druga INTEGER, opcja_trzecia VARCHAR(20), glosy_trzecia INTEGER, aktualna_liczba_glosow INTEGER, koncowa_liczba_glosow INTEGER, data VARCHAR(10), aktywna TINYINT(1), nazwa VARCHAR(45));";
                //string sql4 = "CREATE TABLE Administratorzy (login VARCHAR(45), haslo VARCHAR(45));";
                SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                command1.ExecuteNonQuery();
                SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
                command2.ExecuteNonQuery();
                SQLiteCommand command3 = new SQLiteCommand(sql3, m_dbConnection);
                command3.ExecuteNonQuery();
                //SQLiteCommand command4 = new SQLiteCommand(sql4, m_dbConnection);
                //command4.ExecuteNonQuery();
                string main_admin_password = "password123";
                //byte[] hash;
                //using (MD5 md5 = MD5.Create())
                //{
                //    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(main_admin_password));
                //}
                //string haslo = hash.ToString();
                //string sql5 = string.Format("INSERT INTO Administratorzy (login, haslo) values ('admin', {0});", hash);
                string sql5 = string.Format("INSERT INTO Uzytkownicy (login, haslo, tajne_pytanie, tajna_odpowiedz, email, czy_glosowal, czy_admin) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", "admin", main_admin_password, "brak", "brak", "brak", 1, 1);
                SQLiteCommand command5 = new SQLiteCommand(sql5, m_dbConnection);
                command5.ExecuteNonQuery();
                m_dbConnection.Close();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //LinkButton1;
        }
    }
}