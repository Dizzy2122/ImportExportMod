// InteriorManager.cs
using GTA;
using System.Threading.Tasks;

public class InteriorManager
{
    private GTA.Math.Vector3 _initialEntryPoint;

    public async Task EnterWarehouse(Warehouse warehouse)
    {
        _initialEntryPoint = Game.Player.Character.Position;
        GTA.UI.Notification.Show("Loading interior IPL...");
        await IPLHelper.LoadIPLAsync(warehouse.InteriorIPLName);
        GTA.UI.Notification.Show("Interior IPL loaded. Teleporting player...");
        Game.Player.Character.Position = warehouse.InteriorLocation;
    }

    public GTA.Math.Vector3 GetInitialEntryPoint()
    {
        return _initialEntryPoint;
    }
}
