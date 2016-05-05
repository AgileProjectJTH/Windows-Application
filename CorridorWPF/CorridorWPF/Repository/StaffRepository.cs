using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorridorWPF.Repository
{
    class StaffRepository
    {
        /// <summary>
        /// POST
        /// </summary>
        /// <param name="roomNr">number of staffs room ex E2404</param>
        /// <param name="date">Date of witch day to get schedule, yyyy-mm-dd ex 2016-04-25</param>
        /// <returns>Returns a json string with the schedule for the staff with the roomNr and Date (date may be null)</returns>
        public static string PostSchedule()
        {
            using (var client = new HttpClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:50052/Api/schedule?fromDateAndTime=2016-04-25 14:44:08&toDateAndTime=2016-04-28 14:44:08");
                            //"Adress till vårat API ex http://localhost:50052/Api/" + "controller namn (StaffName i denna class kan ju vara lämpligt" + "?" 
                            // + "variabler" +"ex" +"fromDateAndTime=2016-04-25 14:44:08&toDateAndTime=2016-04-28 14:44:08" 

                httpWebRequest.Method = WebRequestMethods.Http.Post;//GET OR POST
                httpWebRequest.Accept = "application/json; charset=utf-8";
                httpWebRequest.ContentType = "application/json; charset=utf-8";

                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                string json;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }

                return json;
            }
        }
    }
}
