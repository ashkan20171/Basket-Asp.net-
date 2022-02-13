using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basket.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int Sum { get; set; }
        [Required]
        public bool IsFinally { get; set; }

        #region Relation

        public List<OrderDetails> OrderDetails { get; set; }


        #endregion
    }
}
