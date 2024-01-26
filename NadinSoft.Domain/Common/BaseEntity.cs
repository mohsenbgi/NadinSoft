using System.ComponentModel.DataAnnotations;

namespace NadinSoft.Domain.Common
{
    public abstract class BaseEntity<T> where T : IEquatable<T>
    {
        public BaseEntity()
        {
            CreatedDateOnUtc = DateTime.UtcNow;
            UpdatedDateOnUtc = DateTime.UtcNow;
        }

        [Key]
        public T Id { get; set; }

        public string CreatedById { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDateOnUtc { get; set; }

        public DateTime UpdatedDateOnUtc { get; set; }
    }
}
