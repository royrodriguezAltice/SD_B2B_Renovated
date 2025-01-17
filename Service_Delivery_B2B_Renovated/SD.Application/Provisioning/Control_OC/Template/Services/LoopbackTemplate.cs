using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Domain.Entities.Provisioning.Control_OC.Oc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class LoopbackTemplate : ILoopbackTemplate
    {
        private static TbOc _tbOc;
        private StringBuilder _sb;

        public LoopbackTemplate(TbOc tbOc) 
        {
            _tbOc = tbOc;
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

        private string AddLineIpV4AddressApn(string noEquipo)
        {
            return noEquipo == "Primero" ? "ipv4 address 172.16.0.1 255.255.255.255" : "ipv4 address 172.16.4.1 255.255.255.255";
        }

        #endregion

        #endregion

        public Task<string> CreateTemplate(string noEquipment)
        {
            throw new NotImplementedException();
        }
    }
}
