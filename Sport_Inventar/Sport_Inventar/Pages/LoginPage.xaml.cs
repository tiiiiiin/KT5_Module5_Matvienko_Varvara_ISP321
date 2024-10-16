using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sport_Inventar.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(LoginTexBox.Text))
                {
                    errors.AppendLine("заполните логин");
                }
                if (string.IsNullOrEmpty(PasswordBox.Password))
                {
                    errors.AppendLine("заполните пароль");
                }
                if(errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                if(Data.text1Entities.GetContext().User.Any(d => d.UserLogin == LoginTexBox.Text && d.UserPassword == PasswordBox.Password))
                {
                    var user = Data.text1Entities.GetContext().User.Where(d => d.UserLogin == LoginTexBox.Text && d.UserPassword == PasswordBox.Password).FirstOrDefault();
                    Classes.Manager.CurrentUser = user;
                    switch (user.Role.Name)
                    {
                        case "Администратор":
                            Classes.Manager.MainFrame.Navigate(new Pages.AdminPage());
                            break;
                        case "Исполнитель":
                            Classes.Manager.MainFrame.Navigate(new Pages.UserViewPage());
                            break;
                        case "Менеджер":
                            Classes.Manager.MainFrame.Navigate(new Pages.UserViewPage());
                            break;
                    }
                    MessageBox.Show("Успех", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
