using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    public class Transaction
    {
        public string SenderAdress { get; set; }
        public string ReceiverAdress { get; set; }
        public int Amount { get; set; }

        public Transaction(string senderAdress, string receiverAdress, int amount)
        {
            this.SenderAdress = senderAdress;
            this.ReceiverAdress = receiverAdress;
            this.Amount = amount;
        }
    }
}

