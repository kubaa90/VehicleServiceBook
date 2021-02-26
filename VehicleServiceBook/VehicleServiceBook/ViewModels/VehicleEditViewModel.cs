﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceBook.Models;
using VehicleServiceBook.ViewModels;

namespace VehicleServiceBook.ViewModels
{
    public class VehicleEditViewModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
    }
}
