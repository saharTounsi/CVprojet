using APIcv.DATA;
using APIcv.interfaces;
using APIcv.Models;
using AutoMapper;

namespace APIcv.Repository
{
    public class CVEXPORTERep : ICVEXPORTERep
    {
        private readonly Datacontext _context;
        private readonly IMapper _mapper;

        public CVEXPORTERep(Datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateCVEXPORTE(CVEXPORTE cVEXPORTE)
        {
            _context.Add(cVEXPORTE);
            return Save();
        }

        public bool CreateCVEXPORTE(int IDUser, CVEXPORTE cVEXPORTE)
        {
            var CVMODIFEntity = _context.EMPLOYEEs.Where(a => a.ID == IDUser).FirstOrDefault();
            var CVMODIF = new CVMODIF()
            {
                EMPLOYEE = CVMODIFEntity,

            };
            _context.Add(CVMODIF);
            _context.Add(cVEXPORTE);
            return Save();
        }

        public bool CVEXPORTEEXISTS(int ID)
        {
            return _context.CVEXPORTEs.Any(c => c.ID == ID);
        }

        public bool deleteCVEXPORTE(CVEXPORTE cVEXPORTE)
        {
            _context.Remove(cVEXPORTE);
            return Save();
        }

        public CVEXPORTE GetCVEXPORTE(int ID)
        {
            return _context.CVEXPORTEs.Where(c => c.ID == ID).FirstOrDefault();
        }
        public ICollection<CVEXPORTE> GetCVEXPORTEs()
        {
            throw new NotImplementedException();
           // return _context.CVEXPORTEs.ToList();
        }

        public ICollection<CVEXPORTE> GetCVEXPORTEsOfACV(int IDCV)
        {
            return _context.CVEXPORTEs.Where(c => c.CV.ID == IDCV).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool updateCVEXPORTE(CVEXPORTE cVEXPORTE)
        {
            _context.Update(cVEXPORTE);
            return Save();
        }
    }
}
