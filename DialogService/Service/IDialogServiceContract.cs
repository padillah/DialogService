using DialogServiceLibrary.Service.FrameworkDialogs.FolderBrowse;
using DialogServiceLibrary.Service.FrameworkDialogs.OpenFile;
using DialogServiceLibrary.Service.FrameworkDialogs.SaveFile;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;

namespace DialogServiceLibrary.Service
{
	[ContractClassFor(typeof(IDialogService))]
	abstract class IDialogServiceContract : IDialogService
	{
		/// <summary>
		/// Gets the registered views.
		/// </summary>
		public ReadOnlyCollection<FrameworkElement> Views
		{
			get { return default(ReadOnlyCollection<FrameworkElement>); }
		}


		/// <summary>
		/// Registers a View.
		/// </summary>
		/// <param name="argView">The registered View.</param>
		public void Register(FrameworkElement argView)
		{
			Contract.Requires(argView != null);
			Contract.Requires(!Views.Contains(argView));
		}


		/// <summary>
		/// Unregisters a View.
		/// </summary>
		/// <param name="argView">The unregistered View.</param>
		public void Unregister(FrameworkElement argView)
		{
			Contract.Requires(Views.Contains(argView));
		}


		/// <summary>
		/// Shows a modal dialog.
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
		public bool? ShowDialog(object argOwnerViewModel, object argViewModel)
		{
			Contract.Requires(argOwnerViewModel != null);
			Contract.Requires(argViewModel != null);

			return default(bool?);
		}
        

		/// <summary>
		/// Shows a modal dialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argViewModel">The ViewModel of the new dialog.</param>
		/// <typeparam name="T">The type of the dialog to show.</typeparam>
		/// <returns>
		/// A nullable value of type bool that signifies how a window was closed by the user.
		/// </returns>
		public bool? ShowDialog<T>(object argOwnerViewModel, object argViewModel) where T : Window
		{
			Contract.Requires(argOwnerViewModel != null);
			Contract.Requires(argViewModel != null);
			
			return default(bool?);
		}


        /// <summary>
        /// Shows a modal dialog.
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
        public void Show(object argOwnerViewModel, object argViewModel)
        {
            Contract.Requires(argOwnerViewModel != null);
            Contract.Requires(argViewModel != null);
        }


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
        public void Show<T>(object argOwnerViewModel, object argViewModel) where T : Window
        {
            Contract.Requires(argOwnerViewModel != null);
            Contract.Requires(argViewModel != null);
        }


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
        public MessageBoxResult ShowMessageBox(
			object argOwnerViewModel,
			string argMessageBoxText,
			string argCaption,
			MessageBoxButton argButton,
			MessageBoxImage argIcon)
		{
			Contract.Requires(argOwnerViewModel != null);
			Contract.Requires(!string.IsNullOrWhiteSpace(argMessageBoxText));
			Contract.Requires(!string.IsNullOrWhiteSpace(argCaption));

			return default(MessageBoxResult);
		}


		/// <summary>
		/// Shows the OpenFileDialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argOpenFileDialog">The interface of a open file dialog.</param>
		/// <returns>
		/// DialogResult.OK if successful; otherwise DialogResult.Cancel.
		/// </returns>
		public bool ShowOpenFileDialog(object argOwnerViewModel, IOpenFileDialog argOpenFileDialog)
		{
			Contract.Requires(argOwnerViewModel != null);
			Contract.Requires(argOpenFileDialog != null);

			return default(bool);
		}


        /// <summary>
        /// Shows the SaveFileDialog.
        /// </summary>
        /// <param name="argOwnerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="argSaveFileDialog">The interface of a save file dialog.</param>
        /// <returns>
        /// DialogResult.OK if successful; otherwise DialogResult.Cancel.
        /// </returns>
        public bool ShowSaveFileDialog(object argOwnerViewModel, ISaveFileDialog argSaveFileDialog)
        {
            Contract.Requires(argOwnerViewModel != null);
            Contract.Requires(argSaveFileDialog != null);

            return default(bool);
        }


		/// <summary>
		/// Shows the FolderBrowserDialog.
		/// </summary>
		/// <param name="argOwnerViewModel">
		/// A ViewModel that represents the owner window of the dialog.
		/// </param>
		/// <param name="argFolderBrowserDialog">The interface of a folder browser dialog.</param>
		/// <returns>
		/// The DialogResult.OK if successful; otherwise DialogResult.Cancel.
		/// </returns>
		public bool ShowFolderBrowserDialog(object argOwnerViewModel, IFolderBrowserDialog argFolderBrowserDialog)
		{
			Contract.Requires(argOwnerViewModel != null);
			Contract.Requires(argFolderBrowserDialog != null);

			return default(bool);
		}
	}
}
