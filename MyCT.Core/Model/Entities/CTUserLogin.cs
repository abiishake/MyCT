﻿using Microsoft.AspNetCore.Identity;
using MyCT.Core.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCT.Core.Model.Entities
{
    public class CTUserLogin : IdentityUserLogin<int>, IEntity
    {
        public int Id { get ; set ; }
    }
}