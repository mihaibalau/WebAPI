using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WinUI.Service.IService
{
    public interface INotificationService
    {

        /// <summary>
        /// Return all notification
        /// </summary>
        Task<List<Notification>> GetAllAsync();

        /// <summary>
        /// Returnează all notification for a user
        /// </summary>
        Task<List<Notification>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Delete notification <paramref name="notificationId"/>, 
        /// delete notifications only for <paramref name="userId"/>.
        /// </summary>
        Task DeleteAsync(int notificationId, int userId);

    }
}
