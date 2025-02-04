using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Application.Provisioning.Control_OC.Template.Exceptions;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class LoopbackTemplate : ILoopbackTemplate
    {
        private static GetOcDTO _tbOc;
        private StringBuilder _sb;

        public LoopbackTemplate(GetOcDTO tbOc) 
        {
            _tbOc = tbOc;
            _sb?.Clear();
        }

        private StringBuilder GetBuilder()
        {
            return _sb ?? new StringBuilder();
        }
        private void CleanBuilder()
        {
            _sb?.Clear();
        }

        #region Add Lines

        #region Add Lines Genericas
        public string AddPad()
        {
            return "#";
        }
        public string AddAdmiration()
        {
            return "!";
        }

        public string AddJump()
        {
            return "<br>";
        }

        public string AddLineDescription()
        {
            string lastOctect = _tbOc.IpClienteWAg.Split(".").Last();
            string ip = _tbOc.IpClienteWAg.Replace(lastOctect, "0");

            return $" description Premium Plus IP Fija (Internet){ip}/24";
        }

        public string AddLineInterface()
        {
            return $"Interface loopback{_tbOc.LoopBack}";
        }

        #endregion

        #region Add Lines MAN
        public string AddLineVrfMan()
        {
            return $" ip binding vpn-instance {_tbOc.VpnAg}";
        }
        public string AddLineIpAddressMan()
        {
            return $" ip address {_tbOc.IpIspAg} 255.255.255.0";
        }
        #endregion

        #region Add Lines ISP
        public string AddLineVrfIsp()
        {
            return $" vrf {_tbOc.VpnAg}";
        }
        public string AddLineIpAddressIsp()
        {
            return $" ipv4 address {_tbOc.IpIspAg} 255.255.255.0";
        }
        #endregion

        #region AddLines APN

        private string AddLineDescriptionApn()
        {
            return $"description eBGP DGW_Corp_APN-{_tbOc.VpnAg.ToUpper()}";
        }

        private string AddLineIpV4AddressApn(string noEquipment)
        {
            return noEquipment == "Primero" ? "ipv4 address 172.16.0.1 255.255.255.255" : "ipv4 address 172.16.4.1 255.255.255.255";
        }

        #endregion

        #endregion

        private string Huawei()
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLineInterface());
            _sb.AppendLine(AddLineDescription());
            _sb.AppendLine(AddLineVrfMan());
            _sb.AppendLine(AddLineIpAddressMan());
            _sb.AppendLine(AddPad());

            return _sb.ToString();
        }

        private string Cisco()
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineInterface());
            _sb.AppendLine(AddLineDescription());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineIpAddressIsp());
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }

        private string CiscoApn(string noEquipment)
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLineInterface());
            _sb.AppendLine(AddLineDescriptionApn());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineIpV4AddressApn(noEquipment));
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }

        public string CreateTemplate(string noEquipment)
        {
            try
            {
                //Clean the builder before make the template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return Huawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return _tbOc.Producto == "APN" ? CiscoApn(noEquipment) : Cisco();
                }

                return "";
            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
    }
}
