namespace LuckysDepartmentStore.Utilities
{
    public partial class ShoppingCart
    {
        public const string CartSessionKey = "CartId";
        public string ShoppingCartId { get; set; }
        public ShoppingCart()
        {
        
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey,context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return context.Session.GetString(CartSessionKey);
        }
    }
}
