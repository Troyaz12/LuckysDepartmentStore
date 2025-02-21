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

                var cartCount = await _shoppingCartService.GetCartCount(cartID);

                _logger.LogInformation("Test Connection called");
                await Clients.All.SendAsync("ReceiveCartCountUpdate", cartCount.Data);
            }
            catch (Exception ex)
            {

            }
            

        }
        public async Task TestConnection() 
        {
            _logger.LogInformation("TestConnection called"); 
            await Clients.All.SendAsync("ReceiveCartCountUpdate", 999);
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("A client connected: " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }

    }
}
