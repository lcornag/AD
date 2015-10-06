using System;
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

		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("categoria", new CellRendererText (), "text", 1);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 2);
		treeView.AppendColumn ("precio", new CellRendererText (), "text", 3);

		ListStore listStore = new ListStore (typeof(String), typeof(String), typeof(String), typeof(String));

		while (mySqlDataReader.Read()) {
			listStore.AppendValues (mySqlDataReader [0].ToString(), mySqlDataReader [1], mySqlDataReader [2].ToString(), mySqlDataReader [3].ToString());
		}

		treeView.Model = listStore;


		mySqlDataReader.Close ();
		mySqlConnection.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}