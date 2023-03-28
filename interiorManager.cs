// InteriorManager.cs
using GTA;
using System.Threading.Tasks;

public class InteriorManager
{
    public async Task EnterWarehouse(Warehouse warehouse)
    {
        GTA.UI.Notification.Show("Loading interior IPL...");
        await IPLHelper.LoadIPLAsync(warehouse.InteriorIPLName);
        GTA.UI.Notification.Show("Interior IPL loaded. Teleporting player...");
        Game.Player.Character.Position = warehouse.InteriorLocation;
    }
}
