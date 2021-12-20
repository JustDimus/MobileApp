using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Models
{
    public class NutritionData
    {
        public string Id { get; set; }
        public string AthletId { get; set; }
        public int? Kcal { get; set; }
        public int? Proteins { get; set; }
        public int? Fats { get; set; }
        public int? Carbohydrates { get; set; }
        public double? AmountOfWater { get; set; }
        public DateTime? Date { get; set; }
    }
}
