using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Model
{
    internal class Notification
    {
        private int _NotificationId;
        private int _UserId;
        private int _DoctorId;
        private string _NotificationMessage;
        private DateTime _NotificationDateTime;


        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class with specified values.
        /// </summary>
        /// <param name="notificationId">The unique identifier for the notification.</param>
        /// <param name="userId">The unique identifier of the user who receives the notification.</param>
        /// <param name="doctorId">The unique identifier of the doctor who sends the notification.</param>
        /// <param name="notificationMessage">The message content of the notification.</param>
        /// <param name="notificationDateTime">The date and time when the notification was sent or created.</param>
        Notification(int notificationId, int userId, int doctorId, string notificationMessage, DateTime notificationDateTime)
        {
            this._NotificationId = notificationId;
            this._UserId = userId;
            this._DoctorId = doctorId;
            this._NotificationMessage = notificationMessage;
            this._NotificationDateTime = notificationDateTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class with default values.
        /// </summary>
        Notification()
        {
            this._NotificationId = 0;
            this._UserId = 0;
            this._DoctorId = 0;
            this._NotificationMessage = string.Empty;
            this._NotificationDateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the notification.
        /// </summary>
        public int Id
        {
            get { return _NotificationId; }
            set { _NotificationId = value; }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the user who receives the notification.
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the doctor who sends the notification.
        /// </summary>
        public int DoctorId
        {
            get { return _DoctorId; }
            set { _DoctorId = value; }
        }

        /// <summary>
        /// Gets or sets the message content of the notification.
        /// </summary>
        public string NotificationMessage
        {
            get { return _NotificationMessage; }
            set { _NotificationMessage = value; }
        }

        /// <summary>
        /// Gets or sets the date and time when the notification was sent or created.
        /// </summary>
        public DateTime NotificationDate
        {
            get { return _NotificationDateTime; }
            set { _NotificationDateTime = value; }
        }
    }
}
