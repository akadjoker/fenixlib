using System;
using System.Collections.Generic;
using Gtk;
using Fontify.Core;

namespace Fontify.GtkBinding
{
	[Gtk.TreeNode(ListOnly = true)]
	public class SystemFontTreeNode : Gtk.TreeNode
	{
		public SystemFontTreeNode(string fontName)
		{
			Font = fontName;
		}

		[Gtk.TreeNodeValue(Column = 0)]
		public string Font;
	}
}

