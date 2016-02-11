package org.institutoserpis.ad;

import java.math.*;
import javax.persistence.*;

public class PedidoLinea {
	private Long id;
	private Pedido pedido;
	private Articulo articulo;
	private BigDecimal precio;
	private BigDecimal unidades;
	private BigDecimal importe;
	
	public Long getId(){
		return id;
	}
	public void setId(Long id){
		this.id = id;
	}
	public Pedido getPedido(){
		return pedido;
	}
	public void setPedido(Pedido pedido){
		pedido.getPedidoLineas().add(this);
		this.pedido = pedido;
	}
	@ManyToOne
	@JoinColumn(name="articulo")
	public Articulo getArticulo() {
		return articulo;
	}
	public void setArticulo(Articulo articulo) {
		this.articulo = articulo;
	}
	public BigDecimal getPrecio() {
		return precio;
	}
	public void setPrecio(BigDecimal precio) {
		this.precio = precio;
	}
	public BigDecimal getUnidades() {
		return unidades;
	}
	public void setUnidades(BigDecimal unidades) {
		this.unidades = unidades;
	}
	public BigDecimal getImporte() {
		return importe;
	}
	public void setImporte(BigDecimal importe) {
		this.importe = importe;
	}
	
	@Override
	public String toString(){
		return String.format("%s [pedido-%s] [articulo-%s] %s %s %s",
				id,
				pedido == null ? null : pedido.getId(),
				articulo == null ? null : articulo.getId(),
				precio,
				unidades,
				importe
				);
	}
}
