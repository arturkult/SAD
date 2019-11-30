using Newtonsoft.Json;
using SAD.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FakeRFIDCard
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(30));
            Directory.GetCurrentDirectory();
            var rooms = File.ReadAllLines("rooms.csv")
                            .ToList()
                            .Select(line => line.Split(';')[0])
                            .ToArray();

            using (StreamReader stream = new StreamReader(new FileStream("cards.csv", FileMode.Open)))
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    Thread t = new Thread(new ThreadStart(() => SendRequestsAsCard(line.Split(';')[0], rooms)));
                    t.Start();
                }
            }
            Console.ReadKey();
        }

        public static void SendRequestsAsCard(string cardId, string[] rooms)
        {
            Random randomizer = new Random();
            for (var i = 0; i < 10; i++)
            {
                var sleepTime = randomizer.Next(1, 10);
                var roomIndex = randomizer.Next(rooms.Length);
                var requestBody = new RequestVM
                {
                    CardSerialNumber = cardId,
                    RoomNumber = rooms[roomIndex]
                };
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri("https://localhost:5001")
                };
                Thread.Sleep(TimeSpan.FromSeconds(sleepTime));
                Console.WriteLine("Send Request");
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                try
                {
                    var result = client.PostAsync("api/room/check", content).GetAwaiter().GetResult().StatusCode;
                    Console.WriteLine("Request from card {0:s} to room: {1: s} returned with code {2:d}", requestBody.CardSerialNumber, rooms[roomIndex], (int)result);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error while sending request");
                }
            }
        }
    }
}
