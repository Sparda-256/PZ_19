using System;
using System.Collections.Generic;
using System.Linq;
using PZ_18.Data;
using PZ_18.Models;
using Microsoft.EntityFrameworkCore;

public class RepairRequestRepository
{
    private readonly CoreContext _context;

    public RepairRequestRepository(CoreContext context)
    {
        _context = context;
    }

    public RepairRequest FindByNumber(int requestId)
    {
        return _context.RepairRequests.Include(r => r.ApplianceCategory)
                                      .Include(r => r.Technician)
                                      .FirstOrDefault(r => r.RequestID == requestId);
    }

    public List<RepairRequest> SearchRequests(string customerName = null, string status = null, int? applianceId = null)
    {
        var query = _context.RepairRequests.AsQueryable();

        if (!string.IsNullOrEmpty(customerName))
            query = query.Where(r => r.CustomerName.Contains(customerName));

        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.Status == status);

        if (applianceId.HasValue)
            query = query.Where(r => r.ApplianceID == applianceId);

        return query.Include(r => r.ApplianceCategory)
                    .Include(r => r.Technician)
                    .ToList();
    }

    public int GetCompletedRequestsCount()
    {
        return _context.RepairRequests.Count(r => r.Status == "Готова к выдаче");
    }

    public double GetAverageCompletionTime()
    {
        var completed = _context.RepairRequests.Where(r => r.ResolutionDate.HasValue && r.CreationDate < r.ResolutionDate.Value).ToList();
        if (completed.Count == 0) return 0;
        var avgTicks = completed.Average(r => (r.ResolutionDate.Value - r.CreationDate).Ticks);
        return new TimeSpan((long)avgTicks).TotalDays;
    }
}