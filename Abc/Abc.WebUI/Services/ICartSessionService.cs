using Abc.Entities;

namespace Abc.WebUI.Services
{
    public interface ICartSessionService
    {
        //session oku(get)/ yaz(set)
        Cart GetCart();
        void SetCart(Cart cart);
    }
}
