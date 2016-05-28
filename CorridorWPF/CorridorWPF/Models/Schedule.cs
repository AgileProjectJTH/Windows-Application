using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorridorWPF.Models
{
    public class Schedule
    {
        public Schedule() { }
        /// <summary>
        /// Constructor
        /// Creates an Schedule object out of inc Json string
        /// </summary>
        /// <param name="JsonSchedule">JToken</param>
        public Schedule(JToken JsonSchedule)
        {
            room = (string)JsonSchedule["room"];
            date = (string)JsonSchedule["date"];
            from = (string)JsonSchedule["from"];
            to = (string)JsonSchedule["to"];
            signatures = (string)JsonSchedule["signatures"];
            course = (string)JsonSchedule["course"];
            moment = (string)JsonSchedule["moment"];           
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newRoom">roomNr</param>
        /// <param name="newDate">Date format yyyy-mm-dd-hh-mm-ss</param>
        /// <param name="NewFrom">Time format hh-mm</param>
        /// <param name="NewTo">Time format hh-mm</param>
        public Schedule(string newRoom, string newDate, string NewFrom, string NewTo)
        {
            room = newRoom;
            date = newDate;
            from = NewFrom;
            to = NewTo;
        }
        public string room { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string signatures { get; set; }
        public string course { get; set; }
        public string moment { get; set; }
        public bool available { get; set; }
    }
}

