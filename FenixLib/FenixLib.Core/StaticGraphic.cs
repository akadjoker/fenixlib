﻿/*  Copyright 2016 Darío Cutillas Carrillo
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

namespace FenixLib.Core
{
    public class StaticGraphic : IGraphic
    {
        public int Width { get; }
        public int Height { get; }
        public Palette Palette { get; } = null;
        public byte[] PixelData { get; }
        public GraphicFormat GraphicFormat { get; }

        public StaticGraphic ( GraphicFormat graphicFormat, int width, int height,
            byte[] pixelData, Palette palette = null )
        {
            if ( width <= 0 )
            {
                throw new ArgumentOutOfRangeException (
                    "width", width, "Negative values are not accepted." );
            }

            if ( height <= 0 )
            {
                throw new ArgumentOutOfRangeException (
                    "height", height, "Negative values are not accepted." );
            }

            if ( ( graphicFormat == GraphicFormat.RgbIndexedPalette ) != ( palette != null ) )
            {
                throw new ArgumentException ( "A palette is required if, and only if, "
                    + "graphicFormat == GraphicFormat.RgbIndexedPalette.", "palette" );
            }

            if ( pixelData == null )
            {
                throw new ArgumentNullException ( "pixelData" );
            }

            if ( pixelData.Length != graphicFormat.PixelsBytesForSize ( width, height ) )
            {
                throw new ArgumentException ( "Insufficient size of 'pixelData' for the"
                    + "specified graphic format.", "pixelData" );
            }

            Width = width;
            Height = height;
            Palette = palette;
            GraphicFormat = graphicFormat;
            PixelData = pixelData;
        }
    }
}
