using LuckysDepartmentStore.Models;
using LuckysDepartmentStore.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace LuckysDepartmentStore.Utilities
{
    public class CartHub : Hub
    {
        private readonly ILogger<CartHub> _logger;
        private readonly IShoppingCartService _shoppingCartService;

        public CartHub(ILogger<CartHub> logger, IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }
        public async Task SendCartCountUpdate()
        {
            try
            {
                var cartID = _shoppingCartService.GetCart();

                if (!cartID.IsSuccess)
                {
                    _logger.LogWarning("Failed to get cart id for cart update");
                }

                var cartCount = await _shoppingCartService.GetCartCount(cartID.Data);

                await Clients.All.SendAsync("ReceiveCartCountUpdate", cartCount.Data);
            }
            catch (Exception ex)
            {

            }
            

        }
        public async Task TestConnection() 
        {
            await Clients.All.SendAsync("ReceiveCartCountUpdate", 999);
        }

        public override async Task OnConnectedAsync()
        {         
            await base.OnConnectedAsync();
        }

    }
}
