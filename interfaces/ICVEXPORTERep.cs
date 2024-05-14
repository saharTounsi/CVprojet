using APIcv.Models;

namespace APIcv.interfaces
{
    public interface ICVEXPORTERep
    {
        ICollection<CVEXPORTE> GetCVEXPORTEs();
        ICollection<CVEXPORTE> GetCVEXPORTEsOfACV(int IDCV);
        CVEXPORTE GetCVEXPORTE(int ID);
        bool CVEXPORTEEXISTS(int ID);
        bool CreateCVEXPORTE(int IDUser,CVEXPORTE cVEXPORTE);
        bool updateCVEXPORTE(CVEXPORTE cVEXPORTE);
        bool deleteCVEXPORTE(CVEXPORTE cVEXPORTE);
        bool Save();
    }
}