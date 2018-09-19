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
            Tables = new HashSet<Table>();
        }

        public int ID { get; set; }

        public string InvoiceNo { get; set; }

        public bool? OrderType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DatedUpdated { get; set; }

        public int? EmployeeID { get; set; }

        public bool? PaymentType { get; set; }

        [Column(TypeName = "money")]
        public decimal? VAT { get; set; }

        public int? Discount { get; set; }

        public bool? DiscountType { get; set; }

        public int? Extra { get; set; }

        public bool? ExtraType { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceBefore { get; set; }

        [Column(TypeName = "money")]
        public decimal? PriceAfter { get; set; }

        [Column(TypeName = "money")]
        public decimal? MoneyReceive { get; set; }

        [Column(TypeName = "money")]
        public decimal? MoneyCharge { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Table> Tables { get; set; }
    }
}
