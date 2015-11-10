using System;
using Gtk;

using SerpisAd;
using PArticulo;
using System.Collections;

public partial class MainWindow: Gtk.Window {	

	public MainWindow (): base (Gtk.WindowType.Toplevel) {

		Build ();
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
			object id = GetId(treeView);
			Console.WriteLine("click en deleteAction id={0}",GetId(treeView));
		};
		treeView.Selection.Changed += delegate {
			Console.WriteLine ("treeView.Selection.Changed ha ocurrido");
			deleteAction.Sensitive = GetId(treeView) != null;
		};
	}
	public static object GetId(TreeView treeView){
		TreeIter treeIter;
		if (!treeView.Selection.GetSelected (out treeIter)) {
			return null;
		}
		treeView.Selection.GetSelected(out treeIter);
		IList row = (IList)treeView.Model.GetValue(treeIter,0);
		if (row == null) {
			return null;
		}
		return row [0];
	}
	private void fillTreeView(){
		QueryResult queryResult = PersisterHelper.Get ("Select * from articulo");
			TreeViewHelper.Fill(treeView, queryResult);
	}

	/*private void delete(){
		String deleteSql = "delete from articulo where id=@id";
		String.Format (deleteSql);
	}*/
		//newAction.Activated += newActionActivated;
	
		
	//	void newActionActivated (object sender, EventArgs e)
	//	{
	//		new ArticuloView ();
	//	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)	{

		Application.Quit ();
		a.RetVal = true;
	}

}