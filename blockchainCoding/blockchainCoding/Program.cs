using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Blockchain ourBlockchain = new Blockchain();
            ourBlockchain.AddBlock(new Block(DateTime.Now, null, "{sender: Omer, receiver: Duygu, amount: 5}"));
            ourBlockchain.AddBlock(new Block(DateTime.Now, null, "{sender: Micheal, receiver: Gustavo, amount: 50}"));
            ourBlockchain.AddBlock(new Block(DateTime.Now, null, "{sender: Haaland, receiver: Sasha, amount: 10}"));
            Console.WriteLine(JsonConvert.SerializeObject(ourBlockchain,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
