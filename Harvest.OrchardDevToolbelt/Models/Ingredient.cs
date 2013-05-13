namespace Harvest.OrchardDevToolbelt.Models {
    public class Ingredient {
        public virtual int Id { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual string Name { get; set; }
        public virtual float Quantity { get; set; }
        public virtual QuantityUnit Unit { get; set; }
    }
}