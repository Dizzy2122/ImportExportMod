// MissionMenu.cs
using GTA;
using NativeUI;
using System;
using System.Windows.Forms;

public class MissionMenu : Script
{
      private MenuPool _menuPool;
    private UIMenu _testMenu;
    private GTA.Math.Vector3 _laptopLocation = new GTA.Math.Vector3(964.9951f, -3003.473f, -39.63989f);
    private float _interactionDistance = 2.0f;
    private InteriorManager _interiorManager;
    public MissionMenu()
{

    _interiorManager = new InteriorManager();


    _menuPool = new MenuPool();
    _testMenu = new UIMenu("Test Menu", "TEST MENU OPTIONS");
    _menuPool.Add(_testMenu);

    UIMenuItem stealCarMission = new UIMenuItem("Steal a Car");
    UIMenuItem sellWarehouseVehicle = new UIMenuItem("Sell Warehouse Vehicle");
    _testMenu.AddItem(stealCarMission);
    _testMenu.AddItem(sellWarehouseVehicle);

    _testMenu.OnItemSelect += OnItemSelect;

    Tick += OnTick;

    this.KeyDown += (o, e) => _menuPool.ProcessKey(e.KeyCode);
}


    private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
{
    if (selectedItem == sender.MenuItems[index])
    {
        if (index == 0)
        {
            MissionScript missionScript = new MissionScript();
            missionScript.StartStealCarMission();
            Game.Player.Character.Position = _interiorManager.GetInitialEntryPoint();
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
        }
        else
        {
            _testMenu.Visible = false;
        }
    }
}
