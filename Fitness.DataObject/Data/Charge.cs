﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataObject.Data
{
    public class Charge
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public decimal Summ { get; set; }
        public DateTime Date { get; set; }
        public int AdministratorId { get; set; }
    }
}