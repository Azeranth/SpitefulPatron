using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpitefulPatron
{
    public class UnalignedField : Field
    {
        public UnalignedField()
        {
            length = delegate () { return staticLength; };
            content = delegate () { return JoinSubfields(); };
        }

        public UnalignedField(int _length)
        {
            staticLength = _length;
            length = delegate () { return staticLength; };
            content = delegate () { return JoinSubfields(); };
        }

        private bool isBigEndian = true;
        private List<Func<BitArray>> bitArrays = new List<Func<BitArray>>();

        public bool IsBigEndian { get => isBigEndian; set => isBigEndian = value; }
        public List<Func<BitArray>> Subfields { get => bitArrays; set => bitArrays = value; }

        public byte[] JoinSubfields()
        {
            int len = length();
            byte[] rtn = new byte[len];
            BitArray target = new BitArray(len * 8);
            int read = 0;
            foreach (var bitArray in bitArrays.Select(n => n()))
            {
                foreach (bool bit in bitArray)
                {
                    target[read++] = bit;
                }
            }
            target = new BitArray(target.Cast<bool>().Reverse().ToArray());
            target.CopyTo(rtn, 0);
            rtn = rtn.Reverse().ToArray();
            return rtn;
        }
    }
}
