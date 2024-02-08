using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    public class Blockchain
    {
        IList<Transaction> PendingTransactions = new List<Transaction>();
        public IList<Block> Chain { get; set; }
        public int difficulty { get; set; } = 2;
        public int Reward { get; set; } = 1;
        public Blockchain() 
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            Block genesisBlock = new Block(DateTime.Now, null, PendingTransactions);
            genesisBlock.Mine(difficulty);
            PendingTransactions = new List<Transaction>();

            return genesisBlock ;
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.difficulty);
            Chain.Add(block);
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions(string minerAdress)
        {
            CreateTransaction(new Transaction(null, minerAdress, Reward));
            Block pendingBlock = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            AddBlock(pendingBlock);
            PendingTransactions = new List <Transaction>();
        }

        public bool IsValid()
        {
            for(int i = 1; i < Chain.Count; i++) 
            {
                Block currentBlock = Chain[i];
                Block previosuBlock = Chain[i - 1];
                if(currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }
                if(currentBlock.PreviousHash != previosuBlock.Hash) 
                {
                    return false;
                }
            }
            return true;
        }

        public int GetBalance(string adress)
        {
            int balance = 0;
            for(int i = 0; i < Chain.Count; i++)
            {
                for (int j = 0; j < Chain[i].Transactions.Count; j++)
                {
                    var transaction = Chain[i].Transactions[j];
                    if(transaction.SenderAdress == adress)
                    {
                        balance -= transaction.Amount;
                    }
                    if(transaction.ReceiverAdress == adress)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
    }
}
