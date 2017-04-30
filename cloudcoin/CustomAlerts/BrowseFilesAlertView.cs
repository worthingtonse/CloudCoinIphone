using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace cloudcoin
{
	[Register("BrowseFileUIView")]
	public class BrowseFilesAlertView : UIViewController
	{
		public BrowseFilesAlertView()
		{

		}
		public BrowseFilesAlertView(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

            var btn = UIButton.FromType(UIButtonType.System);
			btn.Frame = new CGRect(0, 0, 280, 44);
			btn.SetTitle ("Browse File", UIControlState.Normal);

            var user = new UIViewController();
			user.View.BackgroundColor = UIColor.Gray;

            btn.TouchUpInside += (sender, e) => {
				//this.NavigationController.PushViewController (user, true);
			};
			View.AddSubview (btn);
		}
	}
}
