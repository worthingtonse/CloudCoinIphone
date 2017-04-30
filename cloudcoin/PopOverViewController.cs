using System;
using Foundation;
using UIKit;

namespace cloudcoin
{
	[Register("BrowseFileUIView")]
	public class PopOverViewController : UIViewController
	{
		public PopOverViewController()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Console.WriteLine("Called!");
		}
	}
}
