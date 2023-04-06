using GTA;
using GTA.Native;
using NativeUI;
using System.Windows.Forms;
using System;
using GTA.Math;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

namespace ImportExportModNamespace
{
    public class ImportExportMod : Script
    {
        public static string ModPath { get; private set; }

        private CarSourceManager _carSourceManager;
        private InteriorStyles _interiorStyles;
        private WarehouseInterior _interior;

        private MenuPool menuPool;
        private UIMenu warehouseMenu;
        private UIMenu exitWarehouseMenu;
        private UIMenu laptopMenu;

        public ImportExportMod()
        {
            GTA.UI.Notification.Show("ImportExportMod script has started.");
            _interior = new WarehouseInterior();

            menuPool = new MenuPool();

            _interiorStyles = new InteriorStyles(menuPool, _interior);

            _carSourceManager = new CarSourceManager(ModPath);

            SetupWarehouseMenu();
            SetupInteriorMenus();

            Tick += OnTick;
            KeyUp += OnKeyUp;
        }

        // ... (rest of the code)

        private void OnTick(object sender, EventArgs e)
        {
            menuPool.ProcessMenus();
            CheckPlayerProximity().ConfigureAwait(false);
            
            Vector3 exitWarehouseLocation = new Vector3(970.7842f, -2987.536f, -39.6470f);
            Vector3 laptopLocation = new Vector3(965.0377f, -3003.491f, -39.6399f);

            float distanceToExitWarehouse = Game.Player.Character.Position.DistanceTo(exitWarehouseLocation);
            float distanceToLaptop = Game.Player.Character.Position.DistanceTo(laptopLocation);

            if (distanceToExitWarehouse < 3f && !exitWarehouseMenu.Visible)
            {
                exitWarehouseMenu.Visible = true;
                GTA.UI.Notification.Show("Exit Warehouse Menu should be visible.");
            }
            else if (distanceToExitWarehouse >= 3f && exitWarehouseMenu.Visible)
            {
                exitWarehouseMenu.Visible = false;
            }

            if (distanceToLaptop < 2f && !laptopMenu.Visible)
            {
                laptopMenu.Visible = true;
                GTA.UI.Notification.Show("Laptop Menu should be visible.");
            }
            else if (distanceToLaptop >= 2f && laptopMenu.Visible)
            {
                laptopMenu.Visible = false;
            }
        }


        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                _interior.LoadInteriorIPLs();
                Game.Player.Character.Position = new Vector3(970.7842f, -2987.536f, -39.6470f); // Replace with the actual interior coordinates
            }
        }

        private void SetupWarehouseMenu()
        {
            warehouseMenu = new UIMenu("Warehouse", "OPTIONS");
            menuPool.Add(warehouseMenu);

            UIMenuItem purchaseItem = new UIMenuItem("Purchase Warehouse");
            UIMenuItem sellItem = new UIMenuItem("Sell Warehouse");
            UIMenuItem enterItem = new UIMenuItem("Enter Warehouse");

            warehouseMenu.AddItem(purchaseItem);
            warehouseMenu.AddItem(sellItem);
            warehouseMenu.AddItem(enterItem);

            warehouseMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == purchaseItem)
                {
                    // Code to purchase a warehouse
                }
                else if (item == sellItem)
                {
                    // Code to sell a warehouse
                }
                else if (item == enterItem)
                {
                    // Code to enter a warehouse
                }
            };

            warehouseMenu.RefreshIndex();
        }

        private void SetupInteriorMenus()
        {
            // Setup exit warehouse menu
            exitWarehouseMenu = new UIMenu("Warehouse", "EXIT");
            menuPool.Add(exitWarehouseMenu);

            UIMenuItem exitItem = new UIMenuItem("Exit Warehouse");
            exitWarehouseMenu.AddItem(exitItem);

            exitWarehouseMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == exitItem)
                {
                    // Code to exit the warehouse
                }
            };

            exitWarehouseMenu.RefreshIndex();

            // Setup laptop menu
            laptopMenu = new UIMenu("Laptop", "OPTIONS");
            menuPool.Add(laptopMenu);

            UIMenuItem sourceVehicleItem = new UIMenuItem("Source a Vehicle");
            UIMenuItem sellWarehouseVehicleItem = new UIMenuItem("Sell Warehouse Vehicle");
            UIMenuItem customizeWarehouseItem = new UIMenuItem("Customize Warehouse");

            laptopMenu.AddItem(sourceVehicleItem);
            laptopMenu.AddItem(sellWarehouseVehicleItem);
            laptopMenu.AddItem(customizeWarehouseItem);

            laptopMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == sourceVehicleItem)
            {
                // Code to source a vehicle
                CarSourceManager carSourceManager = new CarSourceManager(ModPath);
                carSourceManager.StartRandomScenario();
            }
            else if (item == sellWarehouseVehicleItem)
            {
                // Code to sell warehouse vehicle
            }
            else if (item == customizeWarehouseItem)
            {
                _interiorStyles.ShowMenu(laptopMenu);
            }
        };


            laptopMenu.RefreshIndex();
        }

        private async Task CheckPlayerProximity()
{
    Vector3 exitWarehouseLocation = new Vector3(970.7842f, -2987.536f, -39.6470f);
    Vector3 laptopLocation = new Vector3(965.0377f, -3003.491f, -39.6399f);

    float distanceToExitWarehouse = Game.Player.Character.Position.DistanceTo(exitWarehouseLocation);
    float distanceToLaptop = Game.Player.Character.Position.DistanceTo(laptopLocation);

    if (distanceToExitWarehouse < 3f && !exitWarehouseMenu.Visible)
    {
        exitWarehouseMenu.Visible = true;
        GTA.UI.Notification.Show("Exit Warehouse Menu should be visible.");
    }
    else if (distanceToExitWarehouse >= 3f && exitWarehouseMenu.Visible)
    {
        exitWarehouseMenu.Visible = false;
    }

    if (distanceToLaptop < 2f && !laptopMenu.Visible)
    {
        laptopMenu.Visible = true;
        GTA.UI.Notification.Show("Laptop Menu should be visible.");
    }
    else if (distanceToLaptop >= 2f && laptopMenu.Visible)
    {
        laptopMenu.Visible = false;
    }

    await Task.FromResult(0);
}


    }
}

