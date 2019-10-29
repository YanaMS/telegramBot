using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using System.Text;
using RestSharp;

namespace TelegramBot
{
    class TelegramApi
    {
        public class ApiResult
        {
            public UpdateRowSource[] result { get; set; }
        }

        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
        }

        public class Message
        {
            public Chat chat { get; set; }
            public string text { get; set; }
        }

        public class Chat
        {
            public int id { get; set; }
            public string first_name { get; set; }
        }

        public TelegramApi() {}
       

        RestClient RC = new RestClient();
        private const string API_URL = "https://api.telegram.org/" + SecretKey.apiKey + "/";

        public void SendMessage(string text, int chat_id)
        {
            SendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}");
        }

        public string SendApiRequest(string apiMethod, string param)
        {
            var Url = API_URL + apiMethod + "?" + param;

            var Request = new RestRequest(Url);

            var Response = RC.Get(Request);

            return ApiResult
        }
    }
}
