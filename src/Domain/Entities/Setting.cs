﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Setting : BaseEntity
    {
        public bool DisableRegistration { get; set; } = false;
    }
}
