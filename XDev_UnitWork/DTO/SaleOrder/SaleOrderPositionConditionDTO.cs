using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderPositionConditionDTO
    {
        public Guid SaleOrderPositionId { get; set; }        
        public Guid PriceConditionId { get; set; }
        public int Position { get; set; }
        public string Code { get; set; }
        public string AltCode {  get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // B - Precio base del producto, I - Impuesto, O - Otros Impuestos, R - Recargo, D - Descuento
        public string Source { get; set; } // A - Precio base del producto, B - Resultado condición anterior, C - Condición especifica
        public Guid SourceConditionId { get; set; }
        public decimal Value { get; set; } // Valor de la condición
        public string ValueType { get; set; } // Tipo Valor de la condición, V - Valor, P - Porcentaje
        public bool Edit { get; set; } // Editable        
        public decimal BaseCondition { get; set; }
        public decimal ValueCondition { get; set; }
    }
}
