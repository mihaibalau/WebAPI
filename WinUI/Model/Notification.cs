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
        private int _userId;
        private int _doctorId;
        private string _notificationMessage;
        private DateTime _notificationDate;

        Notification(int id, int userId, int doctorId, string notificationMessage, DateTime notificationDate)
        {
            this._id = id;
            this._userId = userId;
            this._doctorId = doctorId;
            this._notificationMessage = notificationMessage;
            this._notificationDate = notificationDate;
        }

        Notification()
        {
            this._id = 0;
            this._userId = 0;
            this._doctorId = 0;
            this._notificationMessage = string.Empty;
            this._notificationDate = DateTime.Now;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public int doctorId
        {
            get { return _doctorId; }
            set { _doctorId = value; }
        }

        public string notificationMessage
        {
            get { return _notificationMessage; }
            set { _notificationMessage = value; }
        }

        public DateTime notificationDate
        {
            get { return _notificationDate; }
            set { _notificationDate = value; }
        }
    }
}
