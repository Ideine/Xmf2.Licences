using System;
using Android.Runtime;
using Android.Webkit;

namespace Xmf2.Licences;

internal class ProgressWebChromeClient : WebChromeClient
{
	private int _currentProgress;

	private IBusyHelper _busyHelper;

	public ProgressWebChromeClient() { }

	protected ProgressWebChromeClient(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

	public void InitializeBusyHelper(IBusyHelper busyHelper)
	{
		_busyHelper = busyHelper;
	}

	public override void OnProgressChanged(WebView view, int newProgress)
	{
		try
		{
			if (newProgress > _currentProgress && !_busyHelper.IsBusy)
			{
				_busyHelper.IsBusy = true;
			}
			else if (newProgress == 100)
			{
				_busyHelper.IsBusy = false;
			}
		}
		finally
		{
			_currentProgress = newProgress;
			base.OnProgressChanged(view, newProgress);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			_busyHelper = null;
		}
		base.Dispose(disposing);
	}
}