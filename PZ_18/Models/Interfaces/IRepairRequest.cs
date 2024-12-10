using System;

namespace PZ_18.Models.Interfaces
{
    /// <summary>
    /// Интерфейс заявки на ремонт.
    /// </summary>
    public interface IRepairRequest
    {
        int RequestID { get; }
        DateTime CreationDate { get; set; }
        int ApplianceID { get; set; }
        string ApplianceModel { get; set; }
        string IssueDescription { get; set; }
        string Status { get; set; }
        DateTime? ResolutionDate { get; set; }
        int? TechnicianID { get; set; }
        int? CustomerID { get; set; }

        /// <summary>
        /// Обновляет статус заявки. Если статус "Готова к выдаче", устанавливается ResolutionDate.
        /// </summary>
        void UpdateStatus(string newStatus);
    }
}