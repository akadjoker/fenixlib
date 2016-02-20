using System;
using System.Linq;

namespace Bennu.IO
{
    public class FpgSpriteAssetEncoder : NativeEncoder<SpriteAsset>
    {
        private const int Version = 0x00;

        protected override void WriteNativeFormatBody ( SpriteAsset asset, NativeFormatWriter writer )
        {
            if ( asset.Palette != null )
            {
                writer.Write ( asset.Palette );
                writer.WriteReservedPaletteGammaSection ();
            }

            foreach ( var sprite in asset )
            {
                // TODO: Will fail for no control points defined
                var maxPivotPointId = Convert.ToUInt16 ( sprite.PivotPoints.Max ( p => p.Id ) );
                var maplen = Convert.ToUInt32 ( 64
                    + sprite.Width * sprite.Height * asset.Depth / 8
                    + maxPivotPointId * 4 );

                writer.Write ( maplen );
                writer.WriteAsciiZ ( sprite.Description, 32 );
                writer.WriteAsciiZ ( "SpritePocket", 12 );
                writer.Write ( Convert.ToUInt32 ( sprite.Width ) );
                writer.Write ( Convert.ToUInt32 ( sprite.Height ) );
                writer.Write ( Convert.ToUInt32 ( maxPivotPointId ) );
                writer.Write ( sprite.PivotPoints );
                writer.Write ( sprite.Pixels );
            }
        }

        private bool HasPalette ( SpriteAsset asset )
        {
            return false;
            // TODO
        }

        protected override string GetFileId ( SpriteAsset asset )
        {
            // TODO
            switch ( asset.Depth )
            {
                case 8:
                    return "fpg";
                case 16:
                    return "f16";
                case 32:
                    return "f32";
                default:
                    // TODO more specific
                    throw new ArgumentException ();
            }
        }

        protected override byte GetLastHeaderByte ( SpriteAsset what )
        {
            return Version;
        }
    }
}
