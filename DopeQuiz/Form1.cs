using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin.Animations;

namespace DopeQuiz
{
    public partial class loginForm : MaterialForm
    {

        //properties 
        public string Myusername { get; set; }
        public string Mypassword { get; set; }
        DopeQuizDbCLassessDataContext db = new DopeQuizDbCLassessDataContext();

        public loginForm()
        {
            InitializeComponent();
            this.txtUserName.BackColor = Color.FromArgb(255, 224, 192);
            this.txtPassword.BackColor = Color.FromArgb(255, 224, 192);


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // this method calls the verifylogin method and check if username and password is in the database
            bool users = verifyLogin();

            if (users==true)
            {
                quizMainForm quizmain = new quizMainForm();
                this.Hide();
                quizmain.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("You Entered an invalid password");
            }
            
        }

        
        private bool verifyLogin()
        {
            Myusername = txtUserName.Text;
            Mypassword = txtPassword.Text;

            var query = from usertable in db.Users
                        where
                            Myusername == usertable.username && Mypassword == usertable.password
                        select usertable;
            int result = query.Count<User>();

            if (result==1)
            {
                return true;
            }
            else
            {
                return false;
            }
            


        }
    }
}
