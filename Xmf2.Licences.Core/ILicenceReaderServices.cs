using System.Threading.Tasks;

namespace Xmf2.Licences;

public interface ILicenceReaderService
{
    Task<string> GetContent(string licencePathFile);
}