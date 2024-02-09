using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace blockchainCoding
{
    public class P2PServer : WebSocketBehavior
    {

        bool chainSynched = false;
        WebSocketServer webSocketServer = null;
        public void Start()
        {
            webSocketServer = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            webSocketServer.AddWebSocketService<P2PServer>("/Blockchain");
            webSocketServer.Start();
            Console.WriteLine($"Server su adreste baslatildi ws://127.0.0.1:{Program.Port}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Merhaba Server")
            {
                Console.WriteLine(e.Data);
                Send("Merhaba Client");
            }
            else
            {
                Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                if (newChain.IsValid() && newChain.Chain.Count > Program.ourBlockchain.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactions);
                    newTransactions.AddRange(Program.ourBlockchain.PendingTransactions);
                    newChain.PendingTransactions = newTransactions;
                    Program.ourBlockchain = newChain;
                }
            }

            if (!chainSynched)
            {
                Send(JsonConvert.SerializeObject(Program.ourBlockchain));
                chainSynched = true;
            }
        }
    }
}
