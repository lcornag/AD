using System;
using System.Data;
using System.Collections.Generic;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{

		Build ();


		Console.WriteLine ("MainWindow ctor.");
		IDbConnection dbConnection = new MySqlConnection (
			"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
		);
		dbConnection.Open ();

		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from articulo";

		IDataReader dataReader = dbCommand.ExecuteReader ();

		string[] columnNames = getColumnNames (dataReader);

		for (int index = 0; index< columnNames.Length; index++) {
			treeView.AppendColumn (columnNames [index], new CellRendererText (), "text", index);
		}

		Type[] types = getTypes (dataReader.FieldCount);
		ListStore listStore = new ListStore (types);

		while (dataReader.Read()) {
			string[] values = getValues (dataReader);
			listStore.AppendValues (values);
		}

		treeView.Model = listStore;


		dataReader.Close ();
		dbConnection.Close ();

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