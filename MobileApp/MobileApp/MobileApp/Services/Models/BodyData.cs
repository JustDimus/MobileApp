using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Models
{
    public class BodyData
    {
        public string Id { get; set; }
        public string AthletId { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? ChestGirth { get; set; }
        public double? WaistCircumference { get; set; }
        public double? AbdominalGirth { get; set; }
        public double? ButtocksGirth { get; set; }
        public double? ThighGirth { get; set; }
        public DateTime? Date { get; set; }
    }
}
