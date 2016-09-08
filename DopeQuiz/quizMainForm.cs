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
using System.Diagnostics;
using System.Net;

namespace DopeQuiz
{
    public partial class quizMainForm :MaterialForm
    {
        question[] Questions = new question[3];
        DopeQuizDbCLassessDataContext db = new DopeQuizDbCLassessDataContext();
        Random rnd = new Random();
        int curr = 0; double correct=0;
       
        public quizMainForm()
        {

            InitializeComponent();
        }

        public void GenerateQuestions()
        {
            var query = from quest in db.questions
                        select quest;
            int numR = query.Count();
            int lent = Questions.Length - 1;
            for (int i = 0; i <= lent; i++)
            {
                int position = rnd.Next(numR);
                while (Array.Exists(Questions,element=>element !=null && element.Id==position+1))
                {
                    position = rnd.Next(numR);
                }
                Questions[i] = query.ToList().ElementAt(position);
            }
           
            
        }

        public void nextQuestion()
        {
            try
            {

                materialLabel1.Text = Questions[curr].questiontext;
                materialRadioButton1.Text = Questions[curr].ans1;
                materialRadioButton2.Text = Questions[curr].ans2;
                materialRadioButton3.Text = Questions[curr].ans3;
                materialRadioButton4.Text = Questions[curr].correct_ans;
                curr++;
              

                MaterialRadioButton[] rbs = groupBox1.Controls.OfType<MaterialRadioButton>().ToArray();
                for (int i=0; i <= rbs.Length-1;i++)
                {
                    string tmp = rbs[i].Text;
                    int num = rnd.Next(rbs.Length);
                    rbs[i].Text = rbs[num].Text;
                    rbs[num].Text = tmp;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
                
            }
                
          

        }

        private void quizMainForm_Load(object sender, EventArgs e)
        {
            //Show username 
           //materialLabel1.Text= showName();
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            if (curr==0)
            {
                groupBox1.Visible = true;
                pictureBox1.Visible = false;
                btnStartQuiz.Text = "Next";

                GenerateQuestions();
                nextQuestion();
            }else if(curr<=Questions.Length){

                checkQuestion();
            }

            

      

        }

        //show username on form
        private string showName()
        {
            loginForm login = new loginForm();
            string username =login.Myusername;
            var query = from user in db.Users
                        where
                            user.username == username
                        select user;
            
            
            foreach (var person in query)
            {
                username= person.username;
            }
            return username;
        }

        private void checkQuestion()
        {
            MaterialRadioButton rb = groupBox1.Controls.OfType<MaterialRadioButton>().FirstOrDefault(element=>element.Checked);
            if (rb!=null)
            {
                if (rb.Text==Questions[curr-1].correct_ans)
                {
                    correct++;
                    MessageBox.Show("You got it right");
                }
                else
                {
                    MessageBox.Show("oops, You failed!");
                }
                //Change Button text to submit
                if (curr == Questions.Length - 1)
                {
                    btnStartQuiz.Text = "Submit";
                }

                // finalize result

                if (curr==Questions.Length)
	            {
                   
		             MessageBox.Show("Completed In a Grand style, You Scored "+Math.Floor(((correct/Questions.Length)*100))+"% on the Quiz");
	            }else
                {
                    rb.Checked = false;
                       nextQuestion();
	            }

            }else
            {
                MessageBox.Show("You Dint answer any Question");
                  checkQuestion();
            }
	            
            }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            prevQuestion();
        }
        public void prevQuestion()
        {
            // next Question
        }
            
        }
    }

