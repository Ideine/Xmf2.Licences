using System.Threading.Tasks;

namespace Xmf2.Licences.Models
{
	public class Licence
	{
		private readonly ILicenceReaderService _licenceReaderService;

		public virtual string LicencePathFile { get; set; }

		private string _cachedSummaryText;

		private string _cachedFullText;

		public Licence(ILicenceReaderService readerService)
		{
			_licenceReaderService = readerService;
		}

		public async Task<string> GetSummaryText(string licencePathFile)
		{
			return _cachedSummaryText ??= await ReadSummaryTextFromPath(licencePathFile);
		}

		public async Task<string> GetFullText(string licencePathFile)
		{
			return _cachedFullText ??= await ReadFullTextFromPath(licencePathFile);
		}

		protected Task<string> GetContent(string licencePathFile) => _licenceReaderService.GetContent(licencePathFile);

		public Task<string> ReadFullTextFromPath(string licencePathFile) => GetContent(licencePathFile);

		public Task<string> ReadSummaryTextFromPath(string licencePathFile) => GetContent(licencePathFile);
	}
}