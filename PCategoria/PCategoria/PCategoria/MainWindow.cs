using System;
using Gtk;

using SerpisAd;
using PCategoria;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("MainWindow ctor.");	

		newAction.Activated += delegate {
			new PCategoria.Window ();
		};
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		Application.Quit ();
		a.RetVal = true;
	}
}