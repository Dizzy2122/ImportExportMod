// IPLHelper.cs
using GTA.Native;
using System.Threading.Tasks;

public static class IPLHelper
{
    public static async Task LoadIPLAsync(string iplName)
    {
        GTA.UI.Notification.Show($"Requesting IPL: {iplName}...");
        Function.Call(Hash.REQUEST_IPL, iplName);

        int attempts = 0;
        while (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, iplName))
        {
            attempts++;
            if (attempts % 100 == 0)
            {
                GTA.UI.Notification.Show($"Still waiting for IPL {iplName} to load (Attempt {attempts})...");
            }
            await Task.Delay(10);
        }

        GTA.UI.Notification.Show($"IPL {iplName} is now active.");
    }
}
