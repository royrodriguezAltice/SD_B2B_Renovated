using SD.Domain.Entities.Provisioning.Control_OC.Apn;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Entities.Provisioning.Control_OC.Oc
{
    public partial class TbOc
    {
        public int Id { get; set; }

        public DateOnly? Fecha { get; set; }

        public string? Oc { get; set; }

        public string? Os { get; set; }

        public string? Circuito { get; set; }

        public string? Cliente { get; set; }

        public string Actividad { get; set; } = null!;

        public string? Bw { get; set; }

        public string Producto { get; set; } = null!;

        public string? NodoAcceso { get; set; }

        public string? PuertoAcceso { get; set; }

        public string? VlanAcceso { get; set; }

        public string? UplinkAcceso { get; set; }

        public string? EqDemarcacionAcceso { get; set; }

        public string? PuertoTransIn { get; set; }

        public string? TxInTrans { get; set; }

        public string? TxOutTrans { get; set; }

        public string? PuertoTransOut { get; set; }

        public string? EquipoAg { get; set; }

        public string? TipoPuertoAg { get; set; }

        public string? PuertoAg { get; set; }

        public string? VpnAg { get; set; }

        public string? IpIspAg { get; set; }

        public string? IpIspMask { get; set; }

        public string? IpClienteWAg { get; set; }

        public string? IpClienteWMask { get; set; }

        public string? IpClienteLAg { get; set; }

        public string? IpClienteLMask { get; set; }

        public string? RealmAg { get; set; }

        public string? VlanMgmtAg { get; set; }

        public string? IpGestionAg { get; set; }

        public string? ManagmentRouter { get; set; }

        public string? ManagmentSwitch { get; set; }

        public string? Ttk { get; set; }

        public string? FechaSolicitudIng { get; set; }

        public string? FechaRespuestaIng { get; set; }

        public int? Semana { get; set; }

        public string? DescripOc { get; set; }

        public string? Descripcion { get; set; }

        public string? Bandwidth { get; set; }

        public string? Estatus { get; set; }

        public string? TipoServicio { get; set; }

        public string? IdGrado { get; set; }

        public string? IpPrefix { get; set; }

        public string? Template { get; set; }

        public int? Idip { get; set; }

        public int? Id2 { get; set; }

        public int? Id3 { get; set; }

        public string? UsuarioCreo { get; set; }

        public string? IpMngRouterMask { get; set; }

        public string? Mngtag { get; set; }

        public string? Prefix { get; set; }

        public string? Bws { get; set; }

        public string? GPON_P2P { get; set; }

        public string? LoopBack { get; set; }

        public string? CreateLoopBack { get; set; }

        public string? CreateVPN { get; set; }

        public string? RouterRD { get; set; }

        public string? PON { get; set; }

        public string? PosicionOnt { get; set; }

        public string? ClienteRutaEstatica { get; set; }

        public string? NombreFactura { get; set; }

        public string? NumeroCuenta { get; set; }

        public string? Plan { get; set; }

        public string? MangaGPON { get; set; }

        public string? PuertoMG { get; set; }

        public string? GestionIpP2P { get; set; }

        public string? GestionVlanP2P { get; set; }

        public string? GestionIpSwitch { get; set; }

        public string? GestionIpRouter { get; set; }

        public string? GestionPrimeraMultiVlan { get; set; }

        public string? GestionSegundaMultiVlan { get; set; }

        public string? CoordenadasClienteY { get; set; }

        public string? CoordenadasClienteX { get; set; }

        public string? CoordenadasMangaY { get; set; }

        public string? CoordenadasMangaX { get; set; }

        public string? Site { get; set; }

        public string? Serial_ONT { get; set; }

        public string? ApnId { get; set; }

        [NotMapped]
        public TbApn? ApnData { get; set; }
    }
}
