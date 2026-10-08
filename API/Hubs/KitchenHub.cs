using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    [Authorize(Roles = "Admin,Bep,PhucVu")]
    public class KitchenHub : Hub
    {
    }
}