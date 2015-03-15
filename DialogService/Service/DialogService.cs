using DialogService.Service.FrameworkDialogs;
using DialogService.Service.FrameworkDialogs.FolderBrowse;
using DialogService.Service.FrameworkDialogs.OpenFile;
using DialogService.Service.FrameworkDialogs.SaveFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using ServiceLocator;
using ServiceLocator.WindowViewModelMapping;
using FolderBrowserDialog = DialogService.Service.FrameworkDialogs.FolderBrowse.FolderBrowserDialog;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = DialogService.Service.FrameworkDialogs.OpenFile.OpenFileDialog;
using SaveFileDialog = DialogService.Service.FrameworkDialogs.SaveFile.SaveFileDialog;

namespace DialogService.Service
{
    /// <summary>
    /// Class responsible for abstracting ViewModels from Views.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly HashSet<FrameworkElement> _views;
        private readonly IWindowViewModelMappings _windowViewModelMappings;


        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        /// <param name="argWindowViewModelMappings">
        /// The window ViewModel mappings. Default value is null.
        /// </param>
        public DialogService(IWindowViewModelMappings argWindowViewModelMappings = null)
        {
            _windowViewModelMappings = argWindowViewModelMappings;

            _views = new HashSet<FrameworkElement>();
        }


        #region IDialogService Members

        /// <summary>
        /// Gets the registered views.
        /// </summary>
        public ReadOnlyCollection<FrameworkElement> Views
        {
            get { return new ReadOnlyCollection<FrameworkElement>(_views.ToList()); }
        }


        /// <summary>
        /// Registers a View.
        /// </summary>
        /// <param name="argView">The registered View.</param>
        public void Register(FrameworkElement argView)
        {
            // Get owner window
            Window owner = GetOwner(argView);
            if (owner == null)
            {
                // Perform a late register when the View hasn't been loaded yet.
                // This will happen if e.g. the View is contained in a Frame.
                argView.Loaded += LateRegister;
                return;
            }

            // Register for owner window closing, since we then should unregister View reference,
            // preventing memory leaks
            owner.Closed += OwnerClosed;

            _views.Add(argView);
        }


