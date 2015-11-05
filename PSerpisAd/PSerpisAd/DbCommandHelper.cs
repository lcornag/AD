using System;
using System.Data;

namespace PArticulo {
	public class DbCommandHelper {
	
		public DbCommandHelper { 
				public static void addParameter(IDbCommand dbCommand, string name, object value){
					IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
					dbDataParameter.ParameterName = name;
					dbDataParameter.Value = value;
					dbCommand.Parameters.Add(dbDataParameter);
				}
		}
	}
}

