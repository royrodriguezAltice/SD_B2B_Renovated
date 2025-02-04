using SD.Application.Interfaces.Convention;
using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Application.Provisioning.Control_OC.Template.Exceptions;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Entities.Provisioning.Templates.Profiles;
using SD.Domain.Enums.Provisioning.Control_OC;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;
using IFormatter = SD.Application.Interfaces.Convention.IFormatter;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class ServiceTemplate : IServiceTemplate
    {

        public static GetOcDTO _tbOc;
        private StringBuilder _sb;
        private IGenericRepository<TbProfiles> _profileRepository;
        private IFormatter _formatter;

        public ServiceTemplate(GetOcDTO tbOc, IGenericRepository<TbProfiles> profileRepository, IFormatter formatter)
        {
            _tbOc = tbOc;
            _formatter = formatter;
            _profileRepository = profileRepository;
            _profileRepository.NoTrackingBehaivour();
            _sb?.Clear();
        }

        public ServiceTemplate()
        {

        }

        #region Formatting Functions
        private StringBuilder GetBuilder()
        {
            return _sb ?? new StringBuilder();
        }
        private string FormattingBandwith()
        {
            int bandwithInNumbers = 0;

            if (!string.IsNullOrEmpty(_tbOc.Bw))
            {
                // Dividimos el valor por el guión para obtener los dos valores
                string[] parts = _tbOc.Bw.Split('-');

                if (parts.Length == 2)
                {
                    // Obtenemos el valor antes del guión
                    string firstValue = parts[0].Trim();

                    if (firstValue.Contains("MBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("MBPS", "").Trim();

                    }
                    else if (firstValue.Contains("KBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("KBPS", "").Trim();
                    }

                    // Convertimos a int y multiplicamos por 1
                    if (int.TryParse(firstValue, out int numericValue))
                    {
                        bandwithInNumbers = numericValue;
                    }
                }
                else if (parts.Length == 1)
                {
                    // Obtenemos el valor antes del guión
                    string firstValue = parts[0].Trim();

                    if (firstValue.Contains("MBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("MBPS", "").Trim();

                    }
                    else if (firstValue.Contains("KBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("KBPS", "").Trim();
                    }

                    // Convertimos a int y multiplicamos por 1
                    if (int.TryParse(firstValue, out int valorNumerico))
                    {
                        bandwithInNumbers = valorNumerico;
                    }
                }

                return bandwithInNumbers.ToString();
            }
            return "";
        }
        #endregion

        //Lineas de comandos de los diversos templates
        #region AddLine
        // Metodo que verifica el shaping profile de los equipos en especifico
        private string VerificarProfileMan()
        {
            try
            {
                decimal velocidad = 0;
                TbProfiles profile = (TbProfiles)_profileRepository.VerifyDataExistenceAsync(p => p.Equipo == _tbOc.EquipoAg && p.Perfil == _tbOc.Bw).Result;

                if (ProfileExist())
                {
                    return $"qos-profile {profile.Perfil} outbound";
                }

                profile = new TbProfiles
                {
                    Equipo = _tbOc.EquipoAg,
                    Perfil = _tbOc.Bw
                };

                _profileRepository.CreateAsync(profile);

                if (_tbOc.Bw.Contains("MBPS"))
                {
                    velocidad = Decimal.Parse(_tbOc.Bw.Replace("MBPS", ""));

                }
                else if (_tbOc.Bw.Contains("MB"))
                {
                    velocidad = Decimal.Parse(_tbOc.Bw.Replace("MB", ""));
                }

                return $"qos-profile {_tbOc.Bw} outbound <br>" +
                        $"car cir {velocidad * 1024} pir {velocidad * 1024} cbs {velocidad * 1000} pbs {velocidad * 1024} green pass yellow pass red discard";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string VerificarProfileIsp()
        {
            try
            {
                TbProfiles profile = (TbProfiles)_profileRepository.VerifyDataExistenceAsync(p => p.Equipo == _tbOc.EquipoAg && p.Perfil == _tbOc.Bw).Result;
                if (ProfileExist())
                {
                    return $"qos-profile {profile.Perfil} outbound";
                }

                profile = new TbProfiles
                {
                    Equipo = _tbOc.EquipoAg,
                    Perfil = _tbOc.Bw
                };

                _profileRepository.CreateAsync(profile);

                return $"qos-profile {_tbOc.Bw} outbound";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region AddLines Genericas
        private string AddLineHeaderInstalation()
        {
            return $"Solicitud: {_tbOc.Ttk} <br>" +
                        "<br>" +
                        "************************************************************<br>" +
                        "############CREAR SERVICIO B2B_ #############<br>" +
                        "************************************************************<br>" +
                        $"{_tbOc.EquipoAg}<br>"; ;
        }
        public string AddLineHeader()
        {
            return $"Solicitud: {_tbOc.Ttk}" +
                   "<br>" +
                    $"{_tbOc.EquipoAg}";
        }
        public string AddLineDescriptionB2B()
        {
            return $"description B2B: [{_tbOc.TipoServicio}] : {_tbOc.Circuito} : {_tbOc.IpClienteWAg}/{_tbOc.IpIspMask} : {_tbOc.Oc} : {_tbOc.VpnAg} : {_formatter.UserCreator(engineer: _tbOc.UsuarioCreo)} : {_tbOc.Cliente} ({_tbOc.GPON_P2P})";
        }
        public string AddLineBandWith()
        {
            return $"bandwidth {FormattingBandwith()}";
        }
        public string AddLineInterface()
        {
            return $"interface {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso}";
        }
        public string AddLineRouteStatic()
        {
            return $"ip route-static vpn-instance {_tbOc.VpnAg} {_tbOc.IpClienteWAg}{_tbOc.IpClienteWMask} {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso}{_tbOc.IpClienteWMask} description {_tbOc.Cliente}_{_tbOc.Circuito}";
        }
        public string AddLineStatitics()
        {
            return "statistic enable";
        }
        public string AddLineTraficPolicy()
        {
            return "traffic-policy SPAM_CONTROL inbound";
        }
        public string AddJump()
        {
            return "<br>";
        }
        private string AddPad()
        {
            return "#";
        }
        public string AddAdmiration()
        {
            return "!";
        }
        private string AddReturn()
        {
            return "Return";
        }
        #endregion

        #region AddLines MAN

        #region Funciones MAN
        private string FormattingBandwidthMan()
        {

            if (!string.IsNullOrEmpty(_tbOc.Bw))
            {
                double bandwithInNumbers = 0;

                if (!string.IsNullOrEmpty(_tbOc.Bw))
                {
                    // Dividimos el valor por el guión para obtener los dos valores
                    string[] parts = _tbOc.Bw.Split('-');

                    if (parts.Length == 2)
                    {
                        // Obtenemos el valor antes del guión
                        string firstValue = parts[0].Trim();

                        if (firstValue.Contains("MBPS"))
                        {
                            // Removemos "MBPS" y espacios en blanco
                            firstValue = firstValue.Replace("MBPS", "").Trim();

                        }
                        else if (firstValue.Contains("KBPS"))
                        {
                            // Removemos "MBPS" y espacios en blanco
                            firstValue = firstValue.Replace("KBPS", "").Trim();
                        }

                        // Convertimos a int y multiplicamos por 1
                        if (double.TryParse(firstValue, out double valorNumerico))
                        {
                            bandwithInNumbers = valorNumerico * 1;
                        }
                    }
                    else if (parts.Length == 1)
                    {
                        // Obtenemos el valor antes del guión
                        string firstValue = parts[0].Trim();

                        if (firstValue.Contains("MBPS"))
                        {
                            // Removemos "MBPS" y espacios en blanco
                            firstValue = firstValue.Replace("MBPS", "").Trim();

                        }
                        else if (firstValue.Contains("KBPS"))
                        {
                            // Removemos "MBPS" y espacios en blanco
                            firstValue = firstValue.Replace("KBPS", "").Trim();
                        }

                        // Convertimos a int y multiplicamos por 1
                        if (double.TryParse(firstValue, out double valorNumerico))
                        {
                            bandwithInNumbers = valorNumerico * 1;
                        }
                    }

                    return bandwithInNumbers.ToString();
                }
            }
            return "";
        }
        #endregion
        private string AddLineBandWithMAN()
        {
            return $"bandwidth {FormattingBandwidthMan()}";
        }
        private string AddLineRouteStaticMan()
        {
            return $"ip route-static vpn-instance {_tbOc.VpnAg} {_tbOc.IpClienteWAg} {_tbOc.IpClienteWMask} {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso} {_tbOc.IpClienteWAg} description {_tbOc.Cliente}_{_tbOc.Circuito}";
        }
        private string AddLineLoopbackMan()
        {
            return $"ip address unnumbered interface LoopBack{_tbOc.LoopBack}";
        }
        private string AddLineVrfMan()
        {
            return $"ip binding vpn-instance {_tbOc.VpnAg}";
        }
        private string AddLineProxyMan()
        {
            return "arp-proxy enable";
        }
        private string AddLineVlanTypeMan()
        {
            return $"vlan-type dot1q {_tbOc.VlanAcceso}";
        }
        private string AddLineIpAddressMan()
        {
            return $"ip address {_tbOc.IpIspAg} {_tbOc.IpClienteWMask}";
        }
        private string AddLineSecondIpAddressMan()
        {
            return $"ip address {_tbOc.IpIspAg} {_tbOc.IpClienteLMask} sub ";
        }

        #region Upgrade
        private string AddLineRemoveProfile()
        {
            return "undo qos-profile outbound";
        }
        #endregion

        #region Quitese
        private string AddLineRemoveInterfaceMan()
        {
            return $"undo interface {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso}";
        }

        #endregion

        #endregion

        #region AddLines ISP

        #region Funciones ISP
        private string FormattingBandwithISP()
        {
            double bandwithInNumbers = 0;

            if (!string.IsNullOrEmpty(_tbOc.Bw))
            {
                // Dividimos el valor por el guión para obtener los dos valores
                string[] parts = _tbOc.Bw.Split('-');

                if (parts.Length == 2)
                {
                    // Obtenemos el valor antes del guión
                    string firstValue = parts[0].Trim();

                    if (firstValue.Contains("MBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("MBPS", "").Trim();

                    }
                    else if (firstValue.Contains("KBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("KBPS", "").Trim();
                    }

                    // Convertimos a int y multiplicamos por 1
                    if (double.TryParse(firstValue, out double valorNumerico))
                    {
                        bandwithInNumbers = valorNumerico * 1000;
                    }
                }
                else if (parts.Length == 1)
                {
                    // Obtenemos el valor antes del guión
                    string firstValue = parts[0].Trim();

                    if (firstValue.Contains("MBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("MBPS", "").Trim();

                    }
                    else if (firstValue.Contains("KBPS"))
                    {
                        // Removemos "MBPS" y espacios en blanco
                        firstValue = firstValue.Replace("KBPS", "").Trim();
                    }

                    // Convertimos a int y multiplicamos por 1
                    if (double.TryParse(firstValue, out double valorNumerico))
                    {
                        bandwithInNumbers = valorNumerico * 1000;
                    }
                }

                return bandwithInNumbers.ToString();
            }
            return "";
        }
        #endregion
        private string AddLineBandwithISP()
        {
            string bandwith = FormattingBandwithISP();
            return $"bandwidth {bandwith}";
        }
        protected string AddLineVlanTypeIsp()
        {
            return $" encapsulation dot1q {_tbOc.VlanAcceso}";
        }
        private string AddLineLoopbackIsp()
        {
            return $" ipv4 unnumbered LoopBack{_tbOc.LoopBack}";
        }
        private string AddLineRouteStaticIsp()
        {
            return $"   {_tbOc.IpClienteWAg}/{_tbOc.IpIspMask} {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso} " +
                                         $"description {_tbOc.ClienteRutaEstatica}_{_tbOc.Circuito}";
        }
        private string AddLineProxyIsp()
        {
            return " local-proxy-arp";
        }
        private string AddLineAccessGroup()
        {
            return " ipv4 access-group premium-plus-gpon-spam-control ingress";
        }
        protected string AddLineVrfIsp()
        {
            return $"vrf {_tbOc.VpnAg}";
        }
        private string AddLineRouterStaticVrfIsp()
        {
            return $"router static vrf {_tbOc.VpnAg} address-family ipv4 unicast";
        }
        private string AddLineIpV4P2P()
        {
            return " ipv4 point-to-point";
        }
        protected string AddLineAddressFamily()
        {
            return " address-family ipv4 unicast";
        }
        protected string AddLineIpV4AddressIsp()
        {
            return $"ipv4 address {_tbOc.IpIspAg} {_tbOc.IpClienteWMask}";
        }
        private string AddLineSecondIpV4AddressIsp()
        {
            return $"ipv4 address {_tbOc.IpIspAg} {_tbOc.IpClienteLMask} secondary ";
        }

        #region Quitese
        private string AddLineRemoveRouteStaticIsp()
        {
            return $"no {_tbOc.IpClienteWAg} {_tbOc.IpClienteWMask} {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso} {_tbOc.IpClienteWMask} ";
        }
        private string AddLineRemoveInterfaceIsp()
        {
            return $"no interface {_tbOc.TipoPuertoAg} {_tbOc.PuertoAg}.{_tbOc.VlanAcceso}<br>";
        }
        #endregion

        #region CASO APN

        private string AddLineInterfaceBvi()
        {
            return $"interface BVI{_tbOc.ApnData.SegundaVlan}";
        }
        private string AddLineDescriptionB2BAPN()
        {
            return $"description to B2B: [{_tbOc.TipoServicio}] : {_tbOc.Circuito} : {_tbOc.ApnData.NombreApn} : {_tbOc.Oc} : {_tbOc.VpnAg} : {_formatter.UserCreator(engineer: _tbOc.UsuarioCreo)} : {_tbOc.Cliente} ({_tbOc.GPON_P2P})";
        }
        private string AddLineInterfaceFirstPort(bool indicadorTransport)
        {
            if (_tbOc.EquipoAg.Contains("ISP777"))
            {
                return indicadorTransport ? $"interface TenGigE0/2/1/2.{_tbOc.ApnData.SegundaVlan} l2transport" : $"interface TenGigE0/2/1/2.{_tbOc.ApnData.SegundaVlan}";
            }
            else if (_tbOc.EquipoAg.Contains("ISP799"))
            {
                return indicadorTransport ? $"interface TenGigE0/2/0/1.{_tbOc.ApnData.SegundaVlan} l2transport" : $"interface TenGigE0/2/0/1.{_tbOc.ApnData.SegundaVlan}";
            };
            return "";

        }
        private string AddLineInterfaceSecondPort(bool indicadorTransport)
        {
            if (_tbOc.EquipoAg.Contains("ISP777"))
            {
                return indicadorTransport ? $"interface TenGigE0/3/0/1.{_tbOc.ApnData.SegundaVlan} l2transport" : $"interface TenGigE0/3/0/1.{_tbOc.ApnData.SegundaVlan}";
            }
            else if (_tbOc.EquipoAg.Contains("ISP799"))
            {
                return indicadorTransport ? $"interface TenGigE0/0/0/30.{_tbOc.ApnData.SegundaVlan} l2transport" : $"interface TenGigE0/0/0/30.{_tbOc.ApnData.SegundaVlan}";
            };
            return "";
        }
        private string AddLineDescriptionApn(bool descIndicator)
        {
            if (descIndicator)
            {
                return _tbOc.EquipoAg.EndsWith("PE2") ? $"description to {_tbOc.EquipoAg.Replace("PE2", "")}NBIG2-1 {_tbOc.VpnAg.Replace("b2b-", "").Replace("-0", "").ToUpper()}-LAN-0 - b2b - Clients" : $"description to {_tbOc.EquipoAg.Replace("PE3", "")}NBIG2-1 {_tbOc.VpnAg.Replace("b2b-", "").Replace("-0", "").ToUpper()}-LAN-0 - b2b - Clients";

            }
            else
            {
                return _tbOc.EquipoAg.EndsWith("PE2") ? $"description to {_tbOc.EquipoAg.Replace("PE2", "")}NBIG2-2 {_tbOc.VpnAg.Replace("b2b-", "").Replace("-0", "")}-LAN-0 - b2b - Clients" : $"description to {_tbOc.EquipoAg.Replace("PE3", "")}NBIG2-2 {_tbOc.VpnAg.Replace("b2b-", "").Replace("-0", "")}-LAN-0 - b2b - Clients";
            };
        }
        private string AddLineBridgeGroup()
        {
            return _tbOc.EquipoAg.EndsWith("PE2") ? $"bridge group {_tbOc.EquipoAg.Replace("PE2", "")}NBIG2" : $"bridge group {_tbOc.EquipoAg.Replace("PE3", "")}NBIG2";
        }
        private string AddLineBridgeDomain()
        {
            return $"bridge-domain {_tbOc.VpnAg}";
        }
        private string AddLineFirstRouteStatic()
        {
            return $"0.0.0.0/0 BVI{_tbOc.ApnData.SegundaVlan} 10.170.221.2 description \"B2B_DEFAULT_{_tbOc.VpnAg.ToUpper()}\"";
        }
        private string AddLineSecondRouteStatic()
        {
            if (_tbOc.EquipoAg.Contains("ISP777"))
            {
                return $"{_tbOc.IpClienteWAg}/{_tbOc.IpClienteWMask} BVI2999 172.17.45.209 description \"B2B_PUBLIC_RANGE_{_tbOc.VpnAg.ToUpper()}\"";
            }
            else if (_tbOc.EquipoAg.Contains("ISP799"))
            {
                return $"{_tbOc.IpClienteWAg}/{_tbOc.IpClienteWMask} BVI2999 172.17.45.217 description \"B2B_PUBLIC_RANGE_{_tbOc.VpnAg.ToUpper()}\"";
            };
            return "";
        }
        private string AddLineVlanTypeIspApn()
        {
            return $" encapsulation dot1q {_tbOc.ApnData.SegundaVlan}";
        }

        #endregion

        #endregion

        #endregion

        #region Actividades
        private string Instalations(string equipo) //INSTALACIONES
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalación Internet Asimetrico 1 ip Fija equipo MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineBandWithMAN());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Instalación Internet Asimetrico 1 ip Fija equipo ISP
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4P2P());
                        _sb.AppendLine(AddLineLoopbackIsp());
                        _sb.AppendLine(AddLineBandwithISP());
                        _sb.AppendLine(AddLineProxyIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddLineAccessGroup());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine("router static ");
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineAddressFamily());
                        _sb.AppendLine(AddLineRouteStaticIsp());
                    }
                }

            }
            else if ((_tbOc.Producto == "2" || _tbOc.Producto == "INTERNET SIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 1 IP DED INTERNET MAN
                    {

                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineBandWithMAN());
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStatic());
                        _sb.AppendLine(AddJump());

                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "3" || _tbOc.Producto == "INTERNET SIMETRICO 2 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 2 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "4" || _tbOc.Producto == "INTERNET SIMETRICO 8 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineBandWithMAN());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineBandWithMAN());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineBandWithMAN());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET ISP
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddLineBandwithISP());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4AddressIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddAdmiration());
                    }
                }
            }
            else if ((_tbOc.Producto == "14" || _tbOc.Producto == "APN"))
            {
                if (_tbOc.TipoServicio == "APN")
                {
                    if (equipo == OCEquipos.MAN.ToString())
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {

                        }
                        else
                        {

                        }
                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {
                        _sb.AppendLine(AddLineInterfaceBvi()); // Primera Vlan
                        _sb.AppendLine(AddLineDescriptionB2BAPN());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine("ipv4 address 10.170.221.1 255.255.255.252");
                        _sb.AppendLine("load-interval 30");
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddLineInterfaceFirstPort(true)); //Primera Vlan
                        _sb.AppendLine(AddLineDescriptionApn(true)); //Primer Equipo NBIG
                        _sb.AppendLine(AddLineVlanTypeIspApn()); //Primer Equipo NBIG
                        _sb.AppendLine("rewrite ingress tag  pop 1 symmetric");
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddLineInterfaceSecondPort(true)); //Primera Vlan
                        _sb.AppendLine(AddLineDescriptionApn(false)); //Primer Equipo NBIG
                        _sb.AppendLine(AddLineVlanTypeIspApn()); //Primer Equipo NBIG
                        _sb.AppendLine("rewrite ingress tag  pop 1 symmetric");
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine("L2vpn");
                        _sb.AppendLine(AddLineBridgeGroup()); //Primer Equipo NBIG
                        _sb.AppendLine(AddLineBridgeDomain());
                        _sb.AppendLine(AddLineInterfaceFirstPort(false)); //Primera Vlan
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddLineInterfaceSecondPort(false)); //Primera Vlan
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine($"routed {AddLineInterfaceBvi()}"); //Primera Vlan
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine("router static");
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineAddressFamily());
                        _sb.AppendLine(AddLineFirstRouteStatic()); //Primera Vlan
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine("router static");
                        _sb.AppendLine("vrf isp-lacnic");
                        _sb.AppendLine(AddLineAddressFamily());
                        _sb.AppendLine(AddLineSecondRouteStatic());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddJump());
                    }
                }
            }

            return _sb.ToString();
        }
        private string Traslado(string equipo) //TRASLADOS
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalación Internet Asimetrico 1 ip Fija equipo MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineBandWithMAN());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Instalación Internet Asimetrico 1 ip Fija equipo ISP
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4P2P());
                        _sb.AppendLine(AddLineLoopbackIsp());
                        _sb.AppendLine(AddLineBandwithISP());
                        _sb.AppendLine(AddLineProxyIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddLineAccessGroup());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine("router static ");
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineAddressFamily());
                        _sb.AppendLine(AddLineRouteStaticIsp());
                    }
                }

            }
            else if ((_tbOc.Producto == "2" || _tbOc.Producto == "INTERNET SIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 1 IP DED INTERNET MAN
                    {

                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineBandWithMAN());
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStatic());
                        _sb.AppendLine(AddJump());

                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "3" || _tbOc.Producto == "INTERNET SIMETRICO 2 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 2 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "4" || _tbOc.Producto == "INTERNET SIMETRICO 8 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())
                    {



                    }
                }

            }
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {

                    if (equipo == OCEquipos.MAN.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET MAN
                    {
                        if ((_tbOc.EquipoAg == "MAN760AR2" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/2/3") || (_tbOc.EquipoAg == "MAN788PE3" && _tbOc.TipoPuertoAg == "GigabitEthernet" && _tbOc.PuertoAg == "0/6/0"))
                        {
                            if (ProfileExist())
                            {
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineBandWithMAN());
                                _sb.AppendLine(AddLineDescriptionB2B());
                                _sb.AppendLine(VerificarProfileMan());
                            }
                            else
                            {
                                _sb.AppendLine(VerificarProfileMan());
                                _sb.AppendLine(AddLineInterface());
                                _sb.AppendLine(AddLineVlanTypeMan());
                                _sb.AppendLine(AddLineBandWithMAN());
                                _sb.AppendLine(AddLineDescriptionB2B());
                            }
                        }
                        else
                        {
                            _sb.AppendLine(AddLineInterface());
                            _sb.AppendLine(AddLineVlanTypeMan());
                            _sb.AppendLine(AddLineBandWithMAN());
                            _sb.AppendLine(AddLineDescriptionB2B());
                        }
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineIpAddressMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddReturn());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Instalacion Internet Simetrico 8 IP DED INTERNET ISP
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddLineBandwithISP());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4AddressIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddAdmiration());
                    }
                }
            }

            return _sb.ToString();
        }
        private string Upgrade(string equipo) //UPGRADES
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {
                    _sb.AppendLine(AddLineInterface());
                    if (equipo == OCEquipos.MAN.ToString()) //Upgrade Internet Asimetrico 1 IP IP FIJA MAN
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());

                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Upgrade Internet Asimetrico 1 IP IP FIJA ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {
                    if (equipo == OCEquipos.MAN.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }
            else if ((_tbOc.Producto == "6" || _tbOc.Producto == "DED DATOS L2"))
            {
                if (_tbOc.TipoServicio == "DED DATOS L2")
                {
                    if (equipo == OCEquipos.MAN.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }
            else if ((_tbOc.Producto == "7" || _tbOc.Producto == "DED DATOS L3"))
            {
                if (_tbOc.TipoServicio == "DED DATOS L3")
                {
                    if (equipo == OCEquipos.MAN.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Upgrade Internet Asimetrico 1 IP SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }

            return _sb.ToString();
        }
        private string Downgrade(string equipo) //DOWNGRADES
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {
                    _sb.AppendLine(AddLineInterface());
                    if (equipo == OCEquipos.MAN.ToString()) //Downgrade Internet Asimetrico 1 IP IP FIJA MAN
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());

                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Downgrade Internet Asimetrico 1 IP IP FIJA ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {
                    if (equipo == OCEquipos.MAN.ToString()) //Downgrade Internet Asimetrico 1 IP SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineInterface());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        if (_tbOc.EquipoAg == "MAN760AR2" || _tbOc.EquipoAg == "MAN788PE3")
                        {
                            _sb.AppendLine(AddLineRemoveProfile());
                            _sb.AppendLine(VerificarProfileMan());
                        }
                        else { }
                        _sb.AppendLine(AddLineBandWithMAN());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //Downgrade Internet Asimetrico 1 IP SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandwithISP());
                    }
                }
            }

            return _sb.ToString();
        }
        private string AddIp(string equipo) //ADD IPS
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {
                    _sb.AppendLine(AddLineInterface());
                    if (equipo == OCEquipos.MAN.ToString()) //ADD IP Internet Asimetrico 1 IP FIJA MAN
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWithMAN());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                        _sb.AppendLine(AddPad());

                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //ADD IP Internet Asimetrico 1 IP FIJA ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddLineRouterStaticVrfIsp());
                        _sb.AppendLine(AddLineRouteStaticIsp());
                        _sb.AppendLine(AddJump());
                    }
                }

            }
            /*else if ((_tbOc.Producto == "4" || _tbOc.Producto == "INTERNET SIMETRICO 8 IP"))
            {
                if (_tbOc.TipoServicio == "DED INTERNET")
                {
                    if (equipo == OCEquipos.MAN.ToString()) //ADD IP Intenet Asimetrico 8 IP MAN
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWith());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                        _sb.AppendLine(AddPad());
                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //ADD IP Intenet Asimetrico 8 IP ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddLineRouterStaticVrfIsp());
                        _sb.AppendLine(AddLineRouteStaticIsp());
                        _sb.AppendLine(AddJump());
                    }
                }
            }*/
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {
                    _sb.AppendLine(AddLineInterface());
                    if (equipo == OCEquipos.MAN.ToString()) //ADD IP SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineSecondIpAddressMan());
                        _sb.AppendLine(AddAdmiration());
                        _sb.AppendLine(AddJump());

                    }
                    else if (equipo == OCEquipos.ISP.ToString()) //ADD IP SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineSecondIpV4AddressIsp());
                        _sb.AppendLine(AddPad());
                    }
                }
            }

            return _sb.ToString();
        }
        private string Quitese(string equipo) //QUITESES
        {
            _sb = GetBuilder();

            if ((_tbOc.Producto == "1" || _tbOc.Producto == "INTERNET ASIMETRICO 1 IP"))
            {
                if (_tbOc.TipoServicio == "IP FIJA")
                {
                    if (equipo == OCEquipos.MAN.ToString())  //Quitese Internet Asimetrico 1 IP FIJA MAN
                    {
                        _sb.AppendLine(AddLineRemoveInterfaceMan());
                        _sb.AppendLine(AddLineVlanTypeMan());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWith());
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                        _sb.AppendLine(AddPad());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())  //Quitese Internet Asimetrico 1 IP FIJA ISP
                    {
                        _sb.AppendLine(AddLineRouterStaticVrfIsp());
                        _sb.AppendLine(AddLineRemoveRouteStaticIsp());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWith());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4P2P());
                        _sb.AppendLine(AddLineLoopbackIsp());
                        _sb.AppendLine(AddLineProxyIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddLineAccessGroup());
                        _sb.AppendLine(AddAdmiration());
                    }
                }
            }
            else if ((_tbOc.Producto == "5" || _tbOc.Producto == "SIP DATOS"))
            {
                if (_tbOc.TipoServicio == "SIP DATOS")
                {
                    if (equipo == OCEquipos.MAN.ToString())  //Quitese SIP DATOS MAN
                    {
                        _sb.AppendLine(AddLineRemoveInterfaceMan());
                        _sb.AppendLine(AddLineVlanTypeMan());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWith());
                        _sb.AppendLine(AddLineVrfMan());
                        _sb.AppendLine(AddLineLoopbackMan());
                        _sb.AppendLine(AddLineStatitics());
                        _sb.AppendLine(AddLineTraficPolicy());
                        _sb.AppendLine(AddLineProxyMan());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddPad());
                        _sb.AppendLine(AddLineRouteStaticMan());
                        _sb.AppendLine(AddPad());
                    }
                    else if (equipo == OCEquipos.ISP.ToString())  //Quitese SIP DATOS ISP
                    {
                        _sb.AppendLine(AddLineRemoveInterfaceIsp());
                        _sb.AppendLine(AddLineDescriptionB2B());
                        _sb.AppendLine(AddLineBandWith());
                        _sb.AppendLine(AddLineVrfIsp());
                        _sb.AppendLine(AddLineIpV4P2P());
                        _sb.AppendLine(AddLineLoopbackIsp());
                        _sb.AppendLine(AddLineProxyIsp());
                        _sb.AppendLine(AddLineVlanTypeIsp());
                        _sb.AppendLine(AddAdmiration());
                    }
                }
            }

            return _sb.ToString();
        }
        #endregion

        #region Equipos

        private string Huawei()
        {
            if (_tbOc.Actividad == "INSTALACION" || _tbOc.Actividad == "1")
            {
                return Instalations(OCEquipos.MAN.ToString());
            }
            else if (_tbOc.Actividad == "TRASLADO" || _tbOc.Actividad == "3")
            {
                return Traslado(OCEquipos.MAN.ToString());
            }
            else if (_tbOc.Actividad == "UPGRADE" || _tbOc.Actividad == "4")
            {
                return Upgrade(OCEquipos.MAN.ToString());
            }
            else if (_tbOc.Actividad == "DOWNGRADE" || _tbOc.Actividad == "5")
            {
                return Downgrade(OCEquipos.MAN.ToString());
            }
            else if (_tbOc.Actividad == "ADD IP" || _tbOc.Actividad == "10")
            {
                return AddIp(OCEquipos.MAN.ToString());
            }
            else if (_tbOc.Actividad == "QUITESE" || _tbOc.Actividad == "2")
            {
                //return Quitese(OCEquipos.MAN.ToString());
                return "";
            }
            return "";
        }
        private string Cisco()
        {
            if (_tbOc.Actividad == "INSTALACION" || _tbOc.Actividad == "1")
            {
                return Instalations(OCEquipos.ISP.ToString());
            }
            else if (_tbOc.Actividad == "TRASLADO" || _tbOc.Actividad == "3")
            {
                return Traslado(OCEquipos.ISP.ToString());
            }
            else if (_tbOc.Actividad == "UPGRADE" || _tbOc.Actividad == "4")
            {
                return Upgrade(OCEquipos.ISP.ToString());
            }
            else if (_tbOc.Actividad == "DOWNGRADE" || _tbOc.Actividad == "5")
            {
                return Downgrade(OCEquipos.ISP.ToString());
            }
            else if (_tbOc.Actividad == "ADD IP" || _tbOc.Actividad == "10")
            {
                return AddIp(OCEquipos.ISP.ToString());
            }
            else if (_tbOc.Actividad == "QUITESE" || _tbOc.Actividad == "2")
            {
                //return Quitese(OCEquipos.ISP.ToString());
                return "";
            }
            return "";
        }

        #endregion

        public string CreateTemplate()
        {
            try
            {
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return Huawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return Cisco();
                }
                return "";
            }
            catch(Exception ex) 
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }

        private bool ProfileExist()
        {
            try
            {
                return _profileRepository.VerifyDataExistenceAsync(p => p.Equipo == _tbOc.EquipoAg && p.Perfil == _tbOc.Bw) == null ? false : true;
            }
            catch(Exception ex)
            {
                throw new VerifyProfileExistenceFailedException();
            }
        }

    }
}
