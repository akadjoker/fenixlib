using System;
using Gtk;
using Pango;
using Gdk;
using Cairo;
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

	protected void RenderTextCairo()
	{
		Cairo.Context cr = Gdk.CairoHelper.Create(previewDrawingArea.GdkWindow);
		cr.SetSourceRGB(0.1, 0.1, 0.1);

		cr.SelectFontFace("Purisa", FontSlant.Normal, FontWeight.Bold);
		cr.SetFontSize(13);

		cr.MoveTo(20, 30);
		cr.ShowText("Most relationships seem so transitory");
		cr.MoveTo(20, 60);
		cr.ShowText("They're all good but not the permanent one");
		cr.MoveTo(20, 120);
		cr.ShowText("Who doesn't long for someone to hold");
		cr.MoveTo(20, 150);
		cr.ShowText("Who knows how to love without being told");
		cr.MoveTo(20, 180);
		cr.ShowText("Somebody tell me why I'm on my own");
		cr.MoveTo(20, 210);
		cr.ShowText("If there's a soulmate for everyone");

		((IDisposable)cr.Target).Dispose();
		((IDisposable)cr).Dispose();
	}

	protected void RenderText3()
	{
		Cairo.Context cr = Gdk.CairoHelper.Create(previewDrawingArea.GdkWindow);
		/*var layout = Pango.CairoHelper.CreateLayout(cr);
		FontDescription fontDescription = FontDescription.FromString("Serif Bold 20");
		layout.FontDescription = fontDescription;
		layout.SetText("Hola");

		cr.NewPath();
		cr.MoveTo(0, 0);*/
		cr.LineWidth = 9;
		cr.SetSourceRGB(0, 0, 1.0);
		/*Pango.CairoHelper.UpdateLayout(cr, layout);
		Pango.CairoHelper.LayoutPath(cr, layout);*/
		cr.Rectangle(new Cairo.Rectangle(0, 0, 100, 100));
		cr.Fill();
	}

	protected void RenderText()
	{
		int width = previewDrawingArea.Allocation.Width;
		PangoRenderer renderer = Gdk.PangoRenderer.GetDefault(previewDrawingArea.Screen);
		renderer.Drawable = previewDrawingArea.GdkWindow;
		renderer.Gc = previewDrawingArea.Style.BlackGC;

		Pango.Context context = previewDrawingArea.CreatePangoContext();
		Pango.Layout layout = new Pango.Layout(context);

		layout.Width = Pango.Units.FromPixels(width);
		layout.SetText("Sample Text");

		FontDescription fontDescription = FontDescription.FromString("Serif Bold 20");
		layout.FontDescription = fontDescription;

		renderer.SetOverrideColor(RenderPart.Foreground, new Gdk.Color(200, 30, 30));
		layout.Alignment = Pango.Alignment.Center;
		renderer.DrawLayout(layout, 0, 0);
		
		renderer.SetOverrideColor(RenderPart.Foreground, Gdk.Color.Zero);
		renderer.Drawable = null;
		renderer.Gc = null;
	}

	protected void OnTestButtonPressEvent(object sender, EventArgs a)
	{
		RenderText3();
	}

	protected void OnSystemFontSelect(object o, Gtk.RowActivatedArgs args)
	{
		var model = systemFontsView.Model;
		TreeIter iter;
		model.GetIter(out iter, args.Path);
		var value = model.GetValue(iter, 0);
	}
}
