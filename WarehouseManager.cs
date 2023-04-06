// WarehouseManager.cs
using System.IO;
using System;
using GTA;
using GTA.Native;
using NativeUI;
using System.Windows.Forms;
using System.Collections.Generic;
using GTA.Math;

namespace ImportExportModNamespace
{
    public class WarehouseManager
{
    public bool IsPlayerNearWarehouse
    {
        get
        {
            if (OwnedWarehouseLocation == Vector3.Zero) return false;
            return World.GetDistance(Game.Player.Character.Position, OwnedWarehouseLocation) < 50f;
        }
    }
        public int WarehouseCost { get; } = 0; // Change when version is final
        public Blip OwnedWarehouseBlip { get; private set; }
        public List<Warehouse> Warehouses { get; private set; }
        public Warehouse NearestWarehouse { get; set; }
        public Vector3 NearestWarehouseLocation => NearestWarehouse?.Location ?? Vector3.Zero;
        public Warehouse OwnedWarehouse { get; private set; }
        public Vector3 OwnedWarehouseLocation
        
        {
            get => OwnedWarehouse?.Location ?? Vector3.Zero;
            private set
            {
                if (OwnedWarehouse != null)
                {
                    OwnedWarehouse.Location = value;
                }
            }
        }


        public WarehouseManager()
        {
            InitializeWarehouses();
            LoadOwnedWarehouseData();
            
        }
        

        private void InitializeWarehouses()
        {
            Warehouses = new List<Warehouse>
            {
                new Warehouse(new Vector3(144.3558f, -3004.987f, 7.030922f)), // Elysian Island Warehouse
                new Warehouse(new Vector3(804.5468f, -2220.445f, 29.44725f)), // Cypress Flats Warehouse
                new Warehouse(new Vector3(1211.428f, -1262.586f, 35.22675f)), // Murrieta Heights Warehouse
                new Warehouse(new Vector3(1764.536f, -1647.494f, 112.6444f)), // El Buro Heights Warehouse
                new Warehouse(new Vector3(-71.89099f, -1821.3256f, 26.94197f)), // Davis Street Warehouse
                new Warehouse(new Vector3(-1152.891f, -2173.466f, 13.26305f)), // Los Santos International Airport Warehouse
                new Warehouse(new Vector3(-513.2588f, -2199.715f, 6.394024f)), // Los Santos International Airport Warehouse 2
                new Warehouse(new Vector3(-636.0688f, -1774.854f, 24.0514f)), // La Puerta Warehouse
                new Warehouse(new Vector3(998.7968f, -1855.474f, 31.03981f)) // La Mesa Warehouse
            };

            CreateBlips();
        }

        public void CreateBlips()
        {
            foreach (Warehouse warehouse in Warehouses)
            {
                Blip blip = World.CreateBlip(warehouse.Location);
                blip.Sprite = BlipSprite.Warehouse;
                blip.Name = "Warehouse";
                blip.IsShortRange = true;
                warehouse.Blip = blip;
            }
        }



        public void SetOwnedWarehouseLocation(Vector3 location)
{
    // If there's already an OwnedWarehouseBlip, reset it
    if (OwnedWarehouseBlip != null)
    {
        OwnedWarehouseBlip.Color = BlipColor.White;
        OwnedWarehouseBlip.Scale = 1f;
        OwnedWarehouseBlip.IsShortRange = true;
        OwnedWarehouseBlip.Name = "Warehouse";
    }

    foreach (var warehouse in Warehouses)
    {
        if (warehouse.Location == location)
        {
            OwnedWarehouse = warehouse;
            break;
        }
    }

    // If there's an OwnedWarehouse, update its blip
    if (OwnedWarehouse != null)
    {
        OwnedWarehouseBlip = OwnedWarehouse.Blip;
        OwnedWarehouseBlip.Color = BlipColor.Yellow;
        OwnedWarehouseBlip.Scale = 1f;
        OwnedWarehouseBlip.IsShortRange = true;
        OwnedWarehouseBlip.Name = "Owned Warehouse";
    }
}






        public void CheckPlayerProximity()
        {
            NearestWarehouse = GetNearestWarehouse();
        }

        private Warehouse GetNearestWarehouse()
        {
            Warehouse nearest = null;
            float minDistance = float.MaxValue;

            foreach (var warehouse in Warehouses)
            {
                float distance = Game.Player.Character.Position.DistanceTo(warehouse.Location);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = warehouse;
                }
            }

            return minDistance <= 100.0f ? nearest : null;
        }

        public void UpdateNearestWarehouse()
        {
            NearestWarehouse = GetNearestWarehouse();
        }


        public void RemoveOwnedWarehouse()
        {
            OwnedWarehouse = null;
        }


        public void RemoveBlips()
        {
            foreach (Warehouse warehouse in Warehouses)
            {
                warehouse.Blip?.Delete();
            }
        }


        public void SaveOwnedWarehouseData()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ownedWarehouse.ini");
            ScriptSettings config = ScriptSettings.Load(configFilePath);

            if (OwnedWarehouse != null)
            {
                config.SetValue("Warehouse", "Location", OwnedWarehouse.Location);
            }
            else
            {
                config.SetValue("Warehouse", "Location", Vector3.Zero); // Set to Vector3.Zero when there's no owned warehouse
            }
            config.Save();
        }

        public void LoadOwnedWarehouseData()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ownedWarehouse.ini");

            if (File.Exists(configFilePath))
            {
                GTA.UI.Notification.Show("Loading owned warehouse data...");
                ScriptSettings config = ScriptSettings.Load(configFilePath);
                float x = config.GetValue<float>("Warehouse", "X", 0);
                float y = config.GetValue<float>("Warehouse", "Y", 0);
                float z = config.GetValue<float>("Warehouse", "Z", 0);
                Vector3 location = new Vector3(x, y, z);

                if (location != Vector3.Zero)
                {
                    SetOwnedWarehouseLocation(location);
                    GTA.UI.Notification.Show($"Loaded owned warehouse at {location}");
                }
                else
                {
                    GTA.UI.Notification.Show("No owned warehouse found in INI file.");
                    RemoveOwnedWarehouse();
                }
            }
            else
            {
                GTA.UI.Notification.Show("INI file not found. No owned warehouse to load.");
                RemoveOwnedWarehouse();
            }
        }

        public bool IsNearestWarehouseOwned()
        {
            return OwnedWarehouseLocation == NearestWarehouseLocation;
        }







        private string GetIniFilePath()
        {
            string scriptFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
            return Path.Combine(scriptFolderPath, "ownedWarehouse.ini");
        }


        private ScriptSettings LoadIniFile()
        {
            string iniFilePath = GetIniFilePath();
            if (!File.Exists(iniFilePath))
            {
                File.Create(iniFilePath).Dispose();
            }

            return ScriptSettings.Load(iniFilePath);
        }
    }
}





