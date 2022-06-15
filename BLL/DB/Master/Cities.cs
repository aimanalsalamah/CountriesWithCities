using System;

namespace BLL.DB.Master
{
    public class Cities : Base.TableName
    {
        public Guid CountryId { get; set; }
        public virtual Countries Country { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
