using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {

            var api = new TelegramApi();
            

            var data = File.ReadAllText(@"C:\Users\yana.sukharieva\Documents\Stady_Projects\study_projects\TelegramBot\TelegramBot\TelegramBot\database.json");
            var dataBase = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            Console.WriteLine("Hello user! My name is SkillBot, I'm here to answer your questions!");

            while (true)
            {
                var userQuestion = Console.ReadLine().ToLower();
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

                if (userQuestion.Contains("bye"))
                {
                    Console.WriteLine("I'm tired anyway, so bye...");
                    break;
                }

                var result = String.Join(", ", answers);
                Console.WriteLine($">>{result}");
            }
            Console.ReadKey();

        }
    }
}
