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
using System.Collections;
using System.Collections.Generic;

namespace FenixLib.Core.Tests.IntegrationTests
{
    internal abstract class ComparableAsset : ISpriteAsset
    {
        ISpriteAsset decorated;

        public bool ComparePalette { get; set; } = false;

        public bool CompareFormat { get; set; } = false;

        public ComparableAsset ( ISpriteAsset decorated )
        {
            this.decorated = decorated;
        }

        public abstract SpriteComparer GetElementComparer ();

        public virtual bool Equals ( ISpriteAsset asset )
        {
            if ( ReferenceEquals ( asset, null ) )
                return false;

            if ( CompareFormat && GraphicFormat != asset.GraphicFormat )
                return false;

            if ( ComparePalette && Palette != asset.Palette )
                return false;

            foreach ( SpriteAssetElement element in Sprites )
            {

                if ( ! GetElementComparer ().Equals ( element, asset[element.Id] ) )
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode ()
        {
            return GraphicFormat.GetHashCode () ^ Sprites.Count.GetHashCode ();
        }

        public override bool Equals ( object obj )
        {
            ISpriteAsset objAsAsset = obj as ISpriteAsset;
            if ( objAsAsset == null )
            {
                return false;
            }
            else
            {
                return Equals ( objAsAsset );
            }
        }

        public SpriteAssetElement this[int id] => decorated[id];

        public GraphicFormat GraphicFormat => decorated.GraphicFormat;

        public IEnumerable<int> Ids => decorated.Ids;

        public Palette Palette => decorated.Palette;

        public ICollection<SpriteAssetElement> Sprites => decorated.Sprites;

        public void Add ( int id, ISprite sprite )
        {
            decorated.Add ( id, sprite );
        }

        public IEnumerator<SpriteAssetElement> GetEnumerator () => decorated.GetEnumerator ();

        public int GetFreeId () => decorated.GetFreeId ();

        public void Update ( int id, ISprite sprite )
        {
            decorated.Update ( id, sprite );
        }

        IEnumerator IEnumerable.GetEnumerator () => GetEnumerator ();
    }
}