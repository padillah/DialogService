using DialogServiceLibrary.Service.FrameworkDialogs.FolderBrowse;
using DialogServiceLibrary.Service.FrameworkDialogs.OpenFile;
using DialogServiceLibrary.Service.FrameworkDialogs.SaveFile;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;

namespace DialogServiceLibrary.Service
{
	/// <summary>
	/// Interface responsible for abstracting ViewModels from Views.
	/// </summary>
	[ContractClass(typeof(IDialogServiceContract))]
	public interface IDialogService
	{
		/// <summary>
		/// Gets the registered views.
		/// </summary>
		ReadOnlyCollection<FrameworkElement> Views { get; }


		/// <summary>
		/// Registers a View.
		/// </summary>
		/// <param name="argView">The registered View.</param>
		void Register(FrameworkElement argView);


		/// <summary>
		/// Unregisters a View.
		/// </summary>
		/// <param name="argView">The unregistered View.</param>
		void Unregister(FrameworkElement argView);


		/// <summary>
		/// Shows a dialog.
		/// </summary>
		/// <remarks>
		/// The dialog used to represent the ViewModel is retrieved from the registered mappings.
		/// </remarks>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argViewModel">The ViewModel of the new dialog.</param>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		bool? ShowDialog(object argOwnerViewModel, object argViewModel);


		/// <summary>
		/// Shows a dialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argViewModel">The ViewModel of the new dialog.</param>
		/// <typeparam name="T">The type of the dialog to show.</typeparam>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		bool? ShowDialog<T>(object argOwnerViewModel, object argViewModel) where T : Window;


		/// <summary>
		/// Shows a message box.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the message box.
		/// </param>
		/// <param name="argMessageBoxText">A string that specifies the text to display.</param>
		/// <param name="argCaption">A string that specifies the title bar caption to display.</param>
		/// <param name="argButton">
		/// A MessageBoxButton value that specifies which button or buttons to display.
		/// </param>
		/// <param name="argIcon">A MessageBoxImage value that specifies the icon to display.</param>
		/// <returns>
		/// A MessageBoxResult value that specifies which message box button is clicked by the user.
		/// </returns>
		MessageBoxResult ShowMessageBox(
			object argOwnerViewModel,
			string argMessageBoxText,
			string argCaption,
			MessageBoxButton argButton,
			MessageBoxImage argIcon);


		/// <summary>
		/// Shows the OpenFileDialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argOpenFileDialog">The interface of a open file dialog.</param>
		/// <returns>True if successful; otherwise false.</returns>
		bool ShowOpenFileDialog(object argOwnerViewModel, IOpenFileDialog argOpenFileDialog);

	    /// <summary>
	    /// Shows the SaveFileDialog.
	    /// </summary>
	    /// <param name="argOwnerViewModel">
	    /// A ViewModel that represents the owner window of the dialog.
	    /// </param>
	    /// <param name="argSaveFileDialog">The interface of a save file dialog.</param>
        /// <returns>True if successful; otherwise false.</returns>
	    bool ShowSaveFileDialog(object argOwnerViewModel, ISaveFileDialog argSaveFileDialog);

		/// <summary>
		/// Shows the FolderBrowserDialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argFolderBrowserDialog">The interface of a folder browser dialog.</param>
        /// <returns>True if successful; otherwise false.</returns>
		bool ShowFolderBrowserDialog(object argOwnerViewModel, IFolderBrowserDialog argFolderBrowserDialog);
	}
}
