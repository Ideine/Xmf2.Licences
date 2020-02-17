using System.Threading.Tasks;
using Xmf2.Licences.Models;

namespace Xmf2.Licences
{
	public static class LicenceLoader
	{
		public static Task<string> GetLicensesText(Notices notices, bool showFullLicenseText = true, bool clickableUrl = false)
		{
			return NoticesHtmlBuilder
					.Create()
					.SetShowFullLicenseText(showFullLicenseText)
					.SetStyle(LicenceStyle.LicenceDefaultStyle)
					.SetNotices(notices)
					.SetClickableUrl(clickableUrl)
					.Build();
		}

		public static Task<string> GetLicensesText(Notices notices, string htmlStyle, bool showFullLicenseText = true, bool clickableUrl = false)
		{
			return NoticesHtmlBuilder
					.Create()
					.SetShowFullLicenseText(showFullLicenseText)
					.SetStyle(htmlStyle)
					.SetNotices(notices)
					.SetClickableUrl(clickableUrl)
					.Build();
		}
	}
}
