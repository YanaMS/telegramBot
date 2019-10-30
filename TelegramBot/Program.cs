using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TelegramBot
{
    class Program
    {
        private static Dictionary<string, string> dataBase;  
        static void Main(string[] args)
        {
            var data = File.ReadAllText(@"C:\Users\yana.sukharieva\Documents\Stady_Projects\study_projects\myFirstBot\TelegramBot\database.json");
            dataBase = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            var api = new TelegramApi();
            while (true)
            {
                var updates = api.GetUpdates();
                foreach (var update in updates)
                {
                    if (update.Message == null || update.Update_id == null || update.Message.Text == null)
                    {
                        continue;
                    }
                    string answer = AnswerQuestion(update.Message.Text);
                    var message = $"Dear, {update.Message.Chat.First_name}, {answer}";
                    api.SendMessage(message, update.Message.Chat.Id);
                }
            }

        }

        private static string AnswerQuestion(string question)
        {
            var userQuestion = question.ToLower();
            var answers = new List<string>();

            foreach (var entry in dataBase)
            {
                if (userQuestion.Contains(entry.Key))
                {
                    answers.Add(entry.Value);
                }
            }

            if (userQuestion.Contains("what time is it"))
            {
                var time = DateTime.Now.ToString("HH:mm:ss");
                answers.Add($"Current time: {time}");
            }

            if (userQuestion.Contains("what date is it"))
            {
                var date = DateTime.Now.ToString("dd.MM.yyyy");
                answers.Add($"Current time: {date}");
            }

            if (answers.Count == 0)
            {
                answers.Add("I don't understand you");
            }

            var result = String.Join(", ", answers);
            return result;

        }
    }
}
