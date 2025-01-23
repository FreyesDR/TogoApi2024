using Microsoft.Data.SqlClient;
using XDev_UnitWork.Custom;

namespace XDev_UnitWork.DTO.Material
{
    public class MaterialFeatureDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public short NumType { get; set; }
        public Guid RangeId { get; set; }

        //public static ValueTask<MaterialFeatureDTO> BindAsync(HttpContext context)
        //{
        //    var orderstring = context.GetValueOrDefault(nameof(SortOrder), string.Empty);

        //    var result = new MaterialFeatureDTO
        //    {
        //        Id = context.GetValueOrDefault(nameof(Id), Guid.Empty),
        //        NumType = context.GetValueOrDefault(nameof(NumType), Convert.ToInt16(0)),
        //        RangeId = context.GetValueOrDefault(nameof(RangeId), Guid.Empty),
        //        ConcurrencyStamp = context.GetValueOrDefault(nameof(ConcurrencyStamp), Guid.Empty.ToString()),
        //    };

        //    return ValueTask.FromResult(result);
        //}
    }
}
