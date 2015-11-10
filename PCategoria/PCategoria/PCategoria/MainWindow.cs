using System;
using Gtk;

using SerpisAd;
using PCategoria;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel){
		Build ();
		Console.WriteLine ("MainWindow ctor.");	
		QueryResult qr = PersisterHelper.Get ("Select * from articulo");
		TreeViewHelper.Fill (treeView, qr);

		newAction.Activated += delegate {
			new PCategoria.Window ();
		};

		refreshAction.Activate += delegate {
			//TreeView.Fi
		};
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a) {
		Application.Quit ();
		a.RetVal = true;
	}
}