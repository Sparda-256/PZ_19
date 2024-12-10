using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore; // Добавлено
using PZ_18.Data;
using PZ_18.Models;

namespace PZ_18.ViewModels
{
    /// <summary>
    /// ViewModel для окна добавления/редактирования заявки.
    /// </summary>
    public class AddEditRequestViewModel : INotifyPropertyChanged
    {
        private RepairRequest _currentRequest;
        private ApplianceCategory _selectedApplianceCategory;

        /// <summary>
        /// Текущая заявка.
        /// </summary>
        public RepairRequest CurrentRequest
        {
            get => _currentRequest;
            set
            {
                _currentRequest = value;
                OnPropertyChanged(nameof(CurrentRequest));
            }
        }

        /// <summary>
        /// Список возможных статусов заявки.
        /// </summary>
        public ObservableCollection<string> StatusOptions { get; } = new ObservableCollection<string>
        {
            "Новая заявка",
            "В процессе ремонта",
            "Ожидание запчастей",
            "Готова к выдаче"
        };

        /// <summary>
        /// Список доступных категорий техники, загружаемых из БД.
        /// </summary>
        public ObservableCollection<ApplianceCategory> ApplianceCategories { get; set; }

        /// <summary>
        /// Выбранная категория техники.
        /// </summary>
        public ApplianceCategory SelectedApplianceCategory
        {
            get => _selectedApplianceCategory;
            set
            {
                _selectedApplianceCategory = value;
                OnPropertyChanged();
                // При выборе категории техники сразу назначаем ApplianceID заявке
                if (CurrentRequest != null && _selectedApplianceCategory != null)
                {
                    CurrentRequest.ApplianceID = _selectedApplianceCategory.ApplianceID;
                }
            }
        }

        /// <summary>
        /// Команда для сохранения заявки.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Событие, вызываемое после успешного сохранения заявки.
        /// </summary>
        public event Action RequestSaved;

        /// <summary>
        /// Конструктор ViewModel.
        /// </summary>
        /// <param name="request">Заявка, которую редактируем или добавляем.</param>
        public AddEditRequestViewModel(RepairRequest request = null)
        {
            using (var context = new CoreContext())
            {
                // Загружаем категории техники из БД
                ApplianceCategories = new ObservableCollection<ApplianceCategory>(context.ApplianceCategories.ToList());
            }

            if (request == null)
            {
                // Создаем новую заявку
                CurrentRequest = new RepairRequest
                {
                    CreationDate = DateTime.Now,
                    Status = "Новая заявка"
                };
                // По умолчанию выберем первую категорию техники, если есть
                if (ApplianceCategories.Count > 0)
                {
                    SelectedApplianceCategory = ApplianceCategories[0];
                }
            }
            else
            {
                // Редактируем существующую
                CurrentRequest = request;
                // Найдем соответствующую категорию техники в списке
                SelectedApplianceCategory = ApplianceCategories.FirstOrDefault(ac => ac.ApplianceID == CurrentRequest.ApplianceID);
            }

            SaveCommand = new RelayCommand(SaveRequest);
        }

        /// <summary>
        /// Логика сохранения заявки.
        /// </summary>
        private void SaveRequest(object obj)
        {
            // Перед сохранением убеждаемся, что выбрана категория техники
            if (SelectedApplianceCategory == null)
            {
                MessageBox.Show("Пожалуйста, выберите категорию техники.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CurrentRequest.ApplianceID = SelectedApplianceCategory.ApplianceID;

            try
            {
                using (var context = new CoreContext())
                {
                    if (CurrentRequest.RequestID == 0)
                    {
                        context.RepairRequests.Add(CurrentRequest);
                    }
                    else
                    {
                        context.RepairRequests.Update(CurrentRequest);
                    }
                    context.SaveChanges();
                }

                // Вызываем событие, чтобы закрыть окно
                RequestSaved?.Invoke();
            }
            catch (DbUpdateException ex)
            {
                // Логирование ошибки и уведомление пользователя
                MessageBox.Show($"Ошибка при сохранении заявки: {ex.InnerException?.Message ?? ex.Message}",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}