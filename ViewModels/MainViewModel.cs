using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TrolleybusWPFApp.Models;

namespace TrolleybusWPFApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Trolleybus _selectedTrolleybus;

        public ObservableCollection<Trolleybus> Trolleybuses { get; set; }
        public ObservableCollection<string> Logs { get; set; }
        public ICommand AddTrolleybusCommand { get; }
        public ICommand StartTrolleybusCommand { get; }
        public ICommand StopTrolleybusCommand { get; }

        public Trolleybus SelectedTrolleybus
        {
            get => _selectedTrolleybus;
            set
            {
                _selectedTrolleybus = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Trolleybuses = new ObservableCollection<Trolleybus>();
            Logs = new ObservableCollection<string>();

            AddTrolleybusCommand = new RelayCommand(AddTrolleybus);
            StartTrolleybusCommand = new RelayCommand(StartTrolleybus, CanStartTrolleybus);
            StopTrolleybusCommand = new RelayCommand(StopTrolleybus, CanStopTrolleybus);
        }

        private void AddTrolleybus(object obj)
        {
            var driver = new Driver("John Doe");
            var trolleybus = new Trolleybus($"Trolleybus {Trolleybuses.Count + 1}", driver);

            trolleybus.BrokenDown += TrolleybusOnBrokenDown;
            trolleybus.PolesOff += TrolleybusOnPolesOff;

            Trolleybuses.Add(trolleybus);

            Logs.Add($"Added {trolleybus.TrolleybusNumber} with driver {driver.Name}");
        }

        private void TrolleybusOnBrokenDown(Trolleybus trolleybus)
        {
            Logs.Add($"{trolleybus.TrolleybusNumber} has broken down.");
            var emergencyService = new EmergencyService();
            emergencyService.HandleBreakdown(trolleybus);
        }

        private void TrolleybusOnPolesOff(Trolleybus trolleybus)
        {
            Logs.Add($"{trolleybus.TrolleybusNumber} poles have come off.");
            trolleybus.Driver.FixPoles(trolleybus);
        }

        private void StartTrolleybus(object obj)
        {
            SelectedTrolleybus?.Start();
            Logs.Add($"{SelectedTrolleybus?.TrolleybusNumber} started.");
        }

        private bool CanStartTrolleybus(object arg)
        {
            return SelectedTrolleybus != null && !SelectedTrolleybus.IsRunning;
        }

        private void StopTrolleybus(object obj)
        {
            SelectedTrolleybus?.Stop();
            Logs.Add($"{SelectedTrolleybus?.TrolleybusNumber} stopped.");
        }

        private bool CanStopTrolleybus(object arg)
        {
            return SelectedTrolleybus != null && SelectedTrolleybus.IsRunning;
        }
    }
}

