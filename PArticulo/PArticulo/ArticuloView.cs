using System;
using Gtk;
using System.Collections;
using SerpisAd;
using PArticulo;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

			entryNombre.Text = "nuevo";

			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			CellRendererText crt = new CellRendererText ();
			comboBoxCategoria.PackStart (crt,false);
			comboBoxCategoria.SetCellDataFunc(crt,
				delegate(CellLayout cell_layout, CellRenderer CellView, TreeModel tree_model, TreeIter iter){
				IList row = (IList)tree_model.GetValue(iter, 0);
				crt.Text =row[1].ToString();
				});
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows) {
				listStore.AppendValues (row);
			}

			comboBoxCategoria.Model = listStore;

		}
	}
}