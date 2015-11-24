using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo {
	public delegate void SaveDelegate();
	public partial class ArticuloView : Gtk.Window {

		private object id = null;
		private string nombre = "";
		private object categoria = null;
		private decimal precio = 0;


		public ArticuloView () : base(Gtk.WindowType.Toplevel) {

			init ();
			saveAction.Activated += delegate {	insert();	};
		}

		public ArticuloView(object id):base(WindowType.Toplevel) {
			this.id = id;
			Load ();
			init ();
			saveAction.Activated += delegate {	update();	};
		}

		public void init(){
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult,categoria);
			spinButtonPrecio.Value=Convert.ToDouble(precio);
			//saveAction.Activated += delegate {	save();	};

		}

		private void Load() {

			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader();

			if (!dataReader.Read ()) {
				//TODO throw exception
				dataReader.Close ();
				return;
			}
			nombre = (string)dataReader ["nombre"];
			categoria = dataReader ["categoria"];
			if (categoria is DBNull) {
				categoria = null;
			}
			precio = (decimal)dataReader ["precio"];
			dataReader.Close ();
		}

		private void insert(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre, categoria, precio) " +
				"values (@nombre, @categoria, @precio)";

			string nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			decimal precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);

			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

//		private void save(){
//			if(id==null){
//				insert();
//			}
//			else{
//				update();
//			}

		private void update(){
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre=@nombre, categoria=@categoria, precio=@precio where id = @id";

			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId (comboBoxCategoria);
			precio = Convert.ToDecimal(spinButtonPrecio.Value);

			DbCommandHelper.AddParameter (dbCommand, "id", id);
			DbCommandHelper.AddParameter (dbCommand, "nombre", nombre);
			DbCommandHelper.AddParameter (dbCommand, "categoria", categoria);
			DbCommandHelper.AddParameter (dbCommand, "precio", precio);


			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}
			
	}
}