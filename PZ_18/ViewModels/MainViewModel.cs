using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PZ_18.Data;
using PZ_18.Models;
using System.Windows.Input;

namespace PZ_18.ViewModels
{
    /// <summary>
    /// Главная ViewModel приложения.
    /// Отвечает за загрузку списков пользователей и заявок, а также поиск заявок.
    /// </summary>
    public class MainViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<RepairRequest> RepairRequests { get; set; }

        public ICommand SearchRequestsCommand { get; }

        public string SearchText { get; set; }

        public MainViewModel()
        {
            // Загрузка данных из контекста (пользователи и заявки).
            using (var context = new CoreContext())
            {
                Accounts = new ObservableCollection<Account>(context.Accounts.Include(a => a.UserCategory).ToList());
                RepairRequests = new ObservableCollection<RepairRequest>(
                    context.RepairRequests.Include(r => r.ApplianceCategory).ToList()
                );
            }

            SearchRequestsCommand = new RelayCommand(SearchRequests);
        }

        /// <summary>
        /// Логика поиска заявок по имени клиента.
        /// </summary>
        public void SearchRequests(object parameter)
        {
            using (var context = new CoreContext())
            {
                var query = context.RepairRequests.Include(r => r.ApplianceCategory).AsQueryable();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    query = query.Where(r => r.CustomerName.Contains(SearchText));
                }
                var list = query.ToList();
                RepairRequests.Clear();
                foreach (var r in list)
                    RepairRequests.Add(r);
            }
        }
    }
}