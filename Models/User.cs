namespace AutoPartsApp.Models
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
