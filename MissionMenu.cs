// MissionMenu.cs
using GTA;
using NativeUI;
using System;
using System.Windows.Forms;

public class MissionMenu : Script
{
    private MenuPool _menuPool;
    private UIMenu _testMenu;
    private MissionScript _missionScript;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;

    public MissionMenu()
    {
        _missionScript = new MissionScript();

        _menuPool = new MenuPool();
        _testMenu = new UIMenu("Warehouse", "WAREHOUE OPTIONS");
        _menuPool.Add(_testMenu);

        UIMenuItem stealCarMission = new UIMenuItem("Source a Car");
        UIMenuItem sellWarehouseVehicle = new UIMenuItem("Sell Warehouse Vehicle");
        _testMenu.AddItem(stealCarMission);
        _testMenu.AddItem(sellWarehouseVehicle);

        _testMenu.OnItemSelect += OnItemSelect;

        Tick += OnTick;
    }

    private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {
        if (selectedItem == sender.MenuItems[index])
        {
            if (index == 0)
            {
                _missionScript.StartStealCarMission();
            }
            else if (index == 1)
            {
                // Sell a warehouse vehicle
                GTA.UI.Notification.Show("Selling a warehouse vehicle.");
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
        }
        else
        {
            _testMenu.Visible = false;
        }
    }
}

