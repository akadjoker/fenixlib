using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
    public MainWindow ()
        : base ( Gtk.WindowType.Toplevel )
    {
        Build ();
        textview1.Buffer.Changed += new EventHandler ( OnTextChanged );
    }

    protected void OnDeleteEvent ( object sender, DeleteEventArgs a )
    {
        Application.Quit ();
        a.RetVal = true;
    }

    protected void OnTextChanged (object o, EventArgs args)
    {
        // var dlg = new MessageDialog ( this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Lala" );
        // dlg.Run ();
        // dlg.Destroy ();
    }
}
