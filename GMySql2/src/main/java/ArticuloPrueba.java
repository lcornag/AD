import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;

public class ArticuloPrueba {

	public static void main(String[] args) throws SQLException {
		Connection connection = DriverManager.getConnection(
			"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		
		Statement stat = connection.createStatement();
		ResultSet rs = stat.executeQuery("select * from articulo");
		ResultSetMetaData rsmd = rs.getMetaData();
				
		for(int i = 1; i<=rsmd.getColumnCount();i++){
			System.out.print(rsmd.getColumnName(i)+"\t"+"\t");
			
		}
		System.out.println();
		while(rs.next()){
			for(int i = 1; i<=rsmd.getColumnCount() ; i++){
				System.out.print(rs.getString(i)+"            "+"\t");
			}
			System.out.println();
		}
		connection.close();
		System.out.println("fin");
	}
}