using System;
using MySql.Data.MySqlClient;

namespace PMySql
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			MySqlConnection mySqlConnection = new MySqlConnection(
				"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
				);

			mySqlConnection.Open ();

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = "select * from articulo";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			showColumnNames (mySqlDataReader);

			show (mySqlDataReader);


			mySqlDataReader.Close ();
			mySqlConnection.Close ();
		}

		private static void showColumnNames(MySqlDataReader mySqlDataReader) {
			Console.WriteLine ("Columnas:");
			for (int i = 0; i < mySqlDataReader.FieldCount; i++)
			{
				Console.WriteLine("nº = " + i + " // nombre: " + mySqlDataReader.GetName (i));
			}
			Console.WriteLine ("");
		}

		private static void show(MySqlDataReader mySqlDataReader) {
			while(mySqlDataReader.Read()){
				Console.WriteLine ("ID:"+mySqlDataReader ["id"].ToString ());
				Console.WriteLine ("nombre:"+mySqlDataReader["nombre"].ToString());
				Console.WriteLine ("Categoria:"+mySqlDataReader ["categoria"].ToString ());
				Console.WriteLine ("Precio:"+mySqlDataReader["precio"].ToString());
				Console.WriteLine ("");
			}
		}
	}
}