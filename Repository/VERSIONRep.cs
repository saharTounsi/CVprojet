using APIcv.DATA;
using APIcv.interfaces;
using APIcv.Models;
using Microsoft.EntityFrameworkCore;

namespace APIcv.Repository
{
    public class VERSIONRep : IVERSION
    {
        private Datacontext _context;
        public VERSIONRep(Datacontext context)
        {
            _context = context;
        }

        public Datacontext Context { get; }

        public bool CreateVERSION(VERSION vERSION)
        {
            _context.Add(vERSION);

            return Save();
        }

        public VERSION GetVERSION(int ID)
        {
            throw new NotImplementedException();
        }

        public ICollection<VERSION> GetVERSIONS()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool VERSIONEXISTS(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
