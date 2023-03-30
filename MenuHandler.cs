// MenuHandler.cs
using GTA;
using NativeUI;
using System;
using System.Windows.Forms;

public class MenuHandler : Script
{
    private MenuPool _menuPool;
    private UIMenu _testMenu;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;

    public MenuHandler()
    {
        GTA.UI.Notification.Show("MenuHandler constructor called");

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
