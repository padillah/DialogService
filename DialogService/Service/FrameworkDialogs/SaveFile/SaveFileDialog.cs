using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace DialogServiceLibrary.Service.FrameworkDialogs.SaveFile
{
	/// <summary>
	/// Class wrapping System.Windows.Forms.SaveFileDialog, making it accept a ISaveFileDialog.
	/// </summary>
	public class SaveFileDialog : IDisposable
	{
		private readonly ISaveFileDialog _saveFileDialog;
		private System.Windows.Forms.SaveFileDialog _concreteSaveFileDialog;
		

		/// <summary>
		/// Initializes a new instance of the <see cref="SaveFile.SaveFileDialog"/> class.
		/// </summary>
		/// <param name="argSaveFileDialog">The interface of a Save file dialog.</param>
		public SaveFileDialog(ISaveFileDialog argSaveFileDialog)
		{
			Contract.Requires(argSaveFileDialog != null);

			_saveFileDialog = argSaveFileDialog;

			// Create concrete SaveFileDialog
			_concreteSaveFileDialog = new System.Windows.Forms.SaveFileDialog
			{
				AddExtension = argSaveFileDialog.AddExtension,
				CheckFileExists = argSaveFileDialog.CheckFileExists,
				CheckPathExists = argSaveFileDialog.CheckPathExists,
				DefaultExt = argSaveFileDialog.DefaultExt,
				FileName = argSaveFileDialog.FileName,
				Filter = argSaveFileDialog.Filter,
				InitialDirectory = argSaveFileDialog.InitialDirectory,
				Title = argSaveFileDialog.Title
			};
		}


		/// <summary>
		/// Runs a common dialog box with the specified owner.
		/// </summary>
		/// <param name="argOwner">
		/// Any object that implements System.Windows.Forms.IWin32Window that represents the top-level
		/// window that will own the modal dialog box.
		/// </param>
		/// <returns>
		/// System.Windows.Forms.DialogResult.OK if the user clicks OK in the dialog box; otherwise,
		/// System.Windows.Forms.DialogResult.Cancel.
		/// </returns>
		public DialogResult ShowDialog(IWin32Window argOwner)
		{
			Contract.Requires(argOwner != null);

			DialogResult result = _concreteSaveFileDialog.ShowDialog(argOwner);

			// Update ViewModel
			_saveFileDialog.FileName = _concreteSaveFileDialog.FileName;
			_saveFileDialog.FileNames = _concreteSaveFileDialog.FileNames;

			return result;
		}


		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting
		/// unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}


		~SaveFileDialog()
		{
			Dispose(false);
		}


		protected virtual void Dispose(bool argDisposing)
		{
			if (argDisposing)
			{
				if (_concreteSaveFileDialog != null)
				{
					_concreteSaveFileDialog.Dispose();
					_concreteSaveFileDialog = null;
				}
			}
		}

		#endregion
	}
}
