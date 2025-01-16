namespace Xmf2.Licences.Models;

public class Notice
{
    public string Name { get; }
    public string Url { get; }
    public string Copyright { get; }
    public Licence License { get; }

    public Notice(string name, string url, string filePath, ILicenceReaderService readerService, string copyright = null)
    {
        Name = name;
        Url = url;
        Copyright = copyright ?? string.Empty;
        License = new Licence(readerService)
        {
            LicencePathFile = filePath
        };
    }

    public Notice(string name, string url, Licence license, string copyright = null)
    {
        Name = name;
        Url = url;
        Copyright = copyright ?? string.Empty;
        License = license;
    }
}