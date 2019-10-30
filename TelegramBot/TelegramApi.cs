using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace TelegramBot
{
    class TelegramApi
    {
        public class ApiResult
        {
            public Update[] Result { get; set; }
        }

        public class Update
        {
            public int? Update_id { get; set; }
            public Message Message { get; set; }
        }

        public class Message
        {
            public Chat Chat { get; set; }
            public string Text { get; set; }
        }

        public class Chat
        {
            public int Id { get; set; }
            public string First_name { get; set; }
        }

        private int lastUpdateId = 0;
        public TelegramApi() {}
       

        RestClient RC = new RestClient();
        private const string API_URL = "https://api.telegram.org/bot" + SecretKey.API_KEY + "/";

        public void SendMessage(string text, int chat_id)
        {
            SendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}");
        }

        public Update[] GetUpdates()
        {
            var json = SendApiRequest("getUpdates", $"offset={lastUpdateId}");
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            foreach (var update in apiResult.Result)
            {
                if (update.Message == null || update.Update_id == null)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine($"Get updates {update.Update_id}, "
                                      + $"message from {update.Message.Chat.First_name}, "
                                      + $"text: {update.Message.Text}"
                    );
                    lastUpdateId = (int) (update.Update_id + 1);
                }
 
            }

            return apiResult.Result;

        }

        private string SendApiRequest(string apiMethod, string param)
        {
            var URL = API_URL + apiMethod + "?" + param;

            var Request = new RestRequest(URL);

            var Response = RC.Get(Request);

            return Response.Content;
        }
    }
}
