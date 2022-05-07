using System.ComponentModel;

namespace AutoPartsApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class Feedback : IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Message")
                {
                    if (string.IsNullOrWhiteSpace(Message) || Message.Length < 10)
                    {
                        return "Введите сообщение от 10 символов";
                    }
                }

                if (columnName == "User1")
                {
                    if (User1 == null)
                    {
                        return "Укажите, кому отправить сообщение";
                    }
                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this[nameof(Message)] != null) return false;
                if (this[nameof(User1)] != null) return false;
                return true;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}
