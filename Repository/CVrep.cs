using APIcv.DATA;
using APIcv.Dto;
using APIcv.interfaces;
using APIcv.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace APIcv.Repository
{
    public class CVrep : ICVrep
    {
        private readonly Datacontext _context;

        public CVrep(Datacontext context)
        {
            _context = context;
        }

        public bool CreateCV(int IDEMPLOYEE, CV cV)
        {
            var CVMODIFEntity = _context.EMPLOYEEs.Where(a => a.ID == IDEMPLOYEE).FirstOrDefault();
            var CVMODIF = new CVMODIF()
            {
                EMPLOYEE = CVMODIFEntity,

            };
            _context.Add(CVMODIF);
            _context.Add(cV);
            return Save();
        }

        public bool CVEXISTS(int IDCV)
        {
            return _context.CVs.Any(p => p.ID == IDCV);
        }

        public bool deleteCV(CV cV)
        {
            _context.Remove(cV);
            return Save();
        }

        public CV GetCV(int ID)
        {
            return _context.CVs.Where(p => p.ID == ID).FirstOrDefault();
        }

        public CV GetCV(string ETAT)
        {
            return _context.CVs.Where(p => p.ETAT == ETAT).FirstOrDefault();
        }


        public ICollection<CV> GetCVs()
        {
            try
            {
                return _context.CVs.OrderBy(p => p.ID).ToList();
            }
            catch(Exception exception){
                return new List<CV>();
            }

        }

        public CV GetCVTrimToUpper(CVDto CVCreate)
        {
            return GetCVs().Where(c => c.ETAT.Trim().ToUpper() == CVCreate.ETAT.TrimEnd().ToUpper()) .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateCV(int IDCVMIDIF, CV cV)
        {
            _context.Update(cV);
            return Save();
        }
    }
}
