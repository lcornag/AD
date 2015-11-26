using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo {

	public delegate void SaveDelegate(Articulo articulo);

	public partial class ArticuloView : Gtk.Window {
		public Articulo articulo;
		public SaveDelegate save;


		public ArticuloView () : base(Gtk.WindowType.Toplevel) {
			init ();
			save = ArticuloPersister.Insert;

		}

		public ArticuloView(object id) : base(WindowType.Toplevel) {
			articulo = ArticuloPersister.Load (id);
			init ();
			save = ArticuloPersister.Update;

		}

		public void init(){
			this.Build ();
			articulo.Nombre = entryNombre.Text ;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, articulo.Categoria);
			spinButtonPrecio.Value=Convert.ToDouble(articulo.Precio);

			saveAction.Activated += delegate {save(articulo);};

		}			
	}
}