using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using MySql.Data.MySqlClient;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{

		Build ();
		Console.WriteLine ("MainWindow ctor.");


		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";

		IDataReader dataReader = dbCommand.ExecuteReader ();

		string[] columnNames = getColumnNames (dataReader);

		CellRendererText cellRendererText = new  CellRendererText ();

		for (int index = 0; index< columnNames.Length; index++) {
			int column = index;
			treeView.AppendColumn (columnNames [index], cellRendererText, 
				delegate(TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
				IList row = (IList)tree_model.GetValue(iter, 0);
				cellRendererText.Text = "[" + row[column].ToString() + "]";
				});
			//treeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);
		}

		//Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (typeof(IList));

		while (dataReader.Read()) {
			IList values = getValues (dataReader);
			listStore.AppendValues (values);

		}

		treeView.Model = listStore;


		dataReader.Close ();
		App.Instance.DbConnection.Close ();

	}

	private string[] getColumnNames(IDataReader dataReader){
		List<String> columnNames = new List<string> ();
		int count = dataReader.FieldCount;
		for (int index = 0; index < count; index++){
			columnNames.Add(dataReader.GetName(index));
		}
		return columnNames.ToArray();
	}

	private Type[] getTypes(int count){
		List<Type> types = new List<Type> ();
		for (int index = 0; index < count; index++) {
			types.Add (typeof(String));
		}
		return  types.ToArray ();
	}

	private string[] getValues(IDataReader dataReader){
		List<string> values = new List<string> ();
		int count = dataReader.FieldCount;
		for (int index = 0; index < count; index++) {
			values.Add(dataReader[index].ToString());
		}
		return values.ToArray ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}