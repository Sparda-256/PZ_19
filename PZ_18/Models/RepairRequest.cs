using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PZ_18.Models.Interfaces;

namespace PZ_18.Models
{
    /// <summary>
    /// Заявка на ремонт.
    /// </summary>
    public class RepairRequest : INotifyPropertyChanged, IRepairRequest
    {
        private int _requestId;
        private DateTime _creationDate;
        private int _applianceId;
        private string _applianceModel;
        private string _issueDescription;
        private string _status;
        private DateTime? _resolutionDate;
        private int? _technicianId;
        private int? _customerId;
        private string _customerName;
        private string _customerPhone;

        [Key]
        public int RequestID
        {
            get => _requestId;
            private set { _requestId = value; OnPropertyChanged(); }
        }

        [Required]
        public DateTime CreationDate
        {
            get => _creationDate;
            set { _creationDate = value; OnPropertyChanged(); }
        }

        [Required]
        public int ApplianceID
        {
            get => _applianceId;
            set { _applianceId = value; OnPropertyChanged(); }
        }

        [ForeignKey("ApplianceID")]
        [JsonIgnore]
        public virtual ApplianceCategory ApplianceCategory { get; private set; }

        public string ApplianceName => ApplianceCategory?.ApplianceName ?? "Не указано";

        [Required]
        [MaxLength(255)]
        public string ApplianceModel
        {
            get => _applianceModel;
            set { _applianceModel = value; OnPropertyChanged(); }
        }

        [Required]
        public string IssueDescription
        {
            get => _issueDescription;
            set { _issueDescription = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(50)]
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public DateTime? ResolutionDate
        {
            get => _resolutionDate;
            set { _resolutionDate = value; OnPropertyChanged(); }
        }

        public int? TechnicianID
        {
            get => _technicianId;
            set { _technicianId = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual Account Technician { get; private set; }

        public int? CustomerID
        {
            get => _customerId;
            set { _customerId = value; OnPropertyChanged(); }
        }

        [JsonIgnore]
        public virtual Account Customer { get; private set; }

        [Required]
        [MaxLength(255)]
        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged(); }
        }

        [Required]
        [MaxLength(15)]
        public string CustomerPhone
        {
            get => _customerPhone;
            set { _customerPhone = value; OnPropertyChanged(); }
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            if (newStatus == "Готова к выдаче")
            {
                ResolutionDate = DateTime.Now;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
