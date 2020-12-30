using Microsoft.Extensions.Options;
using Obfuscation;
using PrizesService.Abstraction;
using PrizesService.Models.Common;
using System;

namespace PrizesService.Repository
{
    public class ObfuscationRepository : IObfuscationRepository
    {
        private readonly AppSettings _appSettings;
        public ObfuscationRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public int IdDecryption(string id)
        {
            return ObfuscationClass.DecodeId(Convert.ToInt32(id), _appSettings.PrimeInverse);
        }
        public string IdEncryption(int id)
        {
            return ObfuscationClass.EncodeId(id, _appSettings.Prime).ToString();
        }
    }
}
