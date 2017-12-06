using System;

namespace Codetecuico.Byns.Data.Entity
{
    public abstract class BaseModel
    {
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}