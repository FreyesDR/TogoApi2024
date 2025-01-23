namespace XDev_Model.Entities
{
    public class PriceCondition : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // B - Precio base del producto, I - Impuesto, R - Recargo - O - Otros Impuestos, D - Descuento
        public string Source { get; set; } // A - Precio base del producto, B - Resultado condición anterior, C - Condición especifica
        public Guid SourceConditionId { get; set; }
        public decimal Value { get; set; } // Valor de la condición
        public string ValueType { get; set; } // Tipo Valor de la condición, V - Valor, P - Porcentaje
        public bool Edit { get; set; } // Editable        
        public HashSet<PriceSchemeCondition> PriceSchemeCondition { get; set; }
    }
}
