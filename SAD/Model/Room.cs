﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Model
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public virtual List<CardRoom> Cards { get; set; }
    }
}
