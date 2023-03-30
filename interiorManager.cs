// InteriorManager.cs
using GTA;
using System.Threading.Tasks;

public class InteriorManager

{
    private GTA.Math.Vector3 _initialEntryPoint;
    private Warehouse _enteredWarehouse;
    public async Task EnterWarehouse(Warehouse warehouse)
    {
        _initialEntryPoint = Game.Player.Character.Position;
        _enteredWarehouse = warehouse;
        GTA.UI.Notification.Show("Loading interior IPL...");
        await IPLHelper.LoadIPLAsync(warehouse.InteriorIPLName);
        GTA.UI.Notification.Show("Interior IPL loaded. Teleporting player...");
        Game.Player.Character.Position = warehouse.InteriorLocation;
    }

    public async Task ExitWarehouse()
{
    if (_enteredWarehouse != null)
    {
        GTA.UI.Notification.Show("Unloading interior IPL...");
        await IPLHelper.UnloadIPLAsync(_enteredWarehouse.InteriorIPLName); // Use _enteredWarehouse.InteriorIPLName
        GTA.UI.Notification.Show("Interior IPL unloaded.");
    }
}


    public GTA.Math.Vector3 GetInitialEntryPoint()
    {
        return _initialEntryPoint;
    }
}

