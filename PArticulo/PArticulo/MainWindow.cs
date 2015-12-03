using System;
using Gtk;
using System.Data;
using SerpisAd;
using PArticulo;
using System.Collections;

public partial class MainWindow: Gtk.Window {	

	public MainWindow (): base (Gtk.WindowType.Toplevel) {

		Build ();
		Title = "Artículo";
		fillTreeView ();

		newAction.Activated += delegate {
			new ArticuloView ();
		};

		refreshAction.Activated += delegate {
			fillTreeView ();
		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetId (treeView);
			new ArticuloView(id);
		};

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine("click en deleteAction id={0}",id);
			delete(id);
		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine ("treeView.Selection.Changed ha ocurrido");
			bool isSelected = TreeViewHelper.IsSelected(treeView);
			deleteAction.Sensitive = isSelected;
			editAction.Sensitive = isSelected;
		};

		editAction.Sensitive = false;
		deleteAction.Sensitive = false;
	}
	private void delete(object id){
		if (!WindowHelper.ConfirmDelete (this)) {
			return;
		}
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from articulo where id = @id";
		DbCommandHelper.AddParameter (dbCommand, "id", id);
		dbCommand.ExecuteNonQuery ();
		fillTreeView ();
		
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