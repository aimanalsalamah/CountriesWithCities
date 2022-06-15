using System.Collections.Generic;

namespace BLL.DB.Master
{
    public class Countries : Base.TableName
    {
        public string Code { get; set; }
        public virtual ICollection<BLL.DB.Master.Cities> Cities { get; set; }
    }
}
