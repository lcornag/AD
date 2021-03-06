using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace PArticulo
{
	public class PersisterHelper
	{
		public static QueryResult Get(string selectText) {
			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = selectText;
			IDataReader dataReader = dbCommand.ExecuteReader ();
			QueryResult queryResult = new QueryResult ();
			queryResult.ColumnNames = getColumnNames (dataReader);
			List<IList> rows = new List<IList> ();
			while (dataReader.Read()) {
				rows.Add ();
			}
			queryResult.Rows = rows;
			//TODO completar
			dataReader.Close ();
			return queryResult;
		}

		private static string[] getColumnNames(IDataReader dataReader) {
			List<string> columnNames = new List<string> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++) {
				columnNames.Add (dataReader.GetName (index));
			}
			return columnNames.ToArray ();
		}
	}
}

