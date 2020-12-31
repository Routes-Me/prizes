using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrizesService.Abstraction
{
   public  interface IObfuscationRepository
    {
        int IdDecryption(string id);
        string IdEncryption(int id);
    }
}
