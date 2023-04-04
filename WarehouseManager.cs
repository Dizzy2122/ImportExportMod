// WarehouseManager.cs
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
                blip.Name = warehouse.Name;
                blip.IsShortRange = true;
                warehouse.Blip = blip;
            }
        }

        public void UpdateOwnedWarehouseBlip()
{
    if (OwnedWarehouseBlip != null)
    {
        OwnedWarehouseBlip.Color = BlipColor.Blue;
        OwnedWarehouseBlip.Scale = 1f;
        OwnedWarehouseBlip.IsShortRange = true;
        OwnedWarehouseBlip.Name = "Owned Warehouse";
    }
    GTA.UI.Notification.Show("UpdateOwnedWarehouseBlip called.");
}


        public void SetOwnedWarehouseLocation(Vector3 location)
{
    foreach (var warehouse in Warehouses)
    {
        if (warehouse.Location == location)
        {
            OwnedWarehouse = warehouse;
            break;
        }
    }

    UpdateOwnedWarehouseBlip();
    GTA.UI.Notification.Show("UpdateOwnedWarehouseBlip called from SetOwnedWarehouseLocation.");
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
            GTA.UI.Notification.Show($"UpdateNearestWarehouse: NearestWarehouseLocation = {NearestWarehouseLocation}");
        }



        public void SetOwnedWarehouseBlip(Blip blip)
        {
            if (OwnedWarehouseBlip != null)
            {
                OwnedWarehouseBlip.Delete();
            }

            OwnedWarehouseBlip = blip;
            OwnedWarehouseBlip.Color = BlipColor.Green; // Change the color to the desired one
            OwnedWarehouseBlip.IsShortRange = true;
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
    }










    
    
    // Other properties and methods
}




