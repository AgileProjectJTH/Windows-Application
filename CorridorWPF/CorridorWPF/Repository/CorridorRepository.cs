using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;

namespace CorridorWPF.Repository
{
    class CorridorRepository
    {

        public static string getCorridor(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor");

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
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return null;
            }

        }

        public static string GetCorridor(string corridorID, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor?corridorId=" + corridorID);

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
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return null;
            }

        }

        public static string addCorridor(string name, string token)
        {
            try {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor?corridorName=" + name);

                    httpWebRequest.Method = WebRequestMethods.Http.Post;//GET OR POST
                    httpWebRequest.Accept = "application/json; charset=utf-8";
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.ContentLength = name.Length;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                    var byteName = Encoding.ASCII.GetBytes(name);

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(byteName, 0, byteName.Length);
                    }

                    var response = (HttpWebResponse)httpWebRequest.GetResponse();

                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    return responseString;
                }
            }
            catch (Exception e)
            {          
                System.Windows.MessageBox.Show(e.ToString());
                return null;
            }

        }
    }
}
