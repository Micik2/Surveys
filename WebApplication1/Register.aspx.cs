using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateUserWizard1.ContinueButtonClick += new EventHandler(this.LinkButton1_Click);
            //CreateUserWizard1.NextButtonClick += new EventHandler(this.LinkButton1_Click);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            TextBox loginTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName");
            TextBox hasloTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Password");
            //TextBox hasloPotwierdzenieTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("");
            TextBox emailTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email");
            TextBox tajnePytanieTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Question");
            TextBox tajnaOdpowiedzTextBox = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Answer");
            //CheckBox pamietajCheckBox = (CheckBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("");

            string login = loginTextBox.Text;
            string haslo = hasloTextBox.Text;
            string email = emailTextBox.Text;
            string tajnePytanie = tajnePytanieTextBox.Text;
            string tajnaOdpowiedz = tajnaOdpowiedzTextBox.Text;

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=J:\\SEMESTR VII\\PIS\\AKTUALNE\\WebApplication1\\baza.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql1 = string.Format("SELECT COUNT(*) from Uzytkownicy where login = '{0}'", login);
            SQLiteCommand command = new SQLiteCommand(sql1, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            //while(reader.Read())
            //{
            Int32 count = Convert.ToInt32(command.ExecuteScalar());
            if (count != 0)
            {
                // raise error
                TextBox duplicateUsernameError = (TextBox)this.CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("DuplicateUserNameErrorMessage");
                duplicateUsernameError.Visible = true;
                m_dbConnection.Close();
            }
            else
            {
                //byte[] hash;
                //using (MD5 md5 = MD5.Create())
                //{
                //    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(haslo));
                //}
                string sql2 = string.Format("INSERT INTO Uzytkownicy (login, haslo, tajne_pytanie, tajna_odpowiedz, email, czy_glosowal, czy_admin) values ('{0}', '{1}', '{2}', '{3}', '{4}', 0, 0)", login, haslo, tajnePytanie, tajnaOdpowiedz, email);
                SQLiteCommand insertCommand = new SQLiteCommand(sql2, m_dbConnection);
                insertCommand.ExecuteNonQuery();
                m_dbConnection.Close();
                Response.Redirect("Default.aspx");
            }
        }

        

        //protected global::System.Web.UI.WebControls.Login Login1()
        //{ 
        //}
    }
}