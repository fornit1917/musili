﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musili.WebApi.Models
{
    public class TracksCriteria
    {
        public Genre Genre { get; set; }
        public Tempo Tempo { get; set; }
    }
}
