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


			return null;
		}
	}
}

