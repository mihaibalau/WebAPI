using Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WinUI.ViewModel.Interface
{
    public interface INotificationViewModel
    {
        ObservableCollection<Notification> Notifications { get; }
        string ErrorMessage { get; }
        Task LoadNotificationsAsync(int userId);
        Task DeleteNotificationAsync(int notificationId, int userId);
    }
}
