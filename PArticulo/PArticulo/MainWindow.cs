using System;
using System.Collections.Generic;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{

		Build ();


		Console.WriteLine ("MainWindow ctor.");
		MySqlConnection mySqlConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
		);
		mySqlConnection.Open ();

		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from articulo";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

		string[] columnNames = getColumnNames (mySqlDataReader);

		for (int index = 0; index< columnNames.Length; index++) {
			treeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);
		}

		Type[] types = getTypes (mySqlDataReader.FieldCount);
		ListStore listStore = new ListStore (types);

		while (mySqlDataReader.Read()) {
			string[] values = getValues (mySqlDataReader);
			listStore.AppendValues (values);
		}

		treeView.Model = listStore;


		mySqlDataReader.Close ();
		mySqlConnection.Close ();

	}

	private string[] getColumnNames(MySqlDataReader mySqlDataReader){
		List<String> columnNames = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int index = 0; index < count; index++){
			columnNames.Add(mySqlDataReader.GetName(index));
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

	private string[] getValues(MySqlDataReader mySqlDataReader){
		List<string> values = new List<string> ();
		int count = mySqlDataReader.FieldCount;
		for (int index = 0; index < count; index++) {
			values.Add(mySqlDataReader[index].ToString());
		}
		return values.ToArray ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}