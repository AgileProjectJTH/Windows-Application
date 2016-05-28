using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


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



        /// <summary>
        /// Returns all staffs of a certain corridor
        /// </summary>
        /// <param name="corridorID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetCorridorTeachers(string corridorID, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/user?corridorNr=" + corridorID);

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
        /// Gets if staff is available
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetTeacherAvailability(string username, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {

                 
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/Staff?dateAndTime=" + DateTime.Now.ToString("yyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss") + "&username=" + username);

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
        /// Adds new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string AddNewUser(Models.User user, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string json;
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/CorridorAPI/api/account/register");
                    
                    httpWebRequest.Method = WebRequestMethods.Http.Post;//GET OR POST
                    httpWebRequest.Accept = "application/json; charset=utf-8";
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        //string json = "{\"user\": \"test\"," +
                        //                "\"password\":\"bla\"}";


                        json = JsonConvert.SerializeObject(user);

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                        var response = (HttpWebResponse)httpWebRequest.GetResponse();
                    
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        json = sr.ReadToEnd();
                    }

                    System.Windows.MessageBox.Show("User was added!");
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
        /// Delete user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string deleteUser(string username, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/User?username=" + username);// + username);

                    httpWebRequest.Method = "DELETE";
                    
                    httpWebRequest.Accept = "application/json; charset=utf-8";
                    httpWebRequest.ContentType = "application/json; charset=utf-8";
                    httpWebRequest.ContentLength = username.Length;
                    httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                    var byteName = Encoding.ASCII.GetBytes(username);

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
