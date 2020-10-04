using System.IO;
using System.Threading.Tasks;

namespace Xmf2.Licences
{
    public class LicenceReaderService : ILicenceReaderService
    {
        public async Task<string> GetContent(string licencePathFile)
        {
            await using var stream = File.OpenRead(licencePathFile);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
