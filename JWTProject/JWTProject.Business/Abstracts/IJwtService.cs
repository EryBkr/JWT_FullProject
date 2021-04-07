﻿using JWTProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Business.Abstracts
{
    public interface IJwtService
    {
        string GenerateJwt(AppUser appUser, List<AppRole> roles);
    }
}
