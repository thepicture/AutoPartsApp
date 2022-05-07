using AutoPartsApp.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AutoPartsApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class User : IDataErrorInfo
    {
        private const string EmailPattern = @"\w+@\w+\.\w{2,3}";
        private string password;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Email))
                {
                    if (string.IsNullOrWhiteSpace(Email)) return "Электронная почта обязательна";
                    if (!Regex.IsMatch(Email, EmailPattern))
                    {
                        return "Введите корректную почту";
                    }
                }
                if (columnName == nameof(LastName))
                {
                    if (string.IsNullOrWhiteSpace(LastName)) return "Фамилия обязательна";
                }
                if (columnName == nameof(FirstName))
                {
                    if (string.IsNullOrWhiteSpace(FirstName)) return "Имя обязательно";
                }
                if (columnName == nameof(UserRole))
                {
                    if (UserRole == null) return "Укажите роль";
                }
                if (columnName == nameof(Password))
                {
                    if (string.IsNullOrWhiteSpace(Password) || Password.Length < 5) return "Введите пароль от 5 символов";
                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this[nameof(Email)] != null) return false;
                if (this[nameof(LastName)] != null) return false;
                if (this[nameof(FirstName)] != null) return false;
                if (this[nameof(UserRole)] != null) return false;
                if (this[nameof(Password)] != null) return false;
                return true;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                DependencyService
                    .Get<IHashGenerator>()
                    .GenerateHash(value,
                                  out byte[] passwordHash,
                                  out byte[] passwordSalt);
                PasswordHash = passwordHash;
                Salt = passwordSalt;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}
