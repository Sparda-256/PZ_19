using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PZ_18.Models
{
    /// <summary>
    /// Категория пользователя (роль).
    /// </summary>
    public class UserCategory : INotifyPropertyChanged
    {
        private int _categoryId;
        private string _categoryName;

        [Key]
        public int CategoryID
        {
            get => _categoryId;
            set { _categoryId = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(50)]
        public string CategoryName
        {
            get => _categoryName;
            set { _categoryName = value; OnPropertyChanged(); }
        }

        public override string ToString() => CategoryName;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
