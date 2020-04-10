using LinqToDB.Mapping;

namespace DAL.LinqToDb.Entities
{
    [Table("[dbo].[Shippers]")]
    public class Shipper
    {
        [Column("ShipperID")]
        [Identity]
        [PrimaryKey]
        public int ShipperId { get; set; }

        [Column("CompanyName")]
        [NotNull]
        public string CompanyName { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }
    }
}
