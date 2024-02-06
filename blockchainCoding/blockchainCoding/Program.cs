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
            Console.WriteLine("blokchain gecerli mi? " + ourBlockchain.IsValid().ToString());
            Console.WriteLine("Veri degistiriliyor...");

            //Butun blokların hash'i degisti(%51 attack). degisim olunca gecerlilik true olur.

            ourBlockchain.Chain[1].Hash = ourBlockchain.Chain[1].CalculateHash();

            ourBlockchain.Chain[2].PreviousHash = ourBlockchain.Chain[1].Hash;
            ourBlockchain.Chain[2].Hash = ourBlockchain.Chain[2].CalculateHash();

            ourBlockchain.Chain[3].PreviousHash = ourBlockchain.Chain[2].Hash;
            ourBlockchain.Chain[3].Hash = ourBlockchain.Chain[3].CalculateHash();

            Console.WriteLine("blokchain gecerli mi? " + ourBlockchain.IsValid().ToString());


            Console.ReadKey();
        }
    }
}
