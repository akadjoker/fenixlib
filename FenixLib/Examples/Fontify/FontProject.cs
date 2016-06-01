using System;
using System.Collections.Generic;

namespace Fontify
{
	public abstract class Charset
	{
		public abstract IEnumerable<char> Characters();
	}

	public class ExampleCharset : Charset
	{
		public override IEnumerable<char> Characters()
		{
			yield return 'a';
			yield return 'b';
			yield return 'c';
		}
	}

	public abstract class GlyphGenerator
	{
		public abstract Gdk.Image Generate();
	}

	public abstract class GlyphFromFile
	{
		
	}

	public class PangoRenderProperties
	{
		public string FontDescription;
		
	}

	public class FontProject
	{
		Charset charset { get; set; }
		IDictionary<char, GlyphGenerator> glyphGenerators;

		public GlyphGenerator GetGeneratorForChar(char c)
		{
			GlyphGenerator generator;
			// TODO: Shall return defualt value
			return glyphGenerators.TryGetValue(c, out generator) ? generator : generator;
		}

		public void DefineGlyphGenerator(char c, GlyphGenerator glyphGenerator)
		{
			glyphGenerators[c] = glyphGenerator;
		}

		public FontProject(Charset charset)
		{
			this.charset = charset;
		}
	}
}

