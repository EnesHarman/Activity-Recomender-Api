using MernisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters
{
    public class MernisPersonCheckAdapter : IPersonCheckSystem
    {
        public async Task<bool> checkPersonReal(string IDNum, string name, string lastname, int birthDate)
        {
            KPSPublicSoapClient client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var result = await client.TCKimlikNoDogrulaAsync(long.Parse(IDNum), name.ToUpper(), lastname.ToUpper(), birthDate);
            return result.Body.TCKimlikNoDogrulaResult; 
        }
    }
}
