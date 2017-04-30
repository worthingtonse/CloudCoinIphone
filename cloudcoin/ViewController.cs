using System;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;

namespace cloudcoin
{
	public partial class ViewController : UIViewController
	{
		private bool importHasFile = true;
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Task.Factory.StartNew(() => CheckRaidaAPI()).ContinueWith((task) =>
			{
				InvokeOnMainThread(() =>
				{
					if (task.Result == true)
						HandleAllUIControlView();
				});
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public bool CheckRaidaAPI()
		{
			try
			{
				InvokeOnMainThread(() =>
				{
					var UIRaidaAlert = UIAlertController.Create("RAIDA Oops!",
																		 "Sorry, the RAiDA is unavailable",
																		 UIAlertControllerStyle.Alert);
					UIRaidaAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null));
					UIRaidaAlert.AddAction(UIAlertAction.Create("Go to Settings", UIAlertActionStyle.Default, null));
					PresentViewController(UIRaidaAlert, true, null);
				});
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Oops: Error - " + ex.Message.ToString());
				return false;
			}
		}

		public void HandleAllUIControlView()
		{
			BtnImport.TouchUpInside += (sender, e) =>
		   {
			   if (importHasFile)
				   ImportHasFilesButtonHandler(13);
			   else
				   ImportNoFilesButtonHandler();
			   importHasFile = !importHasFile;
		   };
		}

		public void ImportHasFilesButtonHandler(int files)
		{
			var message = string.Format("You have {0} files in your import directory.", files);
			var UIImportAlert = UIAlertController.Create("Import Coins",
														 message,
														 UIAlertControllerStyle.Alert);
			UIImportAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null));
			UIImportAlert.AddAction(UIAlertAction.Create("Import", UIAlertActionStyle.Default, (obj) => ImportFilesProgressButtonHandler(3, 13)));

			PresentViewController(UIImportAlert, true, null);
		}
		public void ImportFilesProgressButtonHandler(int files, int total)
		{
			var message = string.Format("Authenticating {0} of {1} CloudCoin Files.", files, total);
			var UIImportAlert = UIAlertController.Create("Import Coins",
														 message,
														 UIAlertControllerStyle.Alert);
			var importProgressView = new UIProgressView();
			importProgressView.Progress = (float)files;
			importProgressView.Style = UIProgressViewStyle.Bar;
			importProgressView.Frame = new CGRect(0, View.Frame.Height - 10, View.Frame.Width, 20);
			importProgressView.Layer.CornerRadius = 10;
			UIImportAlert.Add(importProgressView);
			PresentViewController(UIImportAlert, true, null);
			Task.Factory.StartNew(() => Thread.Sleep(5000)).ContinueWith((arg) =>
			{
				InvokeOnMainThread(() =>
				{
					importProgressView.Progress = 10;
					UIImportAlert.DismissViewController(true, null);
				});
			});
		}
		public void ImportNoFilesButtonHandler()
		{
			var UIImportAlert = UIAlertController.Create("Import Coins",
																 "No files in the import folder",
														 UIAlertControllerStyle.Alert);
			UIImportAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null));
			UIImportAlert.AddAction(UIAlertAction.Create("Import", UIAlertActionStyle.Default, (obj) => ImportCoinFromFileButtonHandler()));

			PresentViewController(UIImportAlert, true, null);
		}
		public void ImportCoinFromFileButtonHandler()
		{
			var UIImportFileAlert = UIAlertController.Create("Import Coins",
															 "No files in the import folder",
															 UIAlertControllerStyle.Alert);
			var btn = UIButton.FromType(UIButtonType.System);
			btn.Frame = new CGRect(0, 100, 260, 44);
			btn.Layer.CornerRadius = 5;
			btn.SetTitle("Choose another folder", UIControlState.Normal);

			btn.BackgroundColor = UIColor.Gray;
			btn.TintColor = UIColor.White;

			btn.TouchUpInside += (bsender, bevt) =>
			{
				//this.NavigationController.PushViewController (user, true);
			};
			UIImportFileAlert.Add(btn);
			UIImportFileAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null));
			UIImportFileAlert.AddAction(UIAlertAction.Create("Import", UIAlertActionStyle.Default, null));
			PresentViewController(UIImportFileAlert, true, null);
		}
	}
}
