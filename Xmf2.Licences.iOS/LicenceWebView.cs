using System;
using CoreGraphics;
using Foundation;
using WebKit;

namespace Xmf2.Licences
{
	public class LicenceWebView : WKWebView, IWKNavigationDelegate
	{
		private IBusyHelper _busyHelper;

		public LicenceWebView(NSCoder coder) : base(coder) { }

		public LicenceWebView(CGRect frame, WKWebViewConfiguration configuration) : base(frame, configuration) { }

		public LicenceWebView() : base(CGRect.Empty, new WKWebViewConfiguration()) { }

		protected LicenceWebView(NSObjectFlag t) : base(t) { }

		protected internal LicenceWebView(IntPtr handle) : base(handle) { }

		public void InitializeDefaultProgressClient(IBusyHelper busyHelper)
		{
			_busyHelper = busyHelper;
			NavigationDelegate = this;
		}

		public void LoadLicences(string licenceHtml) => LoadHtmlString(licenceHtml, null);

		[Export("webView:didStartProvisionalNavigation:")]
		public void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
		{
			if (_busyHelper != null && !_busyHelper.IsBusy)
			{
				_busyHelper.IsBusy = true;
			}
		}


		[Export("webView:didFinishNavigation:")]
		public void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
		{
			if (_busyHelper != null)
			{
				_busyHelper.IsBusy = false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					NavigationDelegate = null;
					_busyHelper = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
