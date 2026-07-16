namespace Base.Domain.Entities.Common
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? LastModifyDate { get; set; }
        long? CreatedBy { get; set; }
        long? LastModifyBy { get; set; }
    }
}