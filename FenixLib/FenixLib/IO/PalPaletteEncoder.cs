/*  Copyright 2016 Dar�o Cutillas Carrillo
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
using FenixLib.Core;

namespace FenixLib.IO
{
	public class PalPaletteEncoder : NativeEncoder<Palette>
	{
        private const int version = 0x00;

		protected override byte GetLastHeaderByte(Palette palette) => version;

		protected override void WriteNativeFormatBody(Palette palette, 
            NativeFormatWriter writer)
		{
			writer.Write(palette);
			writer.WritePaletteGammaSection();
		}

		protected override string GetFileMagic(Palette palette)
		{
			return "pal";
		}
	}
}
