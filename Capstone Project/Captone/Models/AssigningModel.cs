﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captone.Models
{
    public class AssigningModel
    {
        public int RequestID { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int ToLocation { get; set; }
        public string SenderPhone { get; set; }
    }
}