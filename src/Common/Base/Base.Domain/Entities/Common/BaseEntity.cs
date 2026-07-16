using Base.Domain.Interfaces;
namespace Base.Domain.Entities.Common
{
    public class BaseEntity : IBaseEntity,ISoftDeletable
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? LastModifyBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}