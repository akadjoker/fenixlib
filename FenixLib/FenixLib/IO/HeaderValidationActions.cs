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
using System.Linq;
using FenixLib.Core;

namespace FenixLib.IO
{
    public delegate void ValidateHeaderAction ( NativeFormat.Header header );

    static class ValidateHeaderActionFactory
    {
        private static ValidateHeaderAction  KnownMagic (string[] validMagics)
            => header =>
        {
            if ( ! validMagics.Contains ( header.Magic ) )
            {
                throw new UnsuportedFileFormatException (); // TODO: Customize message
            }
        };

        private static ValidateHeaderAction Version ( byte maxVersion )
            => header =>
        {
            if ( ! ( header.LastByte <= maxVersion ) )
            {
                throw new UnsuportedFileFormatException (); // TODO: Customize message
            }
        };

        private static ValidateHeaderAction Terminator ()
            => header =>
        {
            if ( !( header.IsTerminatorValid () ) )
            {
                throw new UnsuportedFileFormatException (); // TODO customize message
            }
        };

        public static ValidateHeaderAction Create (
            string[] validMagics, byte? maxVersion)
        {
            ValidateHeaderAction action = header =>
            {
                KnownMagic ( validMagics ) ( header );
                Terminator () ( header );

                if ( maxVersion.HasValue )
                    Version ( maxVersion.Value ) ( header );
            };

            return action;
        }

    }
}
