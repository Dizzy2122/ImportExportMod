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

    public MenuHandler(InteriorManager interiorManager, ImportExportMod importExportMod, List<Warehouse> availableWarehouses)
    {
        _interiorManager = interiorManager;
        _importExportMod = importExportMod;
        _availableWarehouses = availableWarehouses;

        InitializeMenu();

        // Subscribe to the Tick event
        this.Tick += OnTick;
    }

    private void InitializeMenu()
    {
        _menuPool = new MenuPool();
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
        float distanceToLaptop = playerPed.Position.DistanceTo(_laptopLocation);

        if (distanceToLaptop <= _interactionDistance)
        {
            _mainMenu.Visible = true;
            GTA.UI.Notification.Show("Near laptop. Menu should be visible."); // Debug notification
        }
        else
        {
            _mainMenu.Visible = false;
            GTA.UI.Notification.Show("Away from laptop. Menu should be hidden."); // Debug notification
        }
    }
}
