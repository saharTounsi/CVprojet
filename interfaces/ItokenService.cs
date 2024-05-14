using APIcv.Models;

namespace APIcv.interfaces
{
    public interface ItokenService
    {
        string CreateToken(User user);
    }
}
