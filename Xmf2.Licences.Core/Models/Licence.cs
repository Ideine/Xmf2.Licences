using System.Threading.Tasks;

namespace Xmf2.Licences.Models;

public class Licence(ILicenceReaderService readerService)
{
    public virtual string LicencePathFile { get; set; }

    private string _cachedSummaryText;

    private string _cachedFullText;

    public async Task<string> GetSummaryText(string licencePathFile)
    {
        return _cachedSummaryText ??= await ReadSummaryTextFromPath(licencePathFile);
    }

    public async Task<string> GetFullText(string licencePathFile)
    {
        return _cachedFullText ??= await ReadFullTextFromPath(licencePathFile);
    }

    protected Task<string> GetContent(string licencePathFile) => readerService.GetContent(licencePathFile);

    public Task<string> ReadFullTextFromPath(string licencePathFile) => GetContent(licencePathFile);

    public Task<string> ReadSummaryTextFromPath(string licencePathFile) => GetContent(licencePathFile);
}