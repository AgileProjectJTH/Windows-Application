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
            room = (string)JsonSchedule["Room"];
            date = (string)JsonSchedule["Date"];
            from = (string)JsonSchedule["From"];
            to = (string)JsonSchedule["To"];
            signatures = (string)JsonSchedule["Signatures"];
            course = (string)JsonSchedule["Course"];
            moment = (string)JsonSchedule["Moment"];
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
    }
}

