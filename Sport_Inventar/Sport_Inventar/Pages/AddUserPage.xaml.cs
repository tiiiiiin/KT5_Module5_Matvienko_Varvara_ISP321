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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            GenderComboBox.ItemsSource = Data.text1Entities.GetContext().Gender.ToList();
            RoleComboBox.ItemsSource = Data.text1Entities.GetContext().Role.ToList();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AdminPage());
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(LastNameTextBox.Text))
                {
                    errors.AppendLine("заполните фамилию");
                }
                if (string.IsNullOrEmpty(FirstNameTextBox.Text))
                {
                    errors.AppendLine("заполните имя");
                }
                if (string.IsNullOrEmpty(PatronymicNameTextBox.Text))
                {
                    errors.AppendLine("заполните отчество");
                }

                if (string.IsNullOrEmpty(EmailTextBox.Text))
                {
                    errors.AppendLine("заполните email");
                }
                

                if (string.IsNullOrEmpty(PhoneMubTextBox.Text))
                {
                    errors.AppendLine("заполните номер телефона");
                }

                string phonenum =@"^\+\d{1}\(\{3}\)\-\d{3}\-\d{2}\-\d{2}\$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(PhoneMubTextBox.Text, phonenum))
                {
                    errors.AppendLine("заполните номер телефона по макету +#(###)-###-##-##");
                }
                

                if (string.IsNullOrEmpty(LoginTextBox.Text))
                {
                    errors.AppendLine("заполните login");
                }



                if (string.IsNullOrEmpty(PasswordBox.Password))
                {
                    errors.AppendLine("заполните пароль");
                }

                if (string.IsNullOrEmpty(RePasswordBox.Password))
                {
                    errors.AppendLine("заполните повторение пароля");
                }

                if(RePasswordBox.Password != PasswordBox.Password)
                {
                    errors.AppendLine("пароли не совпадают");
                }
               
                if(RoleComboBox.SelectedItem == null)
                {
                    errors.AppendLine("выберите роль");
                }
                if (GenderComboBox.SelectedItem == null)
                {
                    errors.AppendLine("выберите роль");
                }
                if(DatePicker.SelectedDate == null)
                {
                    errors.AppendLine("укажите свою дату рождения");
                }
                
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if(Data.text1Entities.GetContext().User.Any(d => d.UserEmail == EmailTextBox.Text))
                {
                    MessageBox.Show("email совпадает с данными в бд, попробуйте новый", "ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (Data.text1Entities.GetContext().User.Any(d => d.UserLogin == LoginTextBox.Text))
                {
                    MessageBox.Show("login совпадает с данными в бд, попробуйте новый", "ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var Gender = GenderComboBox.SelectedItem as Data.Gender;
                var Role= RoleComboBox.SelectedItem as Data.Role;

                var newUser = new Data.User

                {

                    LastName = LastNameTextBox.Text,
                    FirstName = FirstNameTextBox.Text,
                    PatronymicName = PatronymicNameTextBox.Text,
                    UserEmail = EmailTextBox.Text,
                    UserLogin = LoginTextBox.Text,
                    UserPassword = PasswordBox.Password,
                    UserPhoneNum = PhoneMubTextBox.Text,
                    IdGender = Gender.Id,
                    IdRole = Role.Id,

                    DateOfBirth = DatePicker.SelectedDate.Value.Date


                };
                Data.text1Entities.GetContext().User.Add(newUser);
                Data.text1Entities.GetContext().SaveChanges();

                MessageBox.Show("пользователь успешно зарегистрирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
