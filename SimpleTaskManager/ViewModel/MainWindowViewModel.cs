using SimpleTaskManager.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskManager.ViewModel
{
    public class MainWindowViewModel:BaseViewModel
    {
        public ObservableCollection<Process> Processes { get; set; }
        private Process selectedProcess;

        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set { selectedProcess = value; }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand EndCommand { get; set; }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());

            AddCommand = new RelayCommand((sender => 
            {

            }));

            CreateCommand = new RelayCommand((sender =>
            {
                Process.Start(mainWindow.processNameTxtbx.Text);
            }));

            EndCommand = new RelayCommand((sender =>
            {
                if (SelectedProcess != null)
                {
                    SelectedProcess.Kill();
                }
            }));
            
        }

    }
}
