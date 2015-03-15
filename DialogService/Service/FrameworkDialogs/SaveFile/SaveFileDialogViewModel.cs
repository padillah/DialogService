namespace DialogServiceLibrary.Service.FrameworkDialogs.SaveFile
{
	/// <summary>
	/// ViewModel of the OpenFileDialog.
	/// </summary>
	public class SaveFileDialogViewModel : FileDialogViewModel, ISaveFileDialog
	{
		/// <summary>
		/// Gets or sets a value indicating whether the dialog box allows multiple files to be selected.
		/// </summary>
		public bool Multiselect { get; set; }
	}
}
