using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PZ_18.Models.Interfaces;

namespace PZ_18.Models
{
    /// <summary>
    /// Аккаунт пользователя.
    /// </summary>
    public class Account : INotifyPropertyChanged, IAccount
    {
        private int _accountId;
        private string _fullName;
        private string _phoneNumber;
        private string _username;
        private string _userPassword;
        private int _categoryId;

        [Key]
        public int AccountID
        {
            get => _accountId;
            private set { _accountId = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(255)]
        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(50)]
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(255)]
        public string UserPassword
        {
            get => _userPassword;
            set { _userPassword = value; OnPropertyChanged(); }
        }

        [Required]
        public int CategoryID
        {
            get => _categoryId;
            set { _categoryId = value; OnPropertyChanged(); }
        }

        [ForeignKey("CategoryID")]
        [JsonIgnore]
        public virtual UserCategory UserCategory { get; private set; }

        public void ChangePassword(string newHashedPassword)
        {
            UserPassword = newHashedPassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
