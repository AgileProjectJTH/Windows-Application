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


        /// <summary>
        /// Get all corridors
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string getAllCorridors(string token)
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




        /// <summary>
        /// Gets corridor
        /// </summary>
        /// <param name="corridorID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Adds new corridor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Moves existing user to corridor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="corridorID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string MoveUserToCorridor(string username, string corridorID, string token)
        {
            try
            {             

                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor?username=" + username + "&corridorId=" + corridorID);
                    
                    httpWebRequest.Method = WebRequestMethods.Http.Post;//GET OR POST
                    httpWebRequest.Accept = "application/json; charset=utf-8";
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    //httpWebRequest.ContentLength = corridorID.Length + username.Length;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                    string data = "?username=" + username + "&corridorId=" + corridorID;
                    var byteToSend = Encoding.ASCII.GetBytes(data);

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(byteToSend, 0, byteToSend.Length);
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



        public static List<string> getStaffInCorridor(string token,  System.Windows.Controls.ComboBox box)
        {
            List<string> staff = new List<string>();
            try
            {
                string data = box.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);
                Models.Staffs staffs = new Models.Staffs(json);
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    staff.Add(staffs.staffs[i].username.ToString());
                }

                return staff;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return null;
            }
        }

        public static string deleteCorridor(string corridorId, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Corridor?corridorId=" + corridorId);

                    httpWebRequest.Method = "DELETE";//GET OR POST
                    httpWebRequest.Accept = "application/json; charset=utf-8";
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.ContentLength = corridorId.Length;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                    var bytesToSend = Encoding.ASCII.GetBytes(corridorId);

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(bytesToSend, 0, bytesToSend.Length);
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
