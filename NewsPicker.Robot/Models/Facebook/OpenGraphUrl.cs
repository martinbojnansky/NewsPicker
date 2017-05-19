﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Robot.Models.Facebook
{
    [DataContract]
    public class OpenGraphUrl
    {
        [DataMember(Name = "share")]
        public OpenGraphShare Share { get; set; }
    }
}
