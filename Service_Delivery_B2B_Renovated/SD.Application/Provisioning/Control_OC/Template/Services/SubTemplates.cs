using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Application.Provisioning.Control_OC.Template.Exceptions;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using SD.Domain.Entities.Provisioning.Templates.Profiles;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class SubTemplates : ServiceTemplate, ISubTemplates
    {
        private static GetOcDTO _tbOc;
        private IGenericRepository<TbProfiles> _profileRepository;
        private StringBuilder _sb;

        public SubTemplates(GetOcDTO tbOc, IGenericRepository<TbProfiles> profileRepository) : base()
        {
            _tbOc = tbOc;
            _profileRepository = profileRepository;
            _profileRepository.NoTrackingBehaivour();
            _sb?.Clear();
        }

        #region Formatting Functions
        private StringBuilder GetBuilder()
        {
            return _sb ?? new StringBuilder();
        }
        private void CleanBuilder()
        {
            _sb?.Clear();
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

        #region AddLines APN
        private string AddLineIpV4AddressApn(string noVlan, string noEquipment)
        {

            if (noVlan == "Primera" && noEquipment == "Primero")
            {
                return "ipv4 address 10.170.208.1 255.255.255.248";
            }
            else if (noVlan == "Segunda" && noEquipment == "Primero")
            {
                return "ipv4 address 10.170.208.9 255.255.255.248";
            }
            else if (noVlan == "Primera" && noEquipment == "Segundo")
            {
                return "ipv4 address 192.168.208.1 255.255.255.248";
            }
            else if (noVlan == "Segunda" && noEquipment == "Segundo")
            {
                return "ipv4 address 192.168.208.9 255.255.255.248";
            }
            else
            {
                return "";
            }

        }
        private string AddLineInterfaceApn(string noVlan)
        {
            return noVlan == "Primera" ? $"interface Bundle-Ether70.{_tbOc.VlanAcceso}" : $"interface Bundle-Ether70.{_tbOc.ApnData.SegundaVlan}";
        }
        private string AddLineDescriptionVlanApn(string noVlan)
        {
            return noVlan == "Primera" ? $"description to DGW_Corp_APN-{_tbOc.VpnAg.ToUpper()}-{_tbOc.VlanAcceso}" : $"description to DGW_Corp_APN-{_tbOc.VpnAg.ToUpper()}-{_tbOc.ApnData.SegundaVlan}";
        }
        private string AddLineDescriptionApn()
        {
            return $"description to DGW_Corp_APN-{_tbOc.VpnAg.ToUpper()}";
        }
        private string AddLinePrefixSetApn(string noEquipment)
        {
            if (noEquipment == "Primero")
            {
                return $"prefix-set PREFIX-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN <br>" +
                $" {_tbOc.ApnData.IpRoutePolicy}/{_tbOc.ApnData.MaskRoutePolicy} le 32 <br>" +
                "end-set";
            }
            else
            {
                return $"prefix-set PREFIX-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN <br>" +
                $" {_tbOc.ApnData.SecondIpRoutePolicy}/{_tbOc.ApnData.MaskRoutePolicy} le 32 <br>" +
                "end-set";
            }
        }
        private string AddLineRoutePolicyApn()
        {
            return $"route-policy RPL-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN";
        }
        private string AddLineRoutePolicyInApn()
        {
            return $"route-policy RPL-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN in";
        }
        private string AddLineRoutePolicyOutApn()
        {
            return "route-policy PREFIX-DEFAULT out";
        }
        private string AddLineRoutePolicyQueryApn(string noEquipment)
        {
            if (noEquipment == "Primero")
            {
                return $" if destination in PREFIX-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN then <br>" +
                    "  set local-preference 130 <br>" +
                    "  pass <br>" +
                    " endif <br>" +
                    "end-policy";
            }
            else
            {
                return $" if destination in PREFIX-GI-PrivateAPN_{_tbOc.VpnAg.Replace("b2b-", "").ToUpper()}-IN then <br>" +
                    "  set local-preference 120 <br>" +
                    "  pass <br>" +
                    " endif <br>" +
                    "end-policy";
            }
        }
        private string AddLineRouteOspf()
        {
            return "router ospf DGW-Corp";
        }
        private string AddLineApplyGroupApn()
        {
            return "apply-group APN_DGW_Corp";
        }
        private string AddLineAreaApn()
        {
            return "area 0.0.7.99";
        }
        private string AddLineInterfaceLoopback()
        {
            return $"interface Loopback{_tbOc.LoopBack}";
        }
        private string AddLineRouterBgp()
        {
            return "router bgp 28118";
        }
        private string AddLineUpdateSourceLoopback()
        {
            return $"update-source Loopback{_tbOc.LoopBack}";
        }
        private string AddLineRouterRDApn(bool routeDIndicator)
        {
            return routeDIndicator ? $"rd 28118:251213{_tbOc.ApnData.RouterTarget}" : $"rd 28118:250061{_tbOc.ApnData.RouterTarget}";
        }
        private string AddLineVlanTypeIspApn(string noVlan)
        {
            return noVlan == "Primera" ? $"encapsulation dot1q {_tbOc.VlanAcceso}" : $"encapsulation dot1q {_tbOc.ApnData.SegundaVlan}";
        }
        private string AddLineNeighboorIpApn(bool neighboorIndicator)
        {
            return neighboorIndicator ? "neighbor 172.16.0.2" : "neighbor 172.16.4.2";
        }

        #endregion

        #region Equipos
        private string CreateInterfaceHuawei()
        {
            return "";
        }
        private string CreateInterfaceCisco(string noEquipment, string noVlan)
        {

            _sb = GetBuilder();

            _sb.AppendLine(AddLineInterfaceApn(noVlan));
            _sb.AppendLine(AddLineDescriptionVlanApn(noVlan));
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineIpV4AddressApn(noVlan, noEquipment));
            _sb.AppendLine(AddLineVlanTypeIspApn(noVlan));
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }
        private string ConfigureRoutePolicyHuawei()
        {
            return "";
        }
        private string ConfigureRoutePolicyCisco(string noEquipment)
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLinePrefixSetApn(noEquipment));
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineRoutePolicyApn());
            _sb.AppendLine(AddLineRoutePolicyQueryApn(noEquipment));
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }
        private string ConfigureRouterOspfHuawei()
        {
            return "";
        }
        private string ConfigureRouterOspfCisco()
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLineRouteOspf());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineApplyGroupApn());
            _sb.AppendLine(AddLineAddressFamily());
            _sb.AppendLine(AddLineAreaApn());
            _sb.AppendLine(AddLineInterfaceApn("Primera")); //Inteface de la primera Vlan
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineInterfaceApn("Segunda")); //Inteface de la segunda Vlan
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineInterfaceLoopback());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }
        private string ConfigureRouterBgpHuawei()
        {
            return "";
        }
        private string ConfigureRouterBgpCisco(bool routeDIndicator, bool neighboorIpApn)
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLineRouterBgp());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineRouterRDApn(routeDIndicator));
            _sb.AppendLine(AddLineAddressFamily());
            _sb.AppendLine("label mode per-vrf");
            _sb.AppendLine("network 0.0.0.0/0");
            _sb.AppendLine("redistribute connected");
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineNeighboorIpApn(neighboorIpApn));
            _sb.AppendLine("remote-as 64640");
            _sb.AppendLine("use neighbor-group DGW_Corp-PEER");
            _sb.AppendLine("bfd fast-detect");
            _sb.AppendLine("ebgp-multihop 2");
            _sb.AppendLine("local-as 64641 no-prepend replace-as");
            _sb.AppendLine(AddLineDescriptionApn());
            _sb.AppendLine(AddLineUpdateSourceLoopback());
            _sb.AppendLine(AddLineAddressFamily());
            _sb.AppendLine(AddLineRoutePolicyInApn());
            _sb.AppendLine(AddLineRoutePolicyOutApn());
            _sb.AppendLine("soft-reconfiguration inbound always");
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }
        #endregion

        #region SubTemplates
        public string CreateInterface(string noEquipment, string noVlan)
        {
            try
            {
                //Limpiar el builder antes de confeccionar el template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return CreateInterfaceHuawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return CreateInterfaceCisco(noEquipment, noVlan);
                }
                return "";
            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
        public string ConfigureRoutePolicy(string noEquipment)
        {
            try
            {
                //Limpiar el builder antes de confeccionar el template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return ConfigureRoutePolicyHuawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return ConfigureRoutePolicyCisco(noEquipment);
                }
                return "";
            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
        public string ConfigureRouterOspf()
        {
            try
            {
                //Limpiar el builder antes de confeccionar el template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return ConfigureRouterOspfHuawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return ConfigureRouterOspfCisco();
                }
                return "";
            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
        public string ConfigureRouterBgp(bool routeDIndicator, bool neighboorIpApn)
        {
            try
            {
                //Limpiar el builder antes de confeccionar el template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return ConfigureRouterBgpHuawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return ConfigureRouterBgpCisco(routeDIndicator, neighboorIpApn);
                }
                return "";
            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
        #endregion
    
    }
}
