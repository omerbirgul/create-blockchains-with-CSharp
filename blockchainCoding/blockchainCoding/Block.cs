﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blockchainCoding
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }

        public Block(DateTime timeStamp, string previousHash, string data)
        {
            this.TimeStamp = timeStamp;
            this.PreviousHash = previousHash;
            this.Data = data;
            this.Index = 0;
            this.Hash = CalculateHash();
        }


        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inByte = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");
            byte[] ourByte = sha256.ComputeHash( inByte );
            return Convert.ToBase64String( ourByte );
        }
    }
}
