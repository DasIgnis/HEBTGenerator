﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class TimeRange
    {
        public int Start;
        public int End;

        public int GetStart() {  return Start; }
    }
}
