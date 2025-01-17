using Microsoft.EntityFrameworkCore;
using SD.Domain.Common;
using SD.Domain.Entities.Adm_Presupuestos.Conduces.Items;
using SD.Domain.Entities.Adm_Presupuestos.Conduces.Recepcion;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyecto.Proyecto_Organizacion;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Categoria;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Entities_Per_Year;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Organizacion;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Proyecto;
using SD.Domain.Entities.Adm_Presupuestos.Presupuestos.Proyectos.Proyecto_Budget;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Categoria;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Equipo;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Item;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Requisicion;
using SD.Domain.Entities.Adm_Presupuestos.Requisiciones.Suplidor;
using SD.Domain.Entities.Adm_Presupuestos.Transferencias.Transferencia;
using SD.Domain.Entities.Administracion.Accesos.Roles;
using SD.Domain.Entities.Administracion.User;
using SD.Domain.Entities.Provisioning.Control_OC.Actividad;
using SD.Domain.Entities.Provisioning.Control_OC.Apn;
using SD.Domain.Entities.Provisioning.Control_OC.Bandwith;
using SD.Domain.Entities.Provisioning.Control_OC.Mascara;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Entities.Provisioning.Control_OC.Producto;
using SD.Domain.Entities.Provisioning.Control_OC.Vpn;
using SD.Domain.Entities.Provisioning.Templates.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Persistence.Context
{
    public partial class SdB2bDbContext : DbContext
	{
		public SdB2bDbContext(DbContextOptions<SdB2bDbContext> options) : base(options) { }

		//Provisioning

		#region Provisioning

		public virtual DbSet<TbActividad> TbActividads { get; set; }

		public virtual DbSet<TbBandwith> TbBandwiths { get; set; }

		public virtual DbSet<TbMask> TbMasks { get; set; }

		public virtual DbSet<TbOc> TbOcs { get; set; } = null!;

		public virtual DbSet<TbApn> TbApn { get; set; }

		public virtual DbSet<TbProducto> TbProductos { get; set; }

		//public virtual DbSet<TbTiposervicio> TbTiposervicios { get; set; }

		public virtual DbSet<TbVpn> TbVpns { get; set; }

		public virtual DbSet<TbCategorium> TbCategoria { get; set; }

		public virtual DbSet<TbProfiles> TbProfiles { get; set; }

		#endregion

		//Administración de Presupuestos

		#region Administración de Presupuestos

		//Requisicion

		#region Requisicion

		public virtual DbSet<TbRequisicion> TbRequisicions { get; set; }

		public virtual DbSet<TbSuplidor> TbSuplidors { get; set; }

		public virtual DbSet<TbEquipos> TbEquipos { get; set; }

		public virtual DbSet<TbItem> TbItems { get; set; }

		#endregion

		//Presupuestos

		#region Presupuestos

		public virtual DbSet<TbProyecto> TbProyectos { get; set; }

		public virtual DbSet<TbOrganizacion> TbOrganizacions { get; set; }

		public virtual DbSet<TbCategoriaProyecto> TbCategoriaProyectos { get; set; }

		public virtual DbSet<TbOrgProyecto> TbOrgProyectos { get; set; }

		public virtual DbSet<TbProyectoPresupuestos> TbProyectoPresupuestoss { get; set; }

		#endregion

		//Recepcion Conduce

		#region Recepcion Conduce

		public virtual DbSet<TbRecepcionC> TbRecepcionCs { get; set; }

		public virtual DbSet<TbItemRecepcionC> TbItemRecepcionC { get; set; }

		public virtual DbSet<TbItemRecepcionados> TbItemRecepcionados { get; set; }

		#endregion

		//Detalles Anuales de Presupuesto

		#region Detalles Anuales de Presupuesto

		public virtual DbSet<TbCantidadAnual> TbCantidadAnuals { get; set; }

		public virtual DbSet<TbPrecioUnitarioAnual> TbPrecioUnitarioAnuals { get; set; }

		public virtual DbSet<TbTotalMonedaCambioAnual> TbTotalMonedaCambioAnuals { get; set; }

		public virtual DbSet<TbTotalDOPAnual> TbTotalDOPAnuals { get; set; }

		public virtual DbSet<TbTotalTrimestral> TbTotalTrimestrals { get; set; }

		#endregion

		//Transferencia

		#region Transferencia

		public virtual DbSet<TbTransferencium> TbTransferencia { get; set; }

		public DbSet<TbTransferencium> TbTransferencium { get; set; } = default!;

		#endregion

		#endregion

		//Anexos

		#region Anexos

		public virtual DbSet<TbAnexo> TbAnexo { get; set; }

		#endregion

		//Administración

		#region Administración

		public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

		public virtual DbSet<TbRolPermiso> TbRolPermiso { get; set; }

		#endregion

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.UseCollation("utf8mb4_unicode_ci")
				.HasCharSet("utf8mb4");

			//Provisioning

			#region Provisioning

			modelBuilder.Entity<TbProfiles>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_prov_profiles");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Equipo).HasMaxLength(45);
				entity.Property(e => e.Perfil).HasMaxLength(45);
			});

			modelBuilder.Entity<TbActividad>(entity =>
			{
				entity.HasKey(e => e.IdActividad).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_actividades")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.IdActividad)
					.ValueGeneratedNever()
					.HasColumnType("int(11)")
					.HasColumnName("id_actividad");
				entity.Property(e => e.Actividad)
					.HasMaxLength(25)
					.HasColumnName("actividad");
			});

			modelBuilder.Entity<TbBandwith>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_bandwith")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.Id)
					.HasColumnType("int(11)")
					.HasColumnName("id");
				entity.Property(e => e.DescripcionBw)
					.HasMaxLength(20)
					.HasColumnName("descripcionBW");
				entity.Property(e => e.DescripcionBws)
					.HasMaxLength(20)
					.HasColumnName("descripcionBWS");
			});

			modelBuilder.Entity<TbMask>(entity =>
			{
				entity.HasKey(e => e.Masc).HasName("PRIMARY");

				entity
					.ToTable("tb_mask")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.HasIndex(e => e.Masc, "masc_UNIQUE").IsUnique();

				entity.Property(e => e.Masc)
					.HasMaxLength(2)
					.HasColumnName("masc");
				entity.Property(e => e.MascIp)
					.HasMaxLength(15)
					.HasColumnName("masc_ip");
			});

			modelBuilder.Entity<TbOc>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_ocs")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.Id)
					.HasColumnType("int(11)")
					.HasColumnName("id");
				entity.Property(e => e.Actividad)
					.HasMaxLength(18)
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Bandwidth)
					.HasMaxLength(17)
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Bw)
					.HasMaxLength(14)
					.HasColumnName("BW")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Circuito).HasMaxLength(36);
				entity.Property(e => e.Cliente)
					.HasMaxLength(120)
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.DescripOc)
					.HasMaxLength(180)
					.HasColumnName("descrip_OC")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Descripcion)
					.HasMaxLength(250)
					.HasColumnName("DESCRIPCION")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.EqDemarcacionAcceso)
					.HasMaxLength(20)
					.HasColumnName("eq_demarcacion_acceso");
				entity.Property(e => e.EquipoAg)
					.HasMaxLength(34)
					.HasColumnName("Equipo_Ag");
				entity.Property(e => e.Estatus)
					.HasMaxLength(12)
					.HasColumnName("estatus");
				entity.Property(e => e.FechaRespuestaIng)
					.HasMaxLength(26)
					.HasColumnName("fecha_Respuesta_Ing");
				entity.Property(e => e.FechaSolicitudIng)
					.HasMaxLength(26)
					.HasColumnName("fecha_Solicitud_ing")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Id2)
					.HasColumnType("int(11)")
					.HasColumnName("id2");
				entity.Property(e => e.Id3)
					.HasColumnType("int(11)")
					.HasColumnName("id3");
				entity.Property(e => e.IdGrado)
					.HasMaxLength(15)
					.HasColumnName("id_grado");
				entity.Property(e => e.Idip)
					.HasColumnType("int(11)")
					.HasColumnName("idip");
				entity.Property(e => e.IpClienteLAg)
					.HasMaxLength(20)
					.HasColumnName("ip_clienteL_Ag");
				entity.Property(e => e.IpClienteLMask)
					.HasMaxLength(15)
					.HasColumnName("ip_clienteL_mask");
				entity.Property(e => e.IpClienteWAg)
					.HasMaxLength(100)
					.HasColumnName("ip_clienteW_Ag");
				entity.Property(e => e.IpClienteWMask)
					.HasMaxLength(15)
					.HasColumnName("ip_clienteW_mask");
				entity.Property(e => e.IpGestionAg)
					.HasMaxLength(15)
					.HasColumnName("ipGestion_ag");
				entity.Property(e => e.IpIspAg)
					.HasMaxLength(36)
					.HasColumnName("ip_isp_Ag")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.IpIspMask)
					.HasMaxLength(15)
					.HasColumnName("ip_isp_mask");
				entity.Property(e => e.IpMngRouterMask)
					.HasMaxLength(15)
					.HasColumnName("ip_mngRouter_mask");
				entity.Property(e => e.IpPrefix)
					.HasMaxLength(11)
					.HasColumnName("ip_prefix");
				entity.Property(e => e.ManagmentRouter)
					.HasMaxLength(22)
					.HasColumnName("managment_Router");
				entity.Property(e => e.ManagmentSwitch)
					.HasMaxLength(22)
					.HasColumnName("managment_Switch");
				entity.Property(e => e.Mngtag)
					.HasMaxLength(8)
					.HasColumnName("mngtag");
				entity.Property(e => e.NodoAcceso)
					.HasMaxLength(50)
					.HasColumnName("Nodo_Acceso");
				entity.Property(e => e.Oc)
					.HasMaxLength(10)
					.HasColumnName("OC")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Os)
					.HasMaxLength(11)
					.HasColumnName("OS")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Prefix)
					.HasMaxLength(20)
					.HasColumnName("prefix");
				entity.Property(e => e.Producto)
					.HasMaxLength(50)
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.PuertoAcceso)
					.HasMaxLength(15)
					.HasColumnName("Puerto_Acceso")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.PuertoAg)
					.HasMaxLength(36)
					.HasColumnName("puerto_Ag");
				entity.Property(e => e.PuertoTransIn)
					.HasMaxLength(30)
					.HasColumnName("Puerto_Trans_IN");
				entity.Property(e => e.PuertoTransOut)
					.HasMaxLength(30)
					.HasColumnName("Puerto_Trans_OUT");
				entity.Property(e => e.RealmAg)
					.HasMaxLength(19)
					.HasColumnName("realm_Ag")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Semana).HasColumnType("int(11)");
				entity.Property(e => e.Template)
					.HasColumnType("longtext")
					.HasColumnName("template");
				entity.Property(e => e.TipoPuertoAg)
					.HasMaxLength(19)
					.HasColumnName("tipoPuerto_Ag")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.TipoServicio)
					.HasMaxLength(15)
					.HasColumnName("tipoServicio");
				entity.Property(e => e.Ttk)
					.HasMaxLength(26)
					.HasColumnName("ttk")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.TxInTrans)
					.HasMaxLength(30)
					.HasColumnName("TX_IN_Trans")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.TxOutTrans)
					.HasMaxLength(24)
					.HasColumnName("TX_OUT_Trans");
				entity.Property(e => e.UplinkAcceso)
					.HasMaxLength(25)
					.HasColumnName("UPLINK_Acceso")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.UsuarioCreo)
					.HasMaxLength(28)
					.HasColumnName("usuario_creo");
				entity.Property(e => e.VlanAcceso)
					.HasMaxLength(25)
					.HasColumnName("VLAN_Acceso");
				entity.Property(e => e.VlanMgmtAg)
					.HasMaxLength(10)
					.HasColumnName("vlan_Mgmt_ag");
				entity.Property(e => e.VpnAg)
					.HasMaxLength(14)
					.HasColumnName("VPN_ag")
					.UseCollation("utf8_general_ci")
					.HasCharSet("utf8");
				entity.Property(e => e.Bws)
					.HasMaxLength(14)
					.HasColumnName("BWS");
				entity.Property(e => e.GPON_P2P)
					.HasMaxLength(15);
				entity.Property(e => e.LoopBack)
					.HasMaxLength(20)
					.HasColumnName("loopback");
				entity.Property(e => e.CreateLoopBack)
					.HasMaxLength(3)
					.HasColumnName("Create_LoopBack");
				entity.Property(e => e.CreateVPN)
					.HasMaxLength(3)
					.HasColumnName("Create_VPN");
				entity.Property(e => e.RouterRD)
					.HasMaxLength(3)
					.HasColumnName("Router_RD");
				entity.Property(e => e.PON)
					.HasMaxLength(30);
				entity.Property(e => e.PosicionOnt)
					.HasMaxLength(15);
				entity.Property(e => e.ClienteRutaEstatica)
					.HasMaxLength(21)
					.HasColumnName("cliente_ruta_estatica");
				entity.Property(e => e.NombreFactura)
					.HasMaxLength(100)
					.HasColumnName("nombre_factura");
				entity.Property(e => e.NumeroCuenta)
					.HasMaxLength(30)
					.HasColumnName("numero_cuenta");
				entity.Property(e => e.Plan)
					.HasMaxLength(100)
					.HasColumnName("plan");
				entity.Property(e => e.MangaGPON)
					.HasMaxLength(45)
					.HasColumnName("manga_gpon");
				entity.Property(e => e.PuertoMG)
					.HasMaxLength(45)
					.HasColumnName("puerto_mg");
				entity.Property(e => e.GestionIpP2P)
					.HasMaxLength(45)
					.HasColumnName("gestion_ip_p2p");
				entity.Property(e => e.GestionVlanP2P)
					.HasMaxLength(45)
					.HasColumnName("gestion_vlan_p2p");
				entity.Property(e => e.GestionIpSwitch)
					.HasMaxLength(45)
					.HasColumnName("gestion_ip_switch");
				entity.Property(e => e.GestionIpRouter)
					.HasMaxLength(45)
					.HasColumnName("gestion_ip_router");
				entity.Property(e => e.GestionPrimeraMultiVlan)
					.HasMaxLength(45)
					.HasColumnName("gestion_primera_multivlan");
				entity.Property(e => e.GestionSegundaMultiVlan)
					.HasMaxLength(45)
					.HasColumnName("gestion_segunda_multivlan");
				entity.Property(e => e.CoordenadasClienteY)
					.HasMaxLength(45)
					.HasColumnName("coordenadas_cliente_y");
				entity.Property(e => e.CoordenadasClienteX)
					.HasMaxLength(45)
					.HasColumnName("coordenadas_cliente_x");
				entity.Property(e => e.CoordenadasMangaY)
					.HasMaxLength(45)
					.HasColumnName("coordenadas_manga_y");
				entity.Property(e => e.CoordenadasMangaX)
					.HasMaxLength(45)
					.HasColumnName("coordenadas_manga_x");
				entity.Property(e => e.Site)
					.HasMaxLength(45)
					.HasColumnName("site");
				entity.Property(e => e.Serial_ONT)
					  .HasMaxLength(21)
					  .HasColumnName("serial_ont");
				entity.Property(e => e.ApnId)
					  .HasMaxLength(10)
					  .HasColumnName("apn_id");
			});

			modelBuilder.Entity<TbApn>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_apn")
					.HasCharSet("utf8mb4")
					.UseCollation("utf8mb4_general_ci");

				entity.Property(e => e.Id)
					.HasMaxLength(10)
					.HasColumnName("id");
				entity.Property(e => e.SegundoEquipo)
					.HasMaxLength(34)
					.HasColumnName("segundo_equipo");
				entity.Property(e => e.SegundaVlan)
					.HasMaxLength(25)
					.HasColumnName("segunda_vlan");
				entity.Property(e => e.RouterTarget)
					.HasMaxLength(10)
					.HasColumnName("route_target");
				entity.Property(e => e.IpRoutePolicy)
					.HasMaxLength(20)
					.HasColumnName("ip_route_policy");
				entity.Property(e => e.SecondIpRoutePolicy)
					.HasMaxLength(20)
					.HasColumnName("second_ip_route_policy");
				entity.Property(e => e.MaskRoutePolicy)
					.HasMaxLength(15)
					.HasColumnName("mask_route_policy");
				entity.Property(e => e.NombreApn)
					.HasMaxLength(50)
					.HasColumnName("nombre_apn");

			});

			modelBuilder.Entity<TbProducto>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_productos")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnType("int(11)")
					.HasColumnName("id");
				entity.Property(e => e.Descripcion)
					.HasMaxLength(45)
					.HasColumnName("descripcion");
				entity.Property(e => e.Orden)
					.HasColumnType("int(11)")
					.HasColumnName("orden");
				entity.Property(e => e.TipoProducto)
					.HasMaxLength(45)
					.HasColumnName("tipoProducto");
			});

			modelBuilder.Entity<TbVpn>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_prov_vpns")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnType("int(4)")
					.HasColumnName("id");
				entity.Property(e => e.Descrip)
					.HasMaxLength(100)
					.HasColumnName("descrip");
				entity.Property(e => e.Name)
					.HasMaxLength(64)
					.HasColumnName("name");
				entity.Property(e => e.Rd)
					.HasMaxLength(25)
					.HasColumnName("rd");
				entity.Property(e => e.VrfGroup)
					.HasColumnType("int(4)")
					.HasColumnName("vrf_group");
			});

			#endregion

			//Administración de Presupuestos

			#region Administración de Presupuestos

			//Requisicion

			#region Requisicion

			modelBuilder.Entity<TbItem>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_req_items");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Cantidad).HasColumnType("decimal(20,10)");
				entity.Property(e => e.Categoria).HasMaxLength(100);
				entity.Property(e => e.Codigo).HasMaxLength(100);
				entity.Property(e => e.CostoPromedio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Promedio");
				entity.Property(e => e.CostoTotalMonedaC)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_MonedaC");
				entity.Property(e => e.CostoTotalRd)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_Rd");
				entity.Property(e => e.Descripcion).HasMaxLength(250);
				entity.Property(e => e.NombreEquipo)
					.HasMaxLength(100)
					.HasColumnName("Nombre_Equipo");
				entity.Property(e => e.OrdenCompra)
					.HasColumnType("int(11)")
					.HasColumnName("Orden_Compra");
				entity.Property(e => e.Requisicion).HasColumnType("int(11)");
				entity.Property(e => e.TasaCambio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Tasa_Cambio");
				entity.Property(e => e.TipoMoneda)
					.HasMaxLength(20)
					.HasColumnName("Tipo_Moneda");
				entity.Property(e => e.TipoCompra)
					.HasMaxLength(20)
					.HasColumnName("Tipo_Compra");
				entity.Property(e => e.Descuento)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Descuento");
				entity.Property(e => e.MontoConDescuento)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Con_Descuento");
				entity.Property(e => e.MontoConDescuentoRd)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Con_DescuentoRd");
				entity.Property(e => e.TotalDescontado)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado");
				entity.Property(e => e.TotalDescontadoDOP)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado_DOP");
			});

			modelBuilder.Entity<TbRequisicion>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_req_requisiciones");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Abrv)
					.HasMaxLength(50)
					.HasColumnName("ABRV");
				entity.Property(e => e.Cliente).HasMaxLength(120);
				entity.Property(e => e.Concepto).HasMaxLength(50);
				entity.Property(e => e.CostoTotalMonedaC)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_MonedaC");
				entity.Property(e => e.CostoTotalRd)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_Rd");
				entity.Property(e => e.DescricionProyecto).HasMaxLength(200);
				entity.Property(e => e.Descripcion).HasMaxLength(250);
				entity.Property(e => e.Estatus).HasMaxLength(50);
				entity.Property(e => e.FechaOrdenC)
					.HasMaxLength(20)
					.HasColumnName("Fecha_Orden_C");
				entity.Property(e => e.FechaReq)
					.HasMaxLength(20)
					.HasColumnName("Fecha_Req");
				entity.Property(e => e.Ing).HasMaxLength(100);
				entity.Property(e => e.LineBudg).HasMaxLength(65);
				entity.Property(e => e.LineBudg1)
					.HasMaxLength(65)
					.HasColumnName("Line_Budg");
				entity.Property(e => e.NoCotizacion).HasMaxLength(50)
					.HasColumnName("No_Cotizacion");
				entity.Property(e => e.OrdenCompra)
					.HasColumnType("int(11)")
					.HasColumnName("Orden_Compra");
				entity.Property(e => e.Organizacion).HasMaxLength(50);
				entity.Property(e => e.Proyecto).HasMaxLength(200);
				entity.Property(e => e.Recurrente).HasMaxLength(50);
				entity.Property(e => e.Requisicion).HasColumnType("int(11)");
				entity.Property(e => e.Suplidor).HasMaxLength(50);
				entity.Property(e => e.TasaCambio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Tasa_Cambio");
				entity.Property(e => e.TipoCompra)
					.HasMaxLength(150)
					.HasColumnName("Tipo_Compra");
				entity.Property(e => e.TipoMoneda)
					.HasMaxLength(20)
					.HasColumnName("Tipo_Moneda");
				entity.Property(e => e.TipoServicio)
					.HasMaxLength(50)
					.HasColumnName("Tipo_Servicio");
				entity.Property(e => e.TotalDescontado)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado");
				entity.Property(e => e.TotalDescontadoDOP)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado_DOP");
			});

			modelBuilder.Entity<TbSuplidor>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_suplidores");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Contacto).HasMaxLength(50);
				entity.Property(e => e.CorreoContacto)
					.HasMaxLength(20)
					.HasColumnName("Correo_contacto");
				entity.Property(e => e.Nombre).HasMaxLength(50);
				entity.Property(e => e.Telefono).HasMaxLength(13);
			});

			modelBuilder.Entity<TbEquipos>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_equipos");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Nombre).HasMaxLength(250)
					.HasColumnName("Nombre_Equipo");
				entity.Property(e => e.CodigoInventario)
					.HasMaxLength(100)
					.HasColumnName("Codigo_Inventario");
			});

			modelBuilder.Entity<TbCategorium>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_req_categorias");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Detalle).HasMaxLength(250);
				entity.Property(e => e.Nombre).HasMaxLength(100);
			});

			#endregion

			//Presupuestos

			#region Presupuestos

			modelBuilder.Entity<TbProyecto>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_proyectos");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Abrv)
					.HasMaxLength(50)
					.HasColumnName("ABRV");
				entity.Property(e => e.AccountNumber)
					.HasMaxLength(150)
					.HasColumnName("Account_Number");
				entity.Property(e => e.BudgetAprobado)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Aprobado");
				entity.Property(e => e.BudgetDescription)
					.HasMaxLength(200)
					.HasColumnName("Budget_Description");
				entity.Property(e => e.BudgetRestante)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Restante");
				entity.Property(e => e.BudgetSometido)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Sometido");
				entity.Property(e => e.BudgetTransferido)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Transferido");
				entity.Property(e => e.Code).HasMaxLength(45);
				entity.Property(e => e.Concepto).HasMaxLength(100);
				entity.Property(e => e.DescripOrganizacion)
					.HasMaxLength(150)
					.HasColumnName("Descrip_Organizacion");
				entity.Property(e => e.Detalle).HasMaxLength(200);
				entity.Property(e => e.Director).HasMaxLength(100);
				entity.Property(e => e.FaEssbase)
					.HasMaxLength(150)
					.HasColumnName("FA_(essbase)");
				entity.Property(e => e.Father).HasMaxLength(100);
				entity.Property(e => e.LineBudg).HasMaxLength(65);
				entity.Property(e => e.NombreProyecto)
					.HasMaxLength(100)
					.HasColumnName("Nombre_Proyecto");
				entity.Property(e => e.Organizacion).HasMaxLength(50);
				entity.Property(e => e.OriginalCurrency)
					.HasMaxLength(20)
					.HasColumnName("Original_Currency");
				entity.Property(e => e.Stream).HasMaxLength(65);
				entity.Property(e => e.Type).HasMaxLength(45);
				entity.Property(e => e.Year).HasMaxLength(45);
				entity.Property(e => e.Suplidor).HasMaxLength(50);
				entity.Property(e => e.TasaCambio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Tasa_Cambio");
				entity.Property(e => e.Concatenate).HasMaxLength(250);
				entity.Property(e => e.BudgetRecibido)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Recibido");
				entity.Property(e => e.BudgetConsumido)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Consumido");
				entity.Property(e => e.BudgetComprometido)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Budget_Comprometido");
				entity.Property(e => e.Company).HasMaxLength(20);
				entity.Property(e => e.DetalleRecibido)
					.HasMaxLength(200)
					.HasColumnName("Detalle_Recibido");
				entity.Property(e => e.CantidadRecibida)
					.HasColumnType("int(3)")
					.HasColumnName("Cantidad_Recibida");
				entity.Property(e => e.CantidadTransferida)
					.HasColumnType("int(3)")
					.HasColumnName("Cantidad_Transferida");
				entity.Property(e => e.Total_POs).HasColumnType("int(3)");
			});

			modelBuilder.Entity<TbOrganizacion>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_organizaciones");

				entity.Property(entity => entity.Id).HasColumnType("Id");
				entity.Property(entity => entity.CC).HasMaxLength(30);
				entity.Property(entity => entity.Descripcion).HasMaxLength(250);

			});

			modelBuilder.Entity<TbCategoriaProyecto>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_project_categories");

				entity.Property(entity => entity.Id).HasColumnType("Id");
				entity.Property(entity => entity.CodProyecto).HasMaxLength(20);
				entity.Property(entity => entity.Categoria).HasMaxLength(10);

			});

			modelBuilder.Entity<TbOrgProyecto>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_proyecto_orgs");

				entity.Property(entity => entity.Id).HasColumnType("Id");
				entity.Property(entity => entity.CodProyecto)
					.HasMaxLength(20)
					.HasColumnName("Project_Code");
				entity.Property(entity => entity.DescProyecto)
					.HasMaxLength(150)
					.HasColumnName("Project_Description");
				entity.Property(entity => entity.CodigoOrg)
					.HasMaxLength(20)
					.HasColumnName("Org_Code");
				entity.Property(entity => entity.CodigoAcc)
					.HasMaxLength(30)
					.HasColumnName("Acc_Code");
				entity.Property(entity => entity.CodigoCat)
					.HasMaxLength(30)
					.HasColumnName("Cat_Code");
				entity.Property(entity => entity.DescripcionAcc)
					.HasMaxLength(20)
					.HasColumnName("Acc_Description");

			});

			modelBuilder.Entity<TbProyectoPresupuestos>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_proyectos_presupuestos");

				entity.Property(entity => entity.Id).HasColumnType("Id");
				entity.Property(entity => entity.CodProyecto)
					.HasMaxLength(20)
					.HasColumnName("Project_Code");
				entity.Property(entity => entity.Item).HasMaxLength(150);
				entity.Property(entity => entity.DescBudget)
				.HasMaxLength(250)
				.HasColumnName("Budget_Description");
				entity.Property(entity => entity.Director).HasMaxLength(100);
				entity.Property(entity => entity.STREAM).HasMaxLength(150);
				entity.Property(entity => entity.Proyecto).HasMaxLength(150);
				entity.Property(entity => entity.StreamC).HasMaxLength(150);

			});

			#endregion

			//Detalles Anuales de Presupuesto

			#region Detalles Anuales de Presupuestos

			modelBuilder.Entity<TbCantidadAnual>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_quantities");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Enero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Ene");
				entity.Property(e => e.Febrero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Feb");
				entity.Property(e => e.Marzo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Mar");
				entity.Property(e => e.Abril)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Abr");
				entity.Property(e => e.Mayo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("May");
				entity.Property(e => e.Junio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jun");
				entity.Property(e => e.Julio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jul");
				entity.Property(e => e.Agosto)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Agt");
				entity.Property(e => e.Septiembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Sept");
				entity.Property(e => e.Octubre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Oct");
				entity.Property(e => e.Noviembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Nov");
				entity.Property(e => e.Diciembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Dic");
				entity.Property(e => e.Anho).HasMaxLength(20);
				entity.Property(e => e.TotalCantidad)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Cantidad");
				entity.Property(e => e.Line_Budget).HasMaxLength(65);
				entity.Property(e => e.Budget_SA).HasMaxLength(20);
				entity.Property(e => e.Code).HasMaxLength(45);
			});

			modelBuilder.Entity<TbPrecioUnitarioAnual>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_unit_prices");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Enero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Ene");
				entity.Property(e => e.Febrero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Feb");
				entity.Property(e => e.Marzo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Mar");
				entity.Property(e => e.Abril)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Abr");
				entity.Property(e => e.Mayo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("May");
				entity.Property(e => e.Junio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jun");
				entity.Property(e => e.Julio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jul");
				entity.Property(e => e.Agosto)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Agt");
				entity.Property(e => e.Septiembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Sept");
				entity.Property(e => e.Octubre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Oct");
				entity.Property(e => e.Noviembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Nov");
				entity.Property(e => e.Diciembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Dic");
				entity.Property(e => e.Anho).HasMaxLength(20);
				entity.Property(e => e.TotalPrecioUnit)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_PU");
				entity.Property(e => e.Line_Budget).HasMaxLength(65);
				entity.Property(e => e.Budget_SA).HasMaxLength(20);
				entity.Property(e => e.Code).HasMaxLength(45);
			});

			modelBuilder.Entity<TbTotalMonedaCambioAnual>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_amount_currencies");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Enero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Ene");
				entity.Property(e => e.Febrero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Feb");
				entity.Property(e => e.Marzo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Mar");
				entity.Property(e => e.Abril)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Abr");
				entity.Property(e => e.Mayo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("May");
				entity.Property(e => e.Junio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jun");
				entity.Property(e => e.Julio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jul");
				entity.Property(e => e.Agosto)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Agt");
				entity.Property(e => e.Septiembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Sept");
				entity.Property(e => e.Octubre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Oct");
				entity.Property(e => e.Noviembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Nov");
				entity.Property(e => e.Diciembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Dic");
				entity.Property(e => e.Anho).HasMaxLength(20);
				entity.Property(e => e.TotalMonedaCambio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_MC");
				entity.Property(e => e.Line_Budget).HasMaxLength(65);
				entity.Property(e => e.Budget_SA).HasMaxLength(20);
				entity.Property(e => e.Code).HasMaxLength(45);
			});

			modelBuilder.Entity<TbTotalDOPAnual>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_amount_dops");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Enero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Ene");
				entity.Property(e => e.Febrero)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Feb");
				entity.Property(e => e.Marzo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Mar");
				entity.Property(e => e.Abril)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Abr");
				entity.Property(e => e.Mayo)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("May");
				entity.Property(e => e.Junio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jun");
				entity.Property(e => e.Julio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Jul");
				entity.Property(e => e.Agosto)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Agt");
				entity.Property(e => e.Septiembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Sept");
				entity.Property(e => e.Octubre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Oct");
				entity.Property(e => e.Noviembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Nov");
				entity.Property(e => e.Diciembre)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Dic");
				entity.Property(e => e.Anho).HasMaxLength(20);
				entity.Property(e => e.TotalDOP)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Dop");
				entity.Property(e => e.Line_Budget).HasMaxLength(65);
				entity.Property(e => e.Budget_SA).HasMaxLength(20);
				entity.Property(e => e.Code).HasMaxLength(45);
			});

			modelBuilder.Entity<TbTotalTrimestral>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_presp_trimestrales");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Q1)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Q1");
				entity.Property(e => e.Q2)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Q2");
				entity.Property(e => e.Q3)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Q3");
				entity.Property(e => e.Q4)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Q4");
				entity.Property(e => e.Anho).HasMaxLength(20);
				entity.Property(e => e.Line_Budget).HasMaxLength(65);
				entity.Property(e => e.Budget_SA).HasMaxLength(20);
				entity.Property(e => e.Code).HasMaxLength(45);
				entity.Property(e => e.Tipo_Total).HasMaxLength(35);
			});

			#endregion

			//Recepcion Conduce

			#region Recepcion Conduce

			modelBuilder.Entity<TbRecepcionC>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_recep_recepcionesc");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.CantidadDisponible)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Cantidad_Disponible");
				entity.Property(e => e.CantidadOriginal)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Cantidad_Original");
				entity.Property(e => e.CantidadRecepcion)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Cantidad_Recepcion");
				entity.Property(e => e.Conduce).HasMaxLength(50);
				entity.Property(e => e.Descripcion).HasMaxLength(250);
				entity.Property(e => e.FechaRecepcion)
					.HasMaxLength(20)
					.HasColumnName("Fecha_Recepcion");
				entity.Property(e => e.Ing).HasMaxLength(100);
				entity.Property(e => e.LineBudg)
					.HasMaxLength(65)
					.HasColumnName("Line_Budg");
				entity.Property(e => e.MontoDisponible)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Disponible");
				entity.Property(e => e.MontoOriginal)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Original");
				entity.Property(e => e.MontoRecepcionado)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Recepcionado");
				entity.Property(e => e.NoRecepcion)
					.HasMaxLength(20)
					.HasColumnName("No_Recepcion");
				entity.Property(e => e.NoRecepcionOracle)
					.HasMaxLength(20)
					.HasColumnName("No_Recepcion_Oracle");
				entity.Property(e => e.OrdenCompra)
					.HasColumnType("int(11)")
					.HasColumnName("Orden_Compra");
				entity.Property(e => e.Suplidor).HasMaxLength(50);
			});

			modelBuilder.Entity<TbItemRecepcionC>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_recep_items_recepcionesc");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Cantidad).HasColumnType("decimal(20,10)");
				entity.Property(e => e.Categoria).HasMaxLength(100);
				entity.Property(e => e.Codigo).HasMaxLength(100);
				entity.Property(e => e.CostoPromedio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Promedio");
				entity.Property(e => e.CostoTotalMonedaC)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_MonedaC");
				entity.Property(e => e.CostoTotalRd)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Costo_Total_Rd");
				entity.Property(e => e.Descripcion).HasMaxLength(250);
				entity.Property(e => e.NombreEquipo)
					.HasMaxLength(100)
					.HasColumnName("Nombre_Equipo");
				entity.Property(e => e.OrdenCompra)
					.HasColumnType("int(11)")
					.HasColumnName("Orden_Compra");
				entity.Property(e => e.Requisicion).HasColumnType("int(11)");
				entity.Property(e => e.TasaCambio)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Tasa_Cambio");
				entity.Property(e => e.TipoMoneda)
					.HasMaxLength(20)
					.HasColumnName("Tipo_Moneda");
				entity.Property(e => e.TipoCompra)
					.HasMaxLength(20)
					.HasColumnName("Tipo_Compra");
				entity.Property(e => e.Descuento)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Descuento");
				entity.Property(e => e.MontoConDescuento)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Con_Descuento");
				entity.Property(e => e.MontoConDescuentoRd)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Monto_Con_DescuentoRd");
				entity.Property(e => e.TotalDescontado)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado");
				entity.Property(e => e.TotalDescontadoDOP)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("Total_Descontado_DOP");
			});

			modelBuilder.Entity<TbItemRecepcionados>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_recep_items_recepcionados");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Cantidad).HasColumnType("decimal(20,10)")
					.HasColumnName("Cantidad_Recepcionada");
				entity.Property(e => e.Descripcion).HasMaxLength(250)
					.HasColumnName("Desc_Item");
				entity.Property(e => e.OrdenCompra)
					.HasColumnType("int(11)")
					.HasColumnName("Orden_Compra");
				entity.Property(e => e.NoRecepcion).HasMaxLength(20)
					.HasColumnName("No_Recepcion");
			});

			#endregion

			//Transferencias

			#region Transferencias

			modelBuilder.Entity<TbTransferencium>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_admpr_transf_transferencias");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.FechaTransfer).HasMaxLength(50)
					.HasColumnName("Fecha_Transferencia");
				entity.Property(e => e.ProyectoOrigen).HasMaxLength(250)
					.HasColumnName("Proyecto_Origen");
				entity.Property(e => e.MD_ProyectoOrigen)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("MontoDisponible_ProyectoOrigen");
				entity.Property(e => e.ProyectoDestino).HasMaxLength(250)
					.HasColumnName("Proyecto_Destino");
				entity.Property(e => e.MR_ProyectoDestino)
					.HasColumnType("decimal(20,10)")
					.HasColumnName("MontoRequerido_ProyectoDestino");
				entity.Property(e => e.Detalle).HasMaxLength(250);
				entity.Property(e => e.Codigo).HasMaxLength(20);
				entity.Property(e => e.Ing).HasMaxLength(100);
			});

			#endregion

			#endregion

			//Anexos

			#region Anexos

			modelBuilder.Entity<TbAnexo>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity.ToTable("tb_anexos");

				entity.Property(e => e.Id).HasColumnType("int(11)");
				entity.Property(e => e.Identificador).HasMaxLength(50)
				.HasColumnName("No_Requisicion");
				entity.Property(e => e.NombreArchivo)
				.HasMaxLength(250)
				.HasColumnName("Nombre_Archivo");
			});

			#endregion

			//Administración

			#region Administración

			modelBuilder.Entity<TbUsuario>(entity =>
			{
				entity.HasKey(e => e.CodigoUsu).HasName("PRIMARY");

				entity
					.ToTable("tb_admin_usuarios")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.CodigoUsu)
					.HasMaxLength(3)
					.HasColumnName("codigo_usu");
				entity.Property(e => e.ClaveUsu)
					.HasMaxLength(150)
					.HasColumnName("clave_usu");
				entity.Property(e => e.CodigoDep)
					.HasMaxLength(2)
					.HasColumnName("codigo_dep");
				entity.Property(e => e.Estado)
					.HasMaxLength(1)
					.HasColumnName("estado");
				entity.Property(e => e.FechaCrea)
					.HasColumnType("datetime")
					.HasColumnName("fecha_crea");
				entity.Property(e => e.FechaMod)
					.HasColumnType("datetime")
					.HasColumnName("fecha_mod");
				entity.Property(e => e.LoginUsu)
					.HasMaxLength(15)
					.HasColumnName("login_usu");
				entity.Property(e => e.NombreUsu)
					.HasMaxLength(45)
					.HasColumnName("nombre_usu");
				entity.Property(e => e.UsrTacas)
					.HasMaxLength(45)
					.HasColumnName("usr_tacas");
				entity.Property(e => e.ClaveTacas)
					.HasMaxLength(150)
					.HasColumnName("clave_tacas");
				entity.Property(e => e.UsrMerengue)
					.HasMaxLength(45)
					.HasColumnName("usr_merengue");
				entity.Property(e => e.ClaveMerengue)
					.HasMaxLength(45)
					.HasColumnName("clave_merengue");
			});

			modelBuilder.Entity<TbRolPermiso>(entity =>
			{
				entity.HasKey(e => e.Id).HasName("PRIMARY");

				entity
					.ToTable("tb_admin_rol_permisos")
					.HasCharSet("latin1")
					.UseCollation("latin1_swedish_ci");

				entity.Property(e => e.CodigoUsu)
					.HasMaxLength(3)
					.HasColumnName("Codigo_usu");
				entity.Property(e => e.NombreUsu)
					.HasMaxLength(100)
					.HasColumnName("Nombre_usu");
				entity.Property(e => e.Rol)
					.HasMaxLength(45)
					.HasColumnName("Rol");
				entity.Property(e => e.Permiso)
					.HasMaxLength(45)
					.HasColumnName("Permiso");
			});

			#endregion

			OnModelCreatingPartial(modelBuilder);

		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
