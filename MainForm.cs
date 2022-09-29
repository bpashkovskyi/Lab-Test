using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var users = new List<User>
            {
                new User { UserName = "User1", Password = "Password1" },
                new User { UserName = "User2", Password = "Password2" }
            };

            var databaseLines = new List<string>();
            foreach (var user in users)
            {
                var hashedPassword = ComputeSha512Hash(user.Password);
                var databaseLine = $"{user.UserName},{hashedPassword}";

                databaseLines.Add(databaseLine);
            }

            System.IO.File.WriteAllLines(path: "D://test.txt", databaseLines);

        }

        private static string ComputeSha512Hash(string plainPassword)
        {
            var plainPasswordBytes = Encoding.ASCII.GetBytes(plainPassword);

            var shaM = new SHA512CryptoServiceProvider();
            var hashBytes = shaM.ComputeHash(plainPasswordBytes);

            var hash = Encoding.ASCII.GetString(hashBytes);

            return hash;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var fileLines = System.IO.File.ReadAllLines(path: "D://test.txt");

                var databaseUsers = new List<User>();

                foreach (var fileLine in fileLines)
                {
                    var lineData = fileLine.Split(',');
                    var user = new User
                    {
                        UserName = lineData[0],
                        Password = lineData[1],
                    };

                    databaseUsers.Add(user);
                }

                var userName = userNameTextBox.Text;

                var databaseUser = databaseUsers.FirstOrDefault(user =>
                    user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (databaseUser == null)
                {
                    this.userNameErrorLabel.Text = $"Користувача з іменем {userName} не знайдено";
                    this.userNameErrorLabel.Visible = true;
                }
                else
                {
                    var hashedPassword = ComputeSha512Hash(passwordTextBox.Text);

                    if (hashedPassword.Equals(databaseUser.Password))
                    {

                        this.userNameErrorLabel.Visible = false;
                        this.passwordErrorLabel.Visible = false;

                        MessageBox.Show("Авторизація успішна");
                    }
                    else
                    {
                        this.passwordErrorLabel.Text = "Пароль невірний";
                        this.passwordErrorLabel.Visible = true;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Помилка {exception.Message}");
            }
        }
    }
}
