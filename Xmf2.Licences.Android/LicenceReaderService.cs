using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;

namespace Xmf2.Licences;

public class LicenceReaderService : ILicenceReaderService
{
    private readonly AssetManager _assetManager;

    public LicenceReaderService(Context context)
    {
        _assetManager = context.Assets;
    }

    public async Task<string> GetContent(string licencePathFile)
    {
        using StreamReader sr = new StreamReader(_assetManager.Open(licencePathFile));
        return await sr.ReadToEndAsync();
    }
}