        /// <summary>
        /// Unregisters a View.
        /// </summary>
        /// <param name="argView">The unregistered View.</param>
        public void Unregister(FrameworkElement argView)
        {
            _views.Remove(argView);
        }


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
        public bool? ShowDialog(object argOwnerViewModel, object argViewModel)
        {
            Type dialogType = _windowViewModelMappings.GetWindowTypeFromViewModelType(argViewModel.GetType());
            return ShowDialog(argOwnerViewModel, argViewModel, dialogType);
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
        public bool? ShowDialog<T>(object argOwnerViewModel, object argViewModel) where T : Window
        {
            return ShowDialog(argOwnerViewModel, argViewModel, typeof(T));
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
        public MessageBoxResult ShowMessageBox(object argOwnerViewModel, string argMessageBoxText, string argCaption, MessageBoxButton argButton, MessageBoxImage argIcon)
        {
            return MessageBox.Show(FindOwnerWindow(argOwnerViewModel), argMessageBoxText, argCaption, argButton, argIcon);
        }


        /// <summary>
        /// Shows the OpenFileDialog.
        /// </summary>
        /// <param name="argOwnerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="argOpenFileDialog">The interface of a open file dialog.</param>
        /// <returns>DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
        public bool ShowOpenFileDialog(object argOwnerViewModel, IOpenFileDialog argOpenFileDialog)
        {
            // Create OpenFileDialog with specified ViewModel
            OpenFileDialog dialog = new OpenFileDialog(argOpenFileDialog);

            // Show dialog
            DialogResult returnResult = dialog.ShowDialog(new WindowWrapper(FindOwnerWindow(argOwnerViewModel)));
            if (returnResult == DialogResult.OK || returnResult == DialogResult.Yes)
            {
                return true;
            }

            return false;

        }


        /// <summary>
        /// Shows the SaveFileDialog.
        /// </summary>
        /// <param name="argOwnerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="argSaveFileDialog">The interface of a save file dialog.</param>
        /// <returns>DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
        public bool ShowSaveFileDialog(object argOwnerViewModel, ISaveFileDialog argSaveFileDialog)
        {
            // Create OpenFileDialog with specified ViewModel
            SaveFileDialog dialog = new SaveFileDialog(argSaveFileDialog);

            // Show dialog
            DialogResult returnResult = dialog.ShowDialog(new WindowWrapper(FindOwnerWindow(argOwnerViewModel)));
            if (returnResult == DialogResult.OK || returnResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Shows the FolderBrowserDialog.
        /// </summary>
        /// <param name="argOwnerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="argFolderBrowserDialog">The interface of a folder browser dialog.</param>
        /// <returns>The DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
        public bool ShowFolderBrowserDialog(object argOwnerViewModel, IFolderBrowserDialog argFolderBrowserDialog)
        {
            // Create FolderBrowserDialog with specified ViewModel
            FolderBrowserDialog dialog = new FolderBrowserDialog(argFolderBrowserDialog);

            // Show dialog
            DialogResult returnResult = dialog.ShowDialog(new WindowWrapper(FindOwnerWindow(argOwnerViewModel)));
            if (returnResult == DialogResult.OK || returnResult == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        #endregion


        #region Attached properties

        /// <summary>
        /// Attached property describing whether a FrameworkElement is acting as a View in MVVM.
        /// </summary>
        public static readonly DependencyProperty CanOpenWindowsProperty =
            DependencyProperty.RegisterAttached("CanOpenWindows", typeof(bool), typeof(DialogService), new UIPropertyMetadata(CanOpenWindowsPropertyChanged));


        /// <summary>
        /// Gets value describing whether FrameworkElement is acting as View in MVVM.
        /// </summary>
        public static bool GetCanOpenWindows(FrameworkElement argTarget)
        {
            return (bool)argTarget.GetValue(CanOpenWindowsProperty);
        }


        /// <summary>
        /// Sets value describing whether FrameworkElement is acting as View in MVVM.
        /// </summary>
        public static void SetCanOpenWindows(FrameworkElement argTarget, bool argValue)
        {
            argTarget.SetValue(CanOpenWindowsProperty, argValue);
        }


        /// <summary>
        /// Is responsible for handling CanOpenWindows changes, i.e. whether
        /// FrameworkElement is acting as View in MVVM or not.
        /// </summary>
        private static void CanOpenWindowsPropertyChanged(DependencyObject argTarget, DependencyPropertyChangedEventArgs argEventArgs)
        {
            // The Visual Studio Designer or Blend will run this code when setting the attached
            // property, however at that point there is no IDialogService registered
            // in the ServiceLocator which will cause the Resolve method to throw a ArgumentException.
            if (DesignerProperties.GetIsInDesignMode(argTarget)) return;

            //Get the current FrameworkElement
            FrameworkElement view = argTarget as FrameworkElement;
            
            if (view != null)
            {
                // Cast values
                bool newValue = (bool)argEventArgs.NewValue;
                //bool oldValue = (bool)argEventArgs.OldValue;

                if (newValue)
                {
                    //If this is an add then register the view with the service
                    Locator.Resolve<IDialogService>().Register(view);
                }
                else
                {
                    //If this is not an add then unregister the view from the service
                    Locator.Resolve<IDialogService>().Unregister(view);
                }
            }
        }

        #endregion



        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <param name="argOwnerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="argViewModel">The ViewModel of the new dialog.</param>
        /// <param name="argDialogType">The type of the dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        private bool? ShowDialog(object argOwnerViewModel, object argViewModel, Type argDialogType)
        {
            // Create dialog and set properties
            Window dialog = (Window)Activator.CreateInstance(argDialogType);
            dialog.Owner = FindOwnerWindow(argOwnerViewModel);
            dialog.DataContext = argViewModel;

            // Show dialog
            return dialog.ShowDialog();
        }

        /// <summary>
        /// Finds window corresponding to specified ViewModel.
        /// </summary>
        private Window FindOwnerWindow(object argViewModel)
        {
            FrameworkElement view = _views.SingleOrDefault(argElement => ReferenceEquals(argElement.DataContext, argViewModel));
            if (view == null)
            {
                throw new ArgumentException("Viewmodel is not referenced by any registered View.");
            }

            // Get owner window
            Window owner = view as Window;
            if (owner == null)
            {
                owner = Window.GetWindow(view);
            }

            // Make sure owner window was found
            if (owner == null)
            {
                throw new InvalidOperationException("View is not contained within a Window.");
            }

            return owner;
        }


        /// <summary>
        /// Callback for late View register. It wasn't possible to do a instant register since the
        /// View wasn't at that point part of the logical nor visual tree.
        /// </summary>
        private void LateRegister(object argSender, RoutedEventArgs argEventArgs)
        {
            FrameworkElement view = argSender as FrameworkElement;
            if (view != null)
            {
                // Unregister loaded event
                view.Loaded -= LateRegister;

                // Register the view
                Register(view);
            }
        }


        /// <summary>
        /// Handles owner window closed, View service should then unregister all Views acting
        /// within the closed window.
        /// </summary>
        private void OwnerClosed(object argSender, EventArgs argEventArgs)
        {
            Window owner = argSender as Window;
            if (owner != null)
            {
                // Find Views acting within closed window
                IEnumerable<FrameworkElement> windowViews =
                    from view in _views
                    where Window.GetWindow(view) == owner
                    select view;

                // Unregister Views in window
                foreach (FrameworkElement view in windowViews.ToArray())
                {
                    Unregister(view);
                }
            }
        }


        /// <summary>
        /// Gets the owning Window of a view.
        /// </summary>
        /// <param name="argView">The view.</param>
        /// <returns>The owning Window if found; otherwise null.</returns>
        private Window GetOwner(FrameworkElement argView)
        {
            return argView as Window ?? Window.GetWindow(argView);
        }
    }
}
