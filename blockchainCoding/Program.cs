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
            Console.WriteLine("gecerli mi? " + ourBlockchain.IsValid().ToString());
            Console.WriteLine("Veri degistiriliyor...");
            ourBlockchain.Chain[1].Data = "{sender: ABC, receiver: XYZ, amount: 100}";
            Console.WriteLine("degistirildi ve gecerli mi? " + ourBlockchain.IsValid().ToString());
            Console.ReadKey();
        }
    }
}
