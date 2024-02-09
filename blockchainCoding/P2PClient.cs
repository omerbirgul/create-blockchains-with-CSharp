using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace blockchainCoding
{
    public class P2PClient
    {
        public IDictionary<string, WebSocket> wsDictionary = new Dictionary<string, WebSocket>();
        // burada bağlanılacak server'lar saklanacak.

        public void Connect(string url)
        {
            if(!wsDictionary.ContainsKey(url))
            {
                WebSocket ws = new WebSocket(url);
                // dictionary'de bu url yoksa yeni bir kanal acacağız.(websocket kanalı açtık)

                ws.OnMessage += (sender, e) =>
                {
                    if(e.Data == "Merhaba Client")
                    {
                        Console.WriteLine(e.Data);
                    }
                    else
                    {
                        Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                        if(newChain.IsValid() && newChain.Chain.Count > Program.ourBlockchain.Chain.Count) 
                        {
                            List<Transaction> newTransactions = new List<Transaction>();
                            newTransactions.AddRange(newChain.PendingTransactions);
                            newTransactions.AddRange(Program.ourBlockchain.PendingTransactions);
                            newChain.PendingTransactions = newTransactions;
                            Program.ourBlockchain = newChain;
                        }   
                    }
                };

                ws.Connect();
                ws.Send("Merhaba Client");
                ws.Send(JsonConvert.SerializeObject(Program.ourBlockchain));
                wsDictionary.Add(url, ws);
            }
        }

        public void Send(string url, string data)
        {
            foreach(var item in wsDictionary)
            {
                if(item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
        }


        public void Broadcast(string data)
        {
            foreach(var item in wsDictionary)
            {
                item.Value.Send(data);
            }
        }

        public IList<string> GetServers()
        {
            IList<string> servers = new List<string>();
            foreach (var item in wsDictionary)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        public void Close()
        {
            foreach (var item in wsDictionary)
            {
                item.Value.Close();
            }

        }

    }
}
