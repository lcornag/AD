package org.institutoserpis.ad;

import java.util.List;
import java.util.logging.*;
import javax.persistence.*;



public class PruebaPedido {
	private static EntityManagerFactory entityManagerFactory;
	
	public static void main(String[] args){
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("Inicio");
		entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
		query();
		
		entityManagerFactory.close();
		System.out.println("Fin");
	}
	
	private static void query(){
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Pedido> Pedidos = entityManager.createQuery("from Pedido", Pedido.class).getResultList();
		for (Pedido Pedido: Pedidos){
			System.out.println(Pedido);
		}
		entityManager.getTransaction().commit();
		entityManager.close();
	}
}