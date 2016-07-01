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
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using FenixLib.Core;
using static FenixLib.IO.NativeFormat;

namespace FenixLib.IO
{
    /// <summary>
    /// <see cref="NativeDecoder{T}"/> base class for all native formats decoders. It defines
    /// common behaviour and defines a set of template methods that derivated classes must
    /// implement.
    /// </summary>
    /// <remarks>
    /// All native formats for fonts, graphics, graphic collections and palettes have
    /// similarities in how they are read from disk:
    /// <list type="bullet">
    ///     <item>Might or might not be compressed in GZip format</item>
    ///     <item>Have a magic indicating the type of the file</item>
    ///     <item>Have an specific sequence of bytes called terminator</item>
    ///     <item>Have a byte indicating the version of the file</item>
    /// </list>
    /// The <see cref="NativeDecoder{T}"/> takes care of the communalities and leave the
    /// read of the body to the derivated classes.
    /// </remarks>
    /// <typeparam name="T">The </typeparam>
	public abstract class NativeDecoder<T> : IDecoder<T>
    {

        public IEnumerable<string> SupportedExtensions { get; private set; }

        private readonly INativeDecoderHelper<T> streamDecoderHelper;

        protected NativeDecoder (IEnumerable<string> supportedExtensions,
            INativeDecoderHelper<T> streamDecoderHelper )
        {
            SupportedExtensions = supportedExtensions;
            this.streamDecoderHelper = streamDecoderHelper;
        }

        public T Decode ( Stream input )
        {
            return streamDecoderHelper.Decode ( input );
        }

        /// <summary>
        /// Attempts to decode <paramref name="input"/> into <paramref name="decoded"/>.
        /// Returns whether the operation succeded or not.
        /// </summary>
        /// <param name="input">The input from which to read.</param>
        /// <param name="decoded">When this method returns, the result of decoding the
        /// <see cref="Stream"/>. If the decoding fails, <paramref name="decoded"/> will
        /// contain the default value of <typeparamref name="T"/>.</param>
        /// <returns>True if the decoding was successful. Otherwise false.</returns>
        public bool TryDecode ( Stream input, out T decoded )
        {
            try
            {
                decoded = Decode ( input );
                return true;
            }
            catch ( Exception )
            {
                decoded = default ( T );
                return false;
            }
        }
    }
}
