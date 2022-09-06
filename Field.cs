using System;
using System.Collections.Generic;
using System.Text;

namespace SpitefulPatron
{
    public class Field
    {
        public Field()
        {
            staticLength = 0;
            staticContent = new byte[0];
            length = delegate () { return staticLength; };
            content = delegate () { return staticContent; };
        }

        public Field(byte[] _content)
        {
            staticLength = _content.Length;
            staticContent = _content;
            length = delegate () { return staticLength; };
            content = delegate () { return staticContent; };
        }

        public Field(int _length)
        {
            staticLength = _length;
            staticContent = new byte[_length];
            length = delegate () { return staticLength; };
            content = delegate () { return staticContent; };
        }

        public Field(byte[] _content, int _length, bool _isPadRight= true)
        {
            staticLength = _length;
            staticContent = new byte[_length];
            //Copy _content starting from 0 to staticContent 
            //starting at 0 if _isPadRight or _content length before end of staticContent if not
            //for _length or _content.Length whichever is smaller
            Array.Copy(_content, 0, staticContent, _isPadRight ? 0 : _length - _content.Length, _length < _content.Length ? _length : _content.Length);
            length = delegate () { return staticLength; };
            content = delegate () { return staticContent; };
        }

        protected int staticLength = 1;
        protected byte[] staticContent = new byte[] { 0 };

        protected Func<int> length;
        protected Func<byte[]> content;

        public Func<int> Length { get => length; set => length = value; }
        public Func<byte[]> Content { get => content; set => content = value; }
    }
}
