using System;
using System.Linq;
using System.Windows.Forms;

namespace LabTest
{
    public partial class MainForm : Form
    {
        private readonly Database database;

        public MainForm()
        {
            InitializeComponent();

            database = new Database();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.database.Initialize();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                var userName = userNameTextBox.Text;

                var users = this.database.GetUsers();

                var user = users.FirstOrDefault(currentUser => currentUser.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    ShowUserNotFoundResult(userName);
                }
                else
                {
                    var password = passwordTextBox.Text;

                    var hashedPassword = PasswordHasher.ComputeSha512Hash(password);
                    if (hashedPassword.Equals(user.HashedPassword))
                    {
                        ShowSuccessAuthenticationResult();
                    }
                    else
                    {
                        ShowInvalidPasswordResult();
                    }
                }
            }
            catch (Exception exception)
            {
                ShowExceptionResult(exception);
            }
        }

        private void ShowUserNotFoundResult(string userName)
        {
            this.userNameErrorLabel.Text = $"Користувача з іменем {userName} не знайдено";
            this.userNameErrorLabel.Visible = true;
        }

        private void ShowInvalidPasswordResult()
        {
            this.passwordErrorLabel.Text = "Пароль невірний";
            this.passwordErrorLabel.Visible = true;
        }

        private void ShowSuccessAuthenticationResult()
        {
            this.userNameErrorLabel.Visible = false;
            this.passwordErrorLabel.Visible = false;

            MessageBox.Show("Авторизація успішна");
        }

        private static void ShowExceptionResult(Exception exception)
        {
            MessageBox.Show($"Помилка {exception.Message}");
        }
    }
}
