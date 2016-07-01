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
using System.Text;
using System.Threading.Tasks;

using FenixLib.Core;
using static FenixLib.IO.NativeFormat;

namespace FenixLib.IO
{
    public class NativeDecoderHelper<T> : INativeDecoderHelper<T>
    {
        private readonly INativeFormatBodyDecoder<T> bodyDecoder;
        private readonly ValidateHeaderAction validateHeaderAction;

        // TODO: There must be a better way to encapsulate this
        protected virtual NativeFormatReader CreateNativeFormatReader ( Stream stream )
        {
            // An alternative to this template method would be an optional parameter
            // with a factory object that creates the NativeFormatReader
            return new BinaryNativeFormatReader ( stream );
        }

        public NativeDecoderHelper ( ValidateHeaderAction validateHeaderAction,
            INativeFormatBodyDecoder<T> bodyDecoder )
        {
            this.validateHeaderAction = validateHeaderAction;
            this.bodyDecoder = bodyDecoder;
        }

        public T Decode ( Stream input )
        {
            if ( input == null )
            {
                throw new ArgumentNullException ( nameof ( input ) );
            }

            /* Native formats support GZip compression transparently
             * This function will read the first two bytes of the file and determine if it
             * is a GZip file, in which case it will use a GZipStream object to read it.
             */

            Header header;
            Stream bodyStream = null;

            byte[] headerBytes = new byte[8];

            if ( input.Read ( headerBytes, 0, 2 ) != 2 )
                throw new UnsuportedFileFormatException (); // TODO: Customize

            if ( HeaderIsGZip ( headerBytes ) )
            {
                // Concatenate a memory stream containing a GZipHeader with the input stream
                // so as to be able to use a GZipStream to read the bytes of the body
                using ( var GzipHeaderStream = new MemoryStream ( new byte[] { 31, 139 } ) )
                {
                    Stream concatenated = new ConcatenatedStream ( GzipHeaderStream, input );
                    bodyStream = new GZipStream ( concatenated, CompressionMode.Decompress );

                    if ( bodyStream.Read ( headerBytes, 0, 8 ) != 8 )
                        throw new UnsuportedFileFormatException ();
                }
            }
            else
            {
                // Read the remaining 6 bytes of the header
                if ( input.Read ( headerBytes, 2, 6 ) != 6 )
                    throw new UnsuportedFileFormatException ();

                bodyStream = input;
            }

            using ( MemoryStream headerStream = new MemoryStream ( headerBytes ) )
            {
                using ( var reader = CreateNativeFormatReader ( headerStream ) )
                {
                    header = reader.ReadHeader ();

                }
            }

            validateHeaderAction ( header );

            using ( var reader = new BinaryNativeFormatReader ( bodyStream ) )
            {
                return bodyDecoder.DecodeBody ( header, reader );
            }
        }

        /// <summary>
        /// GZip files start always with bytes {31, 139}. This function
        /// will check if the argument matches that criteria.
        /// </summary>
        /// <param name="header">An array of bytes containing at least two elements</param>
        /// <returns>True if the first two bytes <paramref name="header"/> are
        /// that of GZip format.</returns>
        private static bool HeaderIsGZip ( byte[] header )
        {
            return header.Length >= 2 & header[0] == 31 & header[1] == 139;
        }
    }
}
