namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderTables = new HashSet<OrderTable>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string InvoiceNo { get; set; }

        public byte? OrderType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DatedUpdated { get; set; }

        public int? EmployeeID { get; set; }

        public byte? PaymentType { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        public decimal? Discount { get; set; }

        public byte? DiscountType { get; set; }

        public decimal? Extra { get; set; }

        public String ExtraContent { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceBefore { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceAfter { get; set; }

        [Column(TypeName = "money")]
        public decimal? MoneyReceive { get; set; }

        [Column(TypeName = "money")]
        public decimal? MoneyCharge { get; set; }

        public string CustomerName { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTable> OrderTables { get; set; }
    }
}
