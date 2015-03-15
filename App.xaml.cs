using DialogService.Service;
using log4net;
using PartyTracker.BusinessRules;
using PartyTracker.Main;
using PartyTracker.WindowViewModelMapping;
using ServiceLocator;
using ServiceLocator.WindowViewModelMapping;
using System.ComponentModel;
using System.Windows;

namespace PartyTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(App));

        private MainViewModel _mainViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ThreadContext.Stacks["NDC"].Push("OnStartup"))
            {
                _log.DebugFormat("App is starting...");

                base.OnStartup(e);


                //Service registration goes here.
                Locator.RegisterSingleton<IWindowViewModelMappings, PartyTrackerMapping>();
                Locator.RegisterSingleton<IDialogService, DialogService.Service.DialogService>();

                //This needs o be repalced with a call to register the AgileService (my Model for this app)
                Locator.RegisterSingleton<IPartyTrackerBusinessRules, PartyTrackerBusinessRules>();
                Locator.RegisterSingleton<IApplicationSettings, ApplicationSettings>();
                //Locator.RegisterSingleton<ITempSettings, TempSettings>();
                
                //ServiceLocator.RegisterSingleton<IPersonService, PersonService>();
                //Locator.Register<IOpenFileDialog, OpenFileDialogViewModel>();
                //Locator.Register<ISaveFileDialog, SaveFileDialogViewModel>();

                //Locator.RegisterSingleton<IGenericSerializer, XmlGenericSerializer>();
                //ServiceLocator.RegisterSingleton<IGenericSerializer, BinaryGenericSerializer>();

                //Create the window and show it.
                _mainViewModel = new MainViewModel();
                MainView view = new MainView();

                view.Closing += view_Closing;
                view.DataContext = _mainViewModel;

                view.Show();
            }
        }

        private void view_Closing(object argSender, CancelEventArgs argEventArgs)
        {
            if (_mainViewModel.ClosingCommand != null)
            {
                if (_mainViewModel.ClosingCommand.CanExecute())
                {
                    _mainViewModel.ClosingCommand.Execute();
                    Current.Shutdown();
                }
                else
                {
                    argEventArgs.Cancel = true;
                }
            }
        }
    }
}
