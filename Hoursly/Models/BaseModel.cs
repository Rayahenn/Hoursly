using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoursly.Common;
using Hoursly.Common.Decorators;

namespace Hoursly.Models
{
    public class BaseModel : IUniqueIdentifier 
    {
        public BaseModel(Guid publicId)
        {
            PublicId = publicId;
        }

        public Guid PublicId { get; set; }
    }

}
