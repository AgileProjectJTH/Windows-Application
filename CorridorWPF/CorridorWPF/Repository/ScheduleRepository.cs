﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CorridorWPF.Repository
{
    class ScheduleRepository
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="roomNr">number of staffs room ex E2404</param>
        /// <param name="date">Date of witch day to get schedule, yyyy-mm-dd ex 2016-04-25</param>
        /// <returns>Returns a json string with the schedule for the staff with the roomNr and Date (date may be null)</returns>
        public static string getSchedule(string time, string date, string token, string adress, string username)
        {
            using (var client = new HttpClient())
            {
                HttpWebRequest httpWebRequest;

                if (username != null)
                {
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(adress + date + " " + time + "&username=" + username);

                }
                else
                {
                    httpWebRequest = (HttpWebRequest)WebRequest.Create(adress + date + " " + time);
                }
                
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

        /// <summary>
        /// Fetches a token for the sent username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string getToken(string userName, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/CorridorAPI/token");


                    var postData = "grant_type=password&username=" + userName + "&password=" + password;


                    var data = Encoding.ASCII.GetBytes(postData);

                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = data.Length;

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)httpWebRequest.GetResponse();

                    string json;

                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        json = sr.ReadToEnd();
                    }

                    Token tok = JsonConvert.DeserializeObject<Token>(json);

                    System.Windows.MessageBox.Show("Success!");

                    return tok.access_token;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return null;
            }

        }

        /// <summary>
        /// Common function to load all tvViews
        /// </summary>
        /// <param name="dGrid"></param>
        /// <param name="cb_Box"></param>
        /// <param name="token"></param>
        public static void LoadTvView(DataGrid dGrid, ComboBox cb_Box, string token)
        {
            try
            {
                TvViewStaff tvView = new TvViewStaff(dGrid);
                List<string> listName = new List<string>();
                List<string> username = new List<string>();
                string data = cb_Box.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);

                Models.Staffs staffs = new Models.Staffs(json);

                tvView.createHeaders();
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    listName.Add(staffs.staffs[i].firstname.ToString() + " " + staffs.staffs[i].lastname.ToString());
                    username.Add(staffs.staffs[i].username.ToString());
                }

                for (int i = 0; i < listName.Count; i++)
                {
                    string jsonn = Repository.StaffRepository.GetTeacherAvailability(username[i], token);
                    if (jsonn == "true")
                    {
                        tvView.addStaff(listName[i].ToString(), true);
                    }
                    else
                    {
                        tvView.addStaff(listName[i].ToString(), false);
                    }

                }


            }
            catch (Exception ee)
            {
                System.Windows.MessageBox.Show(ee.ToString());
            }
        } 

    //    public static string getTeacherSchedule(string dateAndTime, string username, string token, string adress)
    //    {
    //        using (var client = new HttpClient())
    //        {

    //            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://193.10.30.155/corridorAPI/api/schedule?dateAndTime=" + dateAndTime + "?username=" + username);

    //            httpWebRequest.Method = WebRequestMethods.Http.Get;//GET OR POST
    //            httpWebRequest.Accept = "application/json; charset=utf-8";
    //            httpWebRequest.ContentType = "application/json; charset=utf-8";
    //            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

    //            var response = (HttpWebResponse)httpWebRequest.GetResponse();
    //            string json;
    //            using (var sr = new StreamReader(response.GetResponseStream()))
    //            {
    //                json = sr.ReadToEnd();
    //            }

    //            return json;
    //        }
    //    }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}
