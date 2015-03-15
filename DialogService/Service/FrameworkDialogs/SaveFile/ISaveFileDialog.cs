
namespace DialogServiceLibrary.Service.FrameworkDialogs.SaveFile
{
	/// <summary>
	/// Interface describing the OpenFileDialog.
	/// </summary>
	public interface ISaveFileDialog : IFileDialog
	{
		/// <summary>
		/// Gets or sets a value indicating whether the dialog box allows multiple files to be
		/// selected.
		/// </summary>
		bool Multiselect { get; set; }
	}
}
