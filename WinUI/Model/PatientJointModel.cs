using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Model
{
    public class PatientJointModel
    {
        public int user_id { get; set; }
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string blood_type { get; set; }
        public string emergency_contact { get; set; }
        public string allergies { get; set; }
        public double weight { get; set; }
        public int height { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateOnly birth_date { get; set; }
        public string cnp { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public DateTime registration_date { get; set; }

        public static readonly PatientJointModel Default = new(
            -1, -1, string.Empty, string.Empty, string.Empty, string.Empty,
            -1, -1, string.Empty, string.Empty, string.Empty,
            DateOnly.MaxValue, string.Empty, string.Empty, string.Empty, DateTime.Now
        );

        public PatientJointModel(
            int user_id,
            int patient_id,
            string patient_name,
            string blood_type,
            string emergency_contact,
            string allergies,
            double weight,
            int height,
            string username,
            string password,
            string email,
            DateOnly birth_date,
            string cnp,
            string address,
            string phone_number,
            DateTime registration_date
        )
        {
            this.user_id = user_id;
            this.patient_id = patient_id;
            this.patient_name = patient_name;
            this.blood_type = blood_type;
            this.emergency_contact = emergency_contact;
            this.allergies = allergies;
            this.weight = weight;
            this.height = height;
            this.username = username;
            this.password = password;
            this.email = email;
            this.birth_date = birth_date;
            this.cnp = cnp;
            this.address = address;
            this.phone_number = phone_number;
            this.registration_date = registration_date;
        }
    }
}
