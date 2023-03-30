// MenuHandler.cs
using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
public class MenuHandler : Script
{
    private MissionMenu _missionMenu;
    private UIMenu _mainMenu;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;
    private MenuPool _menuPool;
    private UIMenu _testMenu;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;

    public MenuHandler(InteriorManager interiorManager, List<Warehouse> availableWarehouses, List<Warehouse> ownedWarehouses)
    {

        
        GTA.UI.Notification.Show("MenuHandler constructor called");

        _interiorManager = interiorManager;
        _importExportMod = importExportMod;

        InitializeMenu();
        _menuPool = new MenuPool();

        // Set up the main exterior menu
        _mainMenu = new UIMenu("Warehouse Manager", "Select an option:");
        _menuPool.Add(_mainMenu);

        // Set up the interior mission menu
        _testMenu = new UIMenu("Test Menu", "TEST MENU OPTIONS");
        _menuPool.Add(_testMenu);

        // Initialize the mission menu
        _missionMenu = new MissionMenu(this, interiorManager);

        // Add main menu items
        SetupMainMenuItems(availableWarehouses, ownedWarehouses);

        // Subscribe to the Tick event
        this.Tick += OnTick;

        // Handle menu input
        this.KeyDown += (o, e) => _menuPool.ProcessKey(e.KeyCode);
    }

        private async void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
{
    if (selectedItem == sender.MenuItems[index])
    {
        if (index == 0)
        {
            // Purchase warehouse logic
            await _importExportMod.PurchaseWarehouse();
        }
        else if (index == 1)
        {
            // Enter warehouse logic
            Warehouse warehouse = ...; // Get the selected warehouse from available or owned warehouses
            await _importExportMod.EnterWarehouse(warehouse);
        }
    }
}




    private void SetupMainMenuItems(List<Warehouse> availableWarehouses, List<Warehouse> ownedWarehouses)
    {
        // Add menu items for the main exterior menu
        // You can copy the code from the ImportExportMod class where you set up the main menu items
    }

    private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {
        // Your mission logic
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (_testMenu.Visible)
    {
        if (Game.IsControlJustPressed(GTA.Control.FrontendUp))
        {
            _testMenu.GoUp();
        }
        if (Game.IsControlJustPressed(GTA.Control.FrontendDown))
        {
            _testMenu.GoDown();
        }
        if (Game.IsControlJustPressed(GTA.Control.FrontendAccept))
        {
            _testMenu.SelectItem();
        }
        if (Game.IsControlJustPressed(GTA.Control.FrontendCancel))
        {
            _testMenu.GoBack();
        }
    }

        _menuPool.ProcessMenus();

        Ped playerPed = Game.Player.Character;
        float distanceToLaptop = playerPed.Position.DistanceTo(_laptopLocation);

        if (distanceToLaptop <= _interactionDistance)
        {
            _testMenu.Visible = true;
            GTA.UI.Notification.Show("Near laptop. Menu should be visible."); // Debug notification
        }
        else
        {
            _testMenu.Visible = false;
            GTA.UI.Notification.Show("Away from laptop. Menu should be hidden."); // Debug notification
        }
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (_testMenu.Visible)
        {
            _menuPool.ProcessKey(e.KeyCode);
        }
    }

}
