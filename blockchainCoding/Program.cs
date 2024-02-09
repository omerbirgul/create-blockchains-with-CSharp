using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    public class Program
    {
        public static Blockchain ourBlockchain = new Blockchain();
        public static int Port = 0;
        public static P2PClient Client = new P2PClient();
        public static P2PServer Server = null;
        public static string name = "Unknown";
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            ourBlockchain.InitializeChain();
            if(args.Length >= 1)
            {
                Port = int.Parse(args[0]);
            }
            if(args.Length >= 2)
            {
                name = args[1];
            }
            if(Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            if(name != "Unknown")
            {
                Console.WriteLine($"Su anki kullanici: {name}");
            }

            Console.WriteLine("**************************");
            Console.WriteLine("1. Server'a baglan");
            Console.WriteLine("2. Trabsaction ekle");
            Console.WriteLine("3. Blockchain'i göster");
            Console.WriteLine("4. Cikis");
            Console.WriteLine("**************************");

            int selection = 0;
            while(selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Lutfen Server URL Girin:");
                        string serverURL = Console.ReadLine();
                        Client.Connect($"{serverURL}/Blockchain");
                        break;
                    case 2:
                        Console.WriteLine("Lutfen Alici Adi Girin:");
                        string receiverName = Console.ReadLine();
                        Console.WriteLine("Miktari Girin:");
                        string amount = Console.ReadLine();
                        ourBlockchain.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                        ourBlockchain.ProcessPendingTransactions(name);
                        Client.Broadcast(JsonConvert.SerializeObject(ourBlockchain));
                        break;
                    case 3:
                        Console.WriteLine("Blockchain");
                        Console.WriteLine(JsonConvert.SerializeObject(ourBlockchain, Formatting.Indented));
                        break;
                }

                Console.WriteLine("Lutfen bir secenek secin");
                String action = Console.ReadLine();
                selection = int.Parse(action);
            }

            Client.Close();
            Console.ReadKey();
        }
    }
}
