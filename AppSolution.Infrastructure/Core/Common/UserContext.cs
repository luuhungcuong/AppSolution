﻿using AppSolution.Infrastructure.DataModel;
using AppSolution.Infrastructure.Module.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Core
{
    public class UserContext
    {
        public string UserName { get; set; }
        public DbContextHelper<AppSolutionDbContext> Transaction { get; set; }
    }
}
