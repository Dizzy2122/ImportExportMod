using System;
using GTA;
using GTA.Math;
using NativeUI;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ImportExportModNamespace
{
    public class CarSourceManager
    {
        private string _modPath;

        public CarSourceManager(string modPath)
        {
            _modPath = modPath;
            CreateDefaultIniFileIfNotExists(_modPath);
            LoadCarListsFromIni();
            LoadCarSelectionModeFromIni();
            SetupMixedCarsList();
        }
        // Class member variables for car lists
        private List<string> _defaultCars = new List<string>();
        private List<string> _addonCars = new List<string>();
        private CarSelectionMode _carSelectionMode = CarSelectionMode.Mixed;
        private List<string> _mixedCars;
        private CarSourceScenario _currentScenario;

        private void CreateDefaultIniFileIfNotExists(string configPath)
        {
            GTA.UI.Notification.Show($"INI file path: {configPath}");

            if (!File.Exists(configPath))
            {
                try
                {
                    using (StreamWriter sw = File.CreateText(configPath))
                    {
                        sw.WriteLine("[Settings]");
                        sw.WriteLine("IncludeDefaultCars=true");
                        sw.WriteLine("IncludeAddonCars=false");
                        sw.WriteLine("AddonCars=car1,car2,car3");
                        sw.WriteLine("MaxVehicleSlots=20");
                    }
                }
                catch (Exception ex)
                {
                    GTA.UI.Notification.Show($"Failed to create INI file: {ex.Message}");
                }
            }
        }

        private void LoadCarListsFromIni()
        {
            ScriptSettings config = ScriptSettings.Load(_modPath);

            _defaultCars = new List<string>(config.GetValue<string>("CarLists", "DefaultCars", "").Split(','));
            _addonCars = new List<string>(config.GetValue<string>("CarLists", "AddonCars", "").Split(','));
        }

        private void LoadCarSelectionModeFromIni()
        {
            CreateDefaultIniFileIfNotExists(_modPath);
            ScriptSettings config = ScriptSettings.Load(_modPath);

            string selectionModeString = config.GetValue<string>("Settings", "SelectionMode", "Mixed");
            _carSelectionMode = (CarSelectionMode)Enum.Parse(typeof(CarSelectionMode), selectionModeString);
        }

        private void SetupMixedCarsList()
        {
            _mixedCars = new List<string>();

            if (_carSelectionMode == CarSelectionMode.Default || _carSelectionMode == CarSelectionMode.Mixed)
            {
                _mixedCars.AddRange(_defaultCars);
            }

            if (_carSelectionMode == CarSelectionMode.Addon || _carSelectionMode == CarSelectionMode.Mixed)
            {
                _mixedCars.AddRange(_addonCars);
            }
        }

        public void StartRandomScenario()
        {
            // Choose a random car from the _mixedCars list
            Random random = new Random();
            string randomCarModel = _mixedCars[random.Next(_mixedCars.Count)];

            // Choose a random scenario
            // For now, we only have the Parked Car scenario
            // Later, you can add more scenarios to the list and choose one randomly

            // Example parked location
            Vector3 parkedLocation = new Vector3(-2034.75f, -463.05f, 11.35f);

            _currentScenario = new ParkedCarScenario(parkedLocation);
            _currentScenario.StartScenario();
        }

        // Add other car sourcing-related properties and methods here
    }

    public enum CarSelectionMode
    {
       
        Default,
        Addon,
        Mixed
    }
}
