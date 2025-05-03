using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Model
{
    internal class Notification
    {
        private int _id;
        private int _UserId;
        private int _DoctorId;
        private string _NotificationMessage;
        private DateTime _NotificationDate;

        Notification(int id, int userId, int doctorId, string notificationMessage, DateTime notificationDate)
        {
            this._id = id;
            this._UserId = userId;
            this._DoctorId = doctorId;
            this._NotificationMessage = notificationMessage;
            this._NotificationDate = notificationDate;
        }

        Notification()
        {
            this._id = 0;
            this._UserId = 0;
            this._DoctorId = 0;
            this._NotificationMessage = string.Empty;
            this._NotificationDate = DateTime.Now;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int DoctorId
        {
            get { return _DoctorId; }
            set { _DoctorId = value; }
        }

        public string NotificationMessage
        {
            get { return _NotificationMessage; }
            set { _NotificationMessage = value; }
        }

        public DateTime NotificationDate
        {
            get { return _NotificationDate; }
            set { _NotificationDate = value; }
        }
    }
}
