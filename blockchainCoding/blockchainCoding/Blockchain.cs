using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    public class Blockchain
    {
        public IList<Block> Chain { get; set; }
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
            return new Block(DateTime.Now, null, "{}");
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
            Chain.Add(block);
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
    }
}
