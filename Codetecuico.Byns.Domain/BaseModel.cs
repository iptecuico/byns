namespace Codetecuico.Byns.Domain
{
    public abstract class BaseModel
    {
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}