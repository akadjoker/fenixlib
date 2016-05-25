using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using Pango;
using Fontify.Core;

namespace Fontify.GtkBinding
{
	public sealed class SystemFontCollectionNodeStore : NodeStore, ISystemFontCollectionModel
	{
		private  SystemFont[] fonts;
		private Context pangoContext;

		public SystemFontCollectionNodeStore(Context pangoContext) : 
			base (typeof (SystemFontTreeNode))
		{
			this.pangoContext = pangoContext;
			FetchFonts();
		}

		private void BuildFontCollection()
		{
			var pangoFontFamilies = pangoContext.Families;

			fonts = new SystemFont[pangoFontFamilies.Length];

			for (int n = 0; n < fonts.Length; n++)
			{
				var pangoFontFamily = pangoFontFamilies[n];
				fonts[n] = new SystemFont(pangoFontFamily.Name);
			}
		}

		private void AddNodes()
		{
			foreach (var font in fonts)
			{
				this.AddNode(new SystemFontTreeNode(font.Name));
			}			
		}

		public void FetchFonts()
		{
			Clear();
			BuildFontCollection();
			AddNodes();
		}

		public IEnumerable<SystemFont> Fonts => fonts.AsEnumerable();
	}
}

