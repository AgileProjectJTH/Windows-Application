using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorridorWPF.Models
{
    public class Staff
    {
        public Staff() { }

        /// <summary>
        /// Constructor
        /// Creates an Staff object with scedules out of inc Json string
        /// </summary>
        /// <param name="JsonStaff">JToken</param>
        public Staff(JToken JsonStaff)
        {
            schedules = new List<Schedule>();
            signature = (string)JsonStaff["Signature"];
            firstname = (string)JsonStaff["Firstname"];
            lastname = (string)JsonStaff["Lastname"];
            mobile = (string)JsonStaff["Mobile"];
            email = (string)JsonStaff["Mail"];
            JArray jScheArr = (JArray)JsonStaff["Schedule"];
            for (int k = 0; k < jScheArr.Count; k++)
            {
                Schedule s = new Schedule(jScheArr[k]);
                schedules.Add(s);
            }
        }

        /// <summary>
        /// Turns a staff object to A Json string
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public string toJson(Staff staff)
        {
            return JsonConvert.SerializeObject(staff);
        }

        public string signature { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public bool isAvailable { get; set; }
        public bool isAdmin { get; set; }

        public List<Schedule> schedules { get; set; }
    }
}

