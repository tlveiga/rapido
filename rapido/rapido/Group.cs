﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rapido
{
    public class Group: List<Body>
    {
        public string ID { get; private set; }

        public Group(string groupid)
        {
            ID = groupid;
        }
    }
}
