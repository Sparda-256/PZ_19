using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PZ_18.Models
{
    /// <summary>
    /// Категория бытовой техники.
    /// </summary>
    public class ApplianceCategory : INotifyPropertyChanged
    {
        private int _applianceID;
        private string _applianceName;

        [Key]
        public int ApplianceID
        {
            get => _applianceID;
            set { _applianceID = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(255)]
        public string ApplianceName
        {
            get => _applianceName;
            set { _applianceName = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}