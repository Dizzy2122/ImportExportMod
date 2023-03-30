// MissionMenu.cs
using GTA;
using NativeUI;
using System;
using System.Windows.Forms;

public class MissionMenu : Script
{

    private MenuHandler _menuHandler;
    private MenuPool _menuPool;
    private UIMenu _testMenu;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;

    public MissionMenu(MenuHandler menuHandler, InteriorManager interiorManager)
    {
         _menuHandler = menuHandler;

        GTA.UI.Notification.Show("MissionMenu constructor called");

        _menuPool = new MenuPool();
        _testMenu = new UIMenu("Test Menu", "TEST MENU OPTIONS");
        _menuPool.Add(_testMenu);

        UIMenuItem stealCarMission = new UIMenuItem("Steal a Car");
        UIMenuItem sellWarehouseVehicle = new UIMenuItem("Sell Warehouse Vehicle");
        UIMenuItem exitWarehouseItem = new UIMenuItem("Exit Warehouse");
        _testMenu.AddItem(stealCarMission);
        _testMenu.AddItem(sellWarehouseVehicle);
        _testMenu.AddItem(exitWarehouseItem);

        _testMenu.OnItemSelect += OnItemSelect;

        this.Tick += OnTick;
    }

    private async void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
{
    if (selectedItem == sender.MenuItems[index])
    {
        if (index == 0)
        {
            // Steal a car mission
            MissionScript missionScript = new MissionScript();
            missionScript.StartStealCarMission();
        }
        else if (index == 1)
        {
            // Sell warehouse vehicle
            // Your code for selling warehouse vehicle here
        }
        else if (index == 2)
        {
            // Exit warehouse
            GTA.Math.Vector3 initialEntryPoint = _interiorManager.GetInitialEntryPoint();
            GTA.UI.Notification.Show($"Teleporting player back to: {initialEntryPoint}"); // Debug notification
            Game.Player.Character.Position = initialEntryPoint;
            await _interiorManager.ExitWarehouse();
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
            _testMenu.Visible = true;
            GTA.UI.Notification.Show("Near laptop. Menu should be visible."); // Debug notification
        }
        else
        {
            _testMenu.Visible = false;
            GTA.UI.Notification.Show("Away from laptop. Menu should be hidden."); // Debug notification
        }
    }
}
