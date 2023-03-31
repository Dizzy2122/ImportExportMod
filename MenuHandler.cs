using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class MenuHandler : Script
{
    private Warehouse _currentWarehouse;
    private UIMenu _mainMenu;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;
    private MenuPool _menuPool;
    private List<Warehouse> _availableWarehouses;
    private Dictionary<UIMenuItem, Warehouse> _itemToWarehouseMapping;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;

    public MenuHandler(InteriorManager interiorManager, ImportExportMod importExportMod, List<Warehouse> availableWarehouses, List<Warehouse> ownedWarehouses)
    {
        _interiorManager = interiorManager;
        _importExportMod = importExportMod;
        _availableWarehouses = availableWarehouses;
        _menuPool = new MenuPool();
        InitializeMenu();
        SetupMainMenuItems(availableWarehouses, ownedWarehouses);

        // Subscribe to the Tick event
        this.Tick += OnTick;
    }

    private void InitializeMenu()
    {
        
        _mainMenu = new UIMenu("Warehouses", "WAREHOUSE OPTIONS");
        _menuPool.Add(_mainMenu);

        _itemToWarehouseMapping = new Dictionary<UIMenuItem, Warehouse>();

        foreach (var warehouse in _availableWarehouses)
        {
            var warehouseItem = new UIMenuItem(warehouse.Name);
            _itemToWarehouseMapping[warehouseItem] = warehouse;
            _mainMenu.AddItem(warehouseItem);
        }

        _mainMenu.OnItemSelect += MainMenu_OnItemSelect;
    }

    private async void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {
        if (selectedItem == sender.MenuItems[index])
        {
            if (index == 0)
            {
                // Purchase warehouse logic
                _importExportMod.PurchaseWarehouse();
            }
            else if (index == 1)
            {
                // Enter warehouse logic
                if (_itemToWarehouseMapping.ContainsKey(selectedItem))
                {
                    await _importExportMod.EnterWarehouse(_itemToWarehouseMapping[selectedItem]);
                }
                else
                {
                    GTA.UI.Notification.Show("No warehouse selected.");
                }
            }
        }
    }

    private void OnTick(object sender, EventArgs e)
{
    _menuPool.ProcessMenus();

    Ped playerPed = Game.Player.Character;

    // Check if the player is near any warehouse
    foreach (var warehouse in _availableWarehouses)
    {
        float distanceToWarehouse = playerPed.Position.DistanceTo(warehouse.ExteriorLocation);

        if (distanceToWarehouse <= _interactionDistance)
        {
            _currentWarehouse = warehouse;
            _mainMenu.Visible = true;
            GTA.UI.Notification.Show($"Near {warehouse.Name}. Menu should be visible. Distance: {distanceToWarehouse}"); // Debug notification
            break;
        }
        else
        {
            _currentWarehouse = null;
            _mainMenu.Visible = false;
            // GTA.UI.Notification.Show($"Away from {warehouse.Name}. Menu should be hidden. Distance: {distanceToWarehouse}"); // Debug notification
        }
    }
}




        private void SetupMainMenuItems(List<Warehouse> availableWarehouses, List<Warehouse> ownedWarehouses)
    {
        UIMenuItem purchaseWarehouseItem = new UIMenuItem("Purchase Warehouse");
        UIMenuItem enterWarehouseItem = new UIMenuItem("Enter Warehouse");
        _mainMenu.AddItem(purchaseWarehouseItem);
        _mainMenu.AddItem(enterWarehouseItem);
    }
}
    
