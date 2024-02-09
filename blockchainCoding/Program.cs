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
        static void Main(string[] args)
        {
            

            DateTime startTime = DateTime.Now;
            ourBlockchain.CreateTransaction(new Transaction("Omer", "Duygu", 5));
            ourBlockchain.ProcessPendingTransactions("Ali");

            ourBlockchain.CreateTransaction(new Transaction("Ufuk", "Enes", 10));
            ourBlockchain.CreateTransaction(new Transaction("Test", "Mehmet", 8));
            ourBlockchain.ProcessPendingTransactions("Ali");

            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Hesaplamalar icin gerekli sure: {(startTime - endTime).ToString()}");
            Console.WriteLine($"Omer balance: {ourBlockchain.GetBalance("Omer").ToString()}");
            Console.WriteLine($"Enes balance: {ourBlockchain.GetBalance("Enes").ToString()}");
            Console.WriteLine($"Ufuk balance: {ourBlockchain.GetBalance("Ufuk").ToString()}");
            Console.WriteLine($"Ali balance: {ourBlockchain.GetBalance("Ali").ToString()}");

            Console.WriteLine(JsonConvert.SerializeObject(ourBlockchain,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
