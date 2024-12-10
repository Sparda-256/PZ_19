using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace PZ_18.Models
{
    /// <summary>
    /// Компонент для ремонта.
    /// </summary>
    public class RepairComponent : INotifyPropertyChanged
    {
        private int _componentID;
        private int _requestID;
        private string _componentName;
        private int _componentQuantity;

        [Key]
        public int ComponentID
        {
            get => _componentID;
            set { _componentID = value; OnPropertyChanged(); }
        }

        [Required]
        public int RequestID
        {
            get => _requestID;
            set { _requestID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual RepairRequest RepairRequest { get; private set; }

        [Required]
        [MaxLength(255)]
        public string ComponentName
        {
            get => _componentName;
            set { _componentName = value; OnPropertyChanged(); }
        }

        public int ComponentQuantity
        {
            get => _componentQuantity;
            set { _componentQuantity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}