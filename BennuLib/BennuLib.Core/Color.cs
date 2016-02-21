﻿namespace Bennu
{
    public struct Color
    {
        public readonly int r;
        public readonly int g;

        public readonly int b;
        public Color ( int r, int g, int b )
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public bool Equals ( Color color )
        {
            return r == color.r && g == color.g && b == color.b;
        }

        public override bool Equals ( object obj )
        {
            if ( !( obj != null && obj is Color ) )
                return false;

            return Equals ( ( Color ) obj );
        }

        public override int GetHashCode ()
        {
            return r << 16 & g << 8 & b;
        }

        public static bool operator == ( Color colorA, Color colorB )
        {
            if ( ReferenceEquals ( colorA, null ) )
                return ReferenceEquals ( colorB, null );

            return colorA.Equals ( colorB );
        }

        public static bool operator != ( Color colorA, Color colorB )
        {
            return !( colorA == colorB );
        }
    }
}
