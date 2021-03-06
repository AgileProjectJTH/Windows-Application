﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorridorWPF.Models
{
    class Staffs
    {
        /// <summary>
        /// Konverts Json to List of Staffs
        /// </summary>
        /// <param name="json"></param>
        public Staffs(string json)
        {
            try
            {
                JObject jStaff = JObject.Parse(json);
                JArray jStaffArr = (JArray)jStaff["staffModels"];
                staffs = new List<Staff>();
                for (int i = 0; i < jStaffArr.Count; i++)
                {
                    Staff staff = new Staff(jStaffArr[i]);
                    staffs.Add(staff);
                }
                
            }
            catch { }

        }

        public List<Staff> staffs { get; set; }
    }
}
