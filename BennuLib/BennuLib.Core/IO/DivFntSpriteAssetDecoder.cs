using static Bennu.IO.NativeFormat;

namespace Bennu.IO
{
    public class DivFntFpgDecoder : NativeDecoder<SpriteAsset>
    {

        public override int MaxSupportedVersion { get; }

        protected override string[] KnownFileExtensions { get; }

        protected override string[] KnownFileMagics { get; }

        protected override SpriteAsset ReadBody ( Header header, NativeFormatReader reader )
        {

            Palette palette = reader.ReadPalette ();
            reader.ReadUnusedPaletteGamma ();

            int fontInfo = reader.ReadInt32 ();

            GlyphInfo[] characters = new GlyphInfo[256];
            for ( var n = 0 ; n <= 255 ; n++ )
            {
                characters[n] = reader.ReadLegacyFntGlyphInfo ();
            }

            SpriteAsset fpg = SpriteAsset.Create ( palette );

            foreach ( var character in characters )
            {
                var dataLength = character.Height * character.Width;

                if ( character.FileOffset == 0 | dataLength == 0 )
                    continue;

                var pixels = reader.ReadPixels ( header.Depth, character.Width, character.Height );
                var map = Sprite.Create ( (DepthMode) header.Depth, character.Width, 
                    character.Height, pixels );
                fpg.Add ( fpg.FindFreeId (), ref map );
            }

            return fpg;
        }
    }
}
