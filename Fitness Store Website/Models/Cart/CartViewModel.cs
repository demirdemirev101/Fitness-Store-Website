using System.Collections.Generic;

namespace Fitness_Store_Website.Models.Cart
{
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Total => Items.Sum(i => i.Price * i.Quantity);
    }
}