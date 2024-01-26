using NadinSoft.Domain.Common;

namespace NadinSoft.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public Product(string createdById, string name, DateTime produceDate, string manufacturePhone, string manufactureEmail, bool isAvailable)
        {
            CreatedById = createdById;
            Name = name;
            ProduceDate = produceDate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = isAvailable;
        }

        protected Product() { }

        public string Name { get; set; }

        public DateTime ProduceDate { get; set; }

        public string ManufacturePhone { get; set; }

        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }
    }
}
