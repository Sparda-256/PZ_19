namespace PZ_18.Models.Interfaces
{
    /// <summary>
    /// Интерфейс аккаунта пользователя.
    /// </summary>
    public interface IAccount
    {
        int AccountID { get; }
        string FullName { get; set; }
        string PhoneNumber { get; set; }
        string Username { get; set; }
        string UserPassword { get; set; }
        int CategoryID { get; set; }

        /// <summary>
        /// Смена пароля.
        /// </summary>
        void ChangePassword(string newHashedPassword);
    }
}