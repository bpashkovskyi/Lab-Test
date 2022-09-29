using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                try
                {
                string[] str = System.IO.File.ReadAllLines(path: "C:/Users/Dmytro/Desktop/t.txt");

                //System.IO.StreamReader File = new System.IO.StreamReader(path: "C:/Users/Dmytro/Desktop/t.txt");
                //List < String > log = new List<String>();
                //List < String > pas = new List<String>();
                //while (!File.EndOfStream)
                //   {
                //    log.Add(File.ReadLine());
                //    pas.Add(File.ReadLine());
                //   }

                    StringBuilder Login = new StringBuilder(textBox1.Text);
                    StringBuilder pasword = new StringBuilder(textBox2.Text);
                    for (int i = 0; i < Login.Length; i++)
                    {
                        if (!Char.IsLower(Login[i])) Login[i] = Char.ToLower(Login[i]);
                    }
                    for (int i = 0; i < pasword.Length; i++)
                    {
                        if (!Char.IsLower(pasword[i])) pasword[i] = Char.ToLower(pasword[i]);
                    }
                    if (str[0] == Login.ToString() && str[1] == pasword.ToString()) MessageBox.Show("Так!");
                }
                catch (Exception error)
                {

                }
            
        }
    }
}
