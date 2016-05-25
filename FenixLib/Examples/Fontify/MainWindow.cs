using System;
using Gtk;
using Fontify.GtkBinding;

public partial class MainWindow : Gtk.Window
{
	private SystemFontCollectionNodeStore systemFontsStore;
	SystemFontCollectionNodeStore SystemFontsStore
	{
		get
		{
			if (systemFontsStore == null)
			{
				systemFontsStore = new SystemFontCollectionNodeStore(this.PangoContext);
			}
			return systemFontsStore;
		}
	}

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();

		// Create a column with title Artist and bind its renderer to model column 0
		NodeCellDataFunc dataFunc = new NodeCellDataFunc(nodeCellDataFunc);
		systemFontsView.AppendColumn("Font", new Gtk.CellRendererText(), dataFunc);
		systemFontsView.NodeStore = SystemFontsStore;
		systemFontsView.HeadersVisible = false;

		systemFontsView.ShowAll();
	}

	private static void nodeCellDataFunc(TreeViewColumn tree_column, 
	                                     CellRenderer cell, ITreeNode node)
	{
		var fontNode = (SystemFontTreeNode)node;
		var textRenderer = (CellRendererText)cell;

		string fontName = fontNode.Font;

		textRenderer.Alignment = Pango.Alignment.Right;
		textRenderer.Font = fontName + " 16";

		textRenderer.Text = fontName;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
