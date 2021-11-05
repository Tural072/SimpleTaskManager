using SimpleTaskManager.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SimpleTaskManager.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindow MainWindow { get; set; }
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
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            dispatcherTimer.Start();


            MainWindow = mainWindow;
            AddCommand = new RelayCommand((sender =>
            {
                mainWindow.wishListView.Items.Add(mainWindow.wishlistTxtbx.Text);
            }));

            CreateCommand = new RelayCommand((sender =>
            {
                if (mainWindow.wishListView.Items == null)
                {
                    Process.Start(mainWindow.processNameTxtbx.Text);
                }
                else
                {
                    try
                    {
                        Process.Start(mainWindow.processNameTxtbx.Text);
                        Thread.Sleep(4000);
                        foreach (var item in mainWindow.wishListView.Items)
                        {
                            var process = Processes.FirstOrDefault(p => p.ProcessName == item.ToString());
                            process.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }));

            EndCommand = new RelayCommand((sender =>
            {
                try
                {
                    if (SelectedProcess != null)
                    {
                        SelectedProcess.Kill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }));

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            MainWindow.propcessesListView.ItemsSource = Processes;
        }
    }
}
