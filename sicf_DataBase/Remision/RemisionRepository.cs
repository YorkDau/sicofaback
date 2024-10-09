using sicf_DataBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Remision
{
    public class RemisionRepository: IRemisionRepository
    {

        private readonly SICOFAContext _context;

        public RemisionRepository(SICOFAContext context)
        {

            _context = context;

        }



    }
}
