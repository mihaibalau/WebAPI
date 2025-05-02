using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WinUI.IService
{
    internal interface INotificationService
    {
        /// <summary>
        /// Return all Notifications for all users.
        /// </summary>
        Task<List<Notification>> GetAllAsync();

        /// <summary>
        /// Return all notifications for a specific user.
        /// </summary>
        Task<List<Notification>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Delete all notifications with <paramref name="notificationId"/>, 
        /// Only if is from user <paramref name="userId"/>.
        /// </summary>
        Task DeleteAsync(int notificationId, int userId);
    }
}
