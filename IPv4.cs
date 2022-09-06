using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpitefulPatron
{
    public static partial class Extensions
    {
        public static ushort InternetChecksum(this IEnumerable<byte> value)
        {
            byte[] buffer = value.ToArray();
            int length = buffer.Length;
            int i = 0;
            UInt32 sum = 0;
            UInt32 data = 0;
            while (length > 1)
            {
                data = 0;
                data = (UInt32)(
                ((UInt32)(buffer[i]) << 8)
                |
                ((UInt32)(buffer[i + 1]) & 0xFF)
                );

                sum += data;
                if ((sum & 0xFFFF0000) > 0)
                {
                    sum = sum & 0xFFFF;
                    sum += 1;
                }

                i += 2;
                length -= 2;
            }

            if (length > 0)
            {
                sum += (UInt32)(buffer[i] << 8);
                //sum += (UInt32)(buffer[i]);
                if ((sum & 0xFFFF0000) > 0)
                {
                    sum = sum & 0xFFFF;
                    sum += 1;
                }
            }
            sum = ~sum;
            sum = sum & 0xFFFF;
            return (UInt16)sum;
        }
    }
    public class IPv4 : Protocol
    {
        public IPv4()
        {
            fields = new List<Field>
            {
                new UnalignedField(1){ Subfields = new List<Func<BitArray>>{
                    delegate(){ return new BitArray(new bool[] { false,true, false,false}); },
                    delegate(){ return new BitArray(new bool[]{false,true, false, true}); } }},
                new Field(1),
                new Field(2){ Content = delegate() {return BitConverter.GetBytes((short)this.Length()).Reverse().ToArray(); } },
                new Field(BitConverter.GetBytes((short)1234).Reverse().ToArray()),
                new Field(BitConverter.GetBytes((short)0)),
                new Field(new byte[]{ 128}),
                new Field(new byte[]{ 1}),
                new Field(2){ Content = delegate (){return BitConverter.GetBytes(fields.SelectMany(n => n == fields[7] ? new byte[n.Length()] : n.Content()).Reverse().InternetChecksum());}},
                new Field(new byte[]{ 127,0,0,1}),
                new Field(new byte[]{ 127,0,0,2})
            };
            
        }
    }
}
