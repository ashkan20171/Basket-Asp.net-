using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Basket.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ImageName { get; set; }

        #region Relation

        public List<OrderDetails> OrderDetails { get; set; }


        #endregion
    }
}
