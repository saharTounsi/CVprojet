using APIcv.Models;

namespace APIcv.interfaces
{
    public interface IVERSION
    {
        ICollection<VERSION> GetVERSIONS();
        VERSION GetVERSION(int ID);
        bool VERSIONEXISTS(int ID);
        bool CreateVERSION(VERSION vERSION);
        bool Save();
    }
}
