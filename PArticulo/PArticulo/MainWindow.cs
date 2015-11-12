using System;
using Gtk;

using SerpisAd;
using PArticulo;
using System.Collections;

public partial class MainWindow: Gtk.Window {	

	public MainWindow (): base (Gtk.WindowType.Toplevel) {

		Build ();
		Title = "Artículo";
		Console.WriteLine ("MainWindow ctor.");
		QueryResult queryResult = PersisterHelper.Get ("select * from articulo");
		TreeViewHelper.Fill (treeView, queryResult);

		newAction.Activated += delegate {
			new ArticuloView ();
		};

		refreshAction.Activated += delegate {
			fillTreeView ();
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine("click en deleteAction id={0}",id);
			delete(id);
		};
		treeView.Selection.Changed += delegate {
			Console.WriteLine ("treeView.Selection.Changed ha ocurrido");
			deleteAction.Sensitive = TreeViewHelper.IsSelected(treeView);
		};

		deleteAction.Sensitive = false;
	}
	private void delete(object IDictionary){
		if (WindowHelper.ConfirmDelete(this)) {
			Console.WriteLine ("Dice que eliminar si");
		}
	}

	private void fillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("Select * from articulo");
			TreeViewHelper.Fill(treeView, queryResult);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)	{

		Application.Quit ();
		a.RetVal = true;
	}



}