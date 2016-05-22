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
    class CorridorRepository
    {
        public static string getCorridor()
        {
            string token = "6_4QnDpsDB8Wsgn-1lN1Vx3NiQ8-RYV_GxVACV3mJCocsnaygdXK3sWGRG7AM10Iw0NSUAMDlLwP4VF54YDPqCsvfLSfTLrVdI-r2BFEHIwNBL6LMLu5zanRiW7fz57qroEuk13tjUGNUoNWdO4UWPpC90k10JzVe0n3R-3oZlc-YcBcL7NgfR0fTDqAJ1fSgnVblv7Jxs74oteqbbsYIiDOUQ19NXdZOaznyHDJSqA";
            using (var client = new HttpClient())
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor"); //+ roomNr + "?date=" + date);

                httpWebRequest.Method = WebRequestMethods.Http.Get;//GET OR POST
                httpWebRequest.Accept = "application/json; charset=utf-8";
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
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
