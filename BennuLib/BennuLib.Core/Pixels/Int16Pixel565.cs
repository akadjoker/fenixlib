using System;

namespace Bennu
{
    [Serializable ()]
    public class Int16Pixel565 : AbstractPixel
    {

        // TODO: It is certainly possible to speed up this colo conversions by 
        // keeping the list of all 65535 colors possible in memmory (65Kb)...

        private readonly ushort _value;

        public Int16Pixel565 ( byte r, byte g, byte b ) : this ( RgbToUShort ( r, g, b ) )
        {
        }

        internal Int16Pixel565 ( ushort value )
        {
            _value = value;
        }

        public override int Alpha
        {
            get { return _value == 0 ? 255 : 0; }
        }

        public override int Argb
        {
            get { return ( Alpha << 24 | Red << 16 | Green << 8 | Blue ); }
        }

        public override int Blue
        {
            get { return _value & 0x1f; }
        }

        public override int Green
        {
            get { return _value >> 5 & 0x3f; }
        }

        public override int Red
        {
            get { return _value >> 11 & 0x1f; }
        }

        public override int Value
        {
            get { return _value; }
        }

        public override bool IsTransparent
        {
            get { return Value == 0; }
        }

        public override AbstractPixel GetTransparentCopy ()
        {
            return new Int16Pixel565 ( 0 );
        }

        public override AbstractPixel GetOpaqueCopy ()
        {
            if ( _value == 0 )
            {
                return new Int16Pixel565 ( 1 );
            }
            else {
                return new Int16Pixel565 ( _value );
            }
        }

        private static ushort RgbToUShort ( byte r, byte g, byte b )
        {
            return Convert.ToUInt16 ( ( r >> 3 ) << 11 | ( g >> 2 ) << 5 | b >> 2 );
        }

        internal override byte[] GetRawValueBytes ()
        {
            return BitConverter.GetBytes ( _value );
        }

        public override bool IsPalettized ()
        {
            return false;
        }
    }
}
