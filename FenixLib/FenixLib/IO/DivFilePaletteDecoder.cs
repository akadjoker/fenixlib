/*  Copyright 2016 Darío Cutillas Carrillo
*
*   Licensed under the Apache License, Version 2.0 (the "License");
*   you may not use this file except in compliance with the License.
*   You may obtain a copy of the License at
*
*       http://www.apache.org/licenses/LICENSE-2.0
*
*   Unless required by applicable law or agreed to in writing, software
*   distributed under the License is distributed on an "AS IS" BASIS,
*   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*   See the License for the specific language governing permissions and
*   limitations under the License.
*/
using System;
using FenixLib.Core;
using static FenixLib.IO.NativeFormat;

namespace FenixLib.IO
{
    // TODO: After regractoring of NativeDecoder, this and other decoder classes merely
    // delegate on the BodyDecoders. A Factory to create different type of decoders might be
    // more appropiate for exposing functionality of the API.
    public class DivFilePaletteDecoder : NativeDecoder<Palette>
    {
        private static string[] knownExtensions = { "pal", "map", "fpg", "fnt" };
        private static string[] knownMagics = { "pal", "map", "fpg", "fnt" };

        private static NativeDecoderHelper<Palette> decoderHelper =
            new NativeDecoderHelper<Palette> (
                ValidateHeaderActionFactory.Create ( knownMagics, 0 ),
                new DivFilePaletteBodyDecoder ()
                );

        public DivFilePaletteDecoder() : this ( decoderHelper )
        {
        }

        public DivFilePaletteDecoder (INativeDecoderHelper<Palette> decoderHelper) :
            base ( knownExtensions, decoderHelper )
        {
        }
    }
}
