using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace PZ_18.Models
{
    /// <summary>
    /// Заметка к заявке.
    /// </summary>
    public class Note : INotifyPropertyChanged
    {
        private int _noteID;
        private string _noteText;
        private int? _technicianID;
        private int? _requestID;

        [Key]
        public int NoteID
        {
            get => _noteID;
            set { _noteID = value; OnPropertyChanged(); }
        }

        [Required]
        public string NoteText
        {
            get => _noteText;
            set { _noteText = value; OnPropertyChanged(); }
        }

        public int? TechnicianID
        {
            get => _technicianID;
            set { _technicianID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual Account Technician { get; private set; }

        public int? RequestID
        {
            get => _requestID;
            set { _requestID = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual RepairRequest Request { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}