﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVVMClass1.ViewModel
{
    public class ClRecordatorioEVM
    {
        public int IdRecordatorio { get; set; }
        public int IdUsuario { get; set; }
        public string Recordatorio { get; set; }
        public string Fecha { get; set; }
        public int Llamado { get; set; }
    }
}