using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xmf2.Licences.Models;

namespace Xmf2.Licences
{
	public class NoticesHtmlBuilder
	{
		private readonly Dictionary<Licence, string> _licenseTextCache = new Dictionary<Licence, string>();
		private Notices _notices;
		private Notice _notice;
		private string _style;
		private bool _showFullLicenseText;

		private bool _isUrlClickable;

		public static NoticesHtmlBuilder Create() => new NoticesHtmlBuilder();

		public static NoticesHtmlBuilder Create(string style) => new NoticesHtmlBuilder(style);

		private NoticesHtmlBuilder()
		{
			_showFullLicenseText = false;
		}

		private NoticesHtmlBuilder(string style) : this()
		{
			_style = style;
			_isUrlClickable = true;
		}

		public NoticesHtmlBuilder SetNotices(Notices notices)
		{
			_notices = notices;
			_notice = null;
			return this;
		}

		public NoticesHtmlBuilder SetNotice(Notice notice)
		{
			_notice = notice;
			_notices = null;
			return this;
		}

		public NoticesHtmlBuilder SetStyle(string style)
		{
			_style = style;
			return this;
		}

		public NoticesHtmlBuilder SetShowFullLicenseText(bool showFullLicenseText)
		{
			_showFullLicenseText = showFullLicenseText;
			return this;
		}

		public NoticesHtmlBuilder SetClickableUrl(bool isUrlClickable)
		{
			_isUrlClickable = isUrlClickable;
			return this;
		}

		public async Task<string> Build()
		{
			StringBuilder noticesHtmlBuilder = new StringBuilder(500);
			AppendNoticesContainerStart(noticesHtmlBuilder);
			if (_notice != null)
			{
				await AppendNoticeBlock(noticesHtmlBuilder, _notice);
			}
			else if (_notices != null)
			{

				foreach (var notice in _notices.AllNotices)
				{
					await AppendNoticeBlock(noticesHtmlBuilder, notice);
				}
			}
			else
			{
				throw new Exception("no notice(s) set");
			}
			AppendNoticesContainerEnd(noticesHtmlBuilder);
			return noticesHtmlBuilder.ToString();
		}

		private void AppendNoticesContainerStart(StringBuilder noticesHtmlBuilder)
		{
			noticesHtmlBuilder.Append("<!DOCTYPE html><html><head>")
				.Append("<style type=\"text/css\">").Append(_style).Append("</style>")
				.Append("</head><body>");
		}

		private async Task AppendNoticeBlock(StringBuilder noticesHtmlBuilder, Notice notice)
		{
			noticesHtmlBuilder.Append("<ul><li>").Append(notice.Name);
			string currentNoticeUrl = notice.Url;
			if (!string.IsNullOrEmpty(currentNoticeUrl))
			{
				if (_isUrlClickable)
				{
					noticesHtmlBuilder.Append(" (<a href=\"")
						.Append(currentNoticeUrl)
						.Append("\" target=\"_blank\">")
						.Append(currentNoticeUrl)
						.Append("</a>)");
				}
				else
				{
					noticesHtmlBuilder.Append(" (<span>").Append(currentNoticeUrl).Append("</span>)");
				}
			}
			noticesHtmlBuilder.Append("</li></ul>");
			noticesHtmlBuilder.Append("<pre>");
			string copyright = notice.Copyright;
			if (!string.IsNullOrEmpty(copyright))
			{
				noticesHtmlBuilder.Append(copyright).Append("<br/><br/>");
			}
			noticesHtmlBuilder.Append(await GetLicenseText(notice.License)).Append("</pre>");
		}

		private void AppendNoticesContainerEnd(StringBuilder noticesHtmlBuilder)
		{
			noticesHtmlBuilder.Append("</body></html>");
		}

		private async Task<string> GetLicenseText(Licence license)
		{
			if (license != null)
			{
				if (!_licenseTextCache.ContainsKey(license))
				{
					_licenseTextCache[license] = _showFullLicenseText ? await license.GetFullText(license.LicencePathFile) : await license.GetSummaryText(license.LicencePathFile);
				}
				return _licenseTextCache[license];
			}
			return string.Empty;
		}
	}
}
