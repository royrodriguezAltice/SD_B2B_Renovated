using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Provisioning.Templates
{
    public interface ISubTemplates
    {
        string CreateInterface(string noEquipo, string noVlan);
        string ConfigureRoutePolicy(string noEquipment);
        string ConfigureRouterOspf();
        string ConfigureRouterBgp(bool routeDIndicator, bool neighboorIpApn);
    }
}
