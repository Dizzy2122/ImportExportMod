// ImportExportMod.cs
using GTA;
using GTA.Native;
using NativeUI;
using System.Windows.Forms;
using System;
using GTA.Math;

namespace ImportExportModNamespace
{
    public class ImportExportMod : Script
    {
        // Declare managers here
        private WarehouseManager warehouseManager;
        private CarSourceManager carSourceManager;
        private CustomizationManager customizationManager;

        // Declare 'menuPool' and 'warehouseMenu' as class-level variables
        private MenuPool menuPool;
        private UIMenu warehouseMenu;

        public ImportExportMod()
        {
            // Initialize managers here
            warehouseManager = new WarehouseManager();
            carSourceManager = new CarSourceManager();
            customizationManager = new CustomizationManager();

            warehouseManager.CreateBlips();

            // Initialize MenuPool
            menuPool = new MenuPool();

            // Setup warehouse menu
            SetupWarehouseMenu();

            // Subscribe to the Tick event
            Tick += OnTick;

            // Subscribe to the KeyUp event
            KeyUp += OnKeyUp;

            // Other initialization code, including setting up menus, key bindings, etc.
        }

        private void OnTick(object sender, EventArgs e)
{
    menuPool.ProcessMenus();
    warehouseManager.UpdateNearestWarehouse();

    float distanceToNearestWarehouse = Game.Player.Character.Position.DistanceTo(warehouseManager.NearestWarehouseLocation);

    if (distanceToNearestWarehouse < 5f)
    {
        bool isOwned = warehouseManager.OwnedWarehouseLocation == warehouseManager.NearestWarehouseLocation;

        warehouseMenu.MenuItems[0].Enabled = !isOwned;
        warehouseMenu.MenuItems[1].Enabled = isOwned;
        warehouseMenu.MenuItems[2].Enabled = isOwned;

        if (!warehouseMenu.Visible)
        {
            warehouseMenu.Visible = true;
        }
    }
    else
    {
        if (warehouseMenu.Visible)
        {
            warehouseMenu.Visible = false;
        }
    }

    GTA.UI.Notification.Show($"Menu Items: Purchase: {warehouseMenu.MenuItems[0].Enabled}, Sell: {warehouseMenu.MenuItems[1].Enabled}, Enter: {warehouseMenu.MenuItems[2].Enabled}");

    // Check for the F9 key
    if (Game.IsKeyPressed(Keys.F9)) // Probably remove this when the script is final
    {
        // Remove all warehouse blips
        warehouseManager.RemoveBlips();
    }
}



        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Remove all warehouse blips
                warehouseManager.RemoveBlips();
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
                GTA.UI.Notification.Show($"Item selected: {item.Text}");

                if (item == purchaseItem)
                {
                    GTA.UI.Notification.Show("Purchase Warehouse selected");
                    warehouseManager.SetOwnedWarehouseLocation(warehouseManager.NearestWarehouseLocation);

                    warehouseManager.SetOwnedWarehouseBlip(World.CreateBlip(warehouseManager.NearestWarehouseLocation));

                    warehouseManager.UpdateOwnedWarehouseBlip();
                }
                else if (item == sellItem)
                {
                    GTA.UI.Notification.Show("Sell Warehouse selected");
                    warehouseManager.RemoveOwnedWarehouse();

                    warehouseManager.UpdateOwnedWarehouseBlip();
                }
                else if (item == enterItem)
                {
                    GTA.UI.Notification.Show("Enter Warehouse selected");
                    // Code to enter the warehouse
                }
            };

            warehouseMenu.RefreshIndex();
        }
    }
}
