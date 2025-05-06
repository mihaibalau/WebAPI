using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Model
{
    public class PatientDisplayModel
    {
        public int patient_id { get; set; }
        public string fullname { get; set; }
        public string cnp { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateOnly birth_date { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string blood_type { get; set; }
        public string emergency_contact_name { get; set; }
        public string allergies { get; set; }
        public float weight { get; set; }
        public int height { get; set; }
        public DateTime registration_date { get; set; }

        public PatientDisplayModel(
            int patient_id,
            string fullname,
            string cnp,
            string username,
            string email,
            string password,
            DateOnly birth_date,
            string address,
            string phone_number,
            string blood_type,
            string emergency_contact_name,
            string allergies,
            float weight,
            int height,
            DateTime registration_date
        )
        {
            this.patient_id = patient_id;
            this.fullname = fullname;
            this.cnp = cnp;
            this.username = username;
            this.email = email;
            this.password = password;
            this.birth_date = birth_date;
            this.address = address;
            this.phone_number = phone_number;
            this.blood_type = blood_type;
            this.emergency_contact_name = emergency_contact_name;
            this.allergies = allergies;
            this.weight = weight;
            this.height = height;
            this.registration_date = registration_date;
        }
    }
}
