using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public class ArticuloPersister
	{
		static object id=null;
		static object categoria=null;
		static string nombre=null;
		static decimal precio=0;

		public ArticuloPersister ()
		{
		}


		public static Articulo Load(object id){

			Articulo articulo = new Articulo ();

			articulo.Id = id;
			articulo.Categoria = categoria;
			articulo.Nombre = nombre;
			articulo.Precio = precio;

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();

			if (!dataReader.Read ()) {
				//TODO throw exception
				dataReader.Close ();
				return articulo;
			}
			articulo.Nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull) {
				categoria = null;
			}
			precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
			return articulo;
		}



		public static void Insert(Articulo art){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			nombre = art.Nombre;
			categoria = art.Categoria;
			precio = art.Precio;

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery ();

		}



		public static void Update(Articulo art){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio where id = @id";

			nombre = art.Nombre;
			categoria = art.Categoria;
			precio = art.Precio;

			DbCommandHelper.AddParameter (dbCommand, "id", id);//ID???
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);


			dbCommand.ExecuteNonQuery ();

		}


	}
}

