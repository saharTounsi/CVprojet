using APIcv.Dto;
using APIcv.Models;

namespace APIcv.interfaces
{
    public interface ICVrep
    {
        ICollection<CV> GetCVs();
        CV GetCV(int ID);
        CV GetCVTrimToUpper(CVDto CVCreate);
        CV GetCV(string ETAT);
        bool CVEXISTS(int IDCV);
        bool CreateCV(int IDEMPLOYEE, CV cV);
        bool updateCV(int IDCVMIDIF, CV cV);
        bool deleteCV(CV cV);
        bool Save();
    }
}
