﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Provisioning.Templates
{
    public interface ITemplates
    {
        Task<string> GenerateTemplate();
    }
}
