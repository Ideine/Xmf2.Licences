using System.IO;
using System.Threading.Tasks;

namespace Xmf2.Licences;

public class LicenceReaderService : ILicenceReaderService
{
    public async Task<string> GetContent(string licencePathFile)
    {
        await using FileStream stream = File.OpenRead(licencePathFile);
        using StreamReader reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}