using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Webkit;

namespace Xmf2.Licences
{
	public class LicenceWebView : WebView
	{
		private ProgressWebChromeClient _chromeClient;

		public LicenceWebView(Context context) : base(context) { }

		public LicenceWebView(Context context, IAttributeSet attrs) : base(context, attrs) { }

		public LicenceWebView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

		public LicenceWebView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

		protected LicenceWebView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

		public void InitializeDefaultProgressClient(IBusyHelper busyHelper)
		{
			_chromeClient = new ProgressWebChromeClient();
			_chromeClient.InitializeBusyHelper(busyHelper);
			SetWebChromeClient(_chromeClient);
		}

		public void LoadLicences(string licenceHtml) => LoadData(licenceHtml, "text/html", "utf-8");

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					SetWebChromeClient(null);
					_chromeClient?.Dispose();
					_chromeClient = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
