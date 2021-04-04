﻿using JWTProject.DataAccess.Abstracts;
using JWTProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.DataAccess.Concrete.EntityFramework.Repository
{
    public class EfRoleRepository:EfGenericRepository<AppRole>,IRoleDal
    {
    }
}
