// ImportExportMod.cs
using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using NativeUI;
using System.Linq;
using System.Threading.Tasks;


public class ImportExportMod : Script
{
    private MissionMenu missionMenu;
    private InteriorManager interiorManager;
    private Warehouse enteredWarehouse;
    private UIMenu mainMenu;
    private UIMenu exitInteriorMenu;
    private List<Warehouse> availableWarehouses;
    private List<Warehouse> ownedWarehouses;
    private Warehouse currentWarehouse;
    private UIMenuItem purchaseWarehouseItem;
    private UIMenuItem enterWarehouseItem;
    private UIMenuItem sellWarehouseItem;
    private MenuPool menuPool;
    private List<string> addonCarModelNames;
    private ScriptSettings settings;
    private int carSetting;

    public ImportExportMod(InteriorManager interiorManager)
    {

        GTA.UI.Notification.Show("ImportExportMod constructor called");

        interiorManager = new InteriorManager();
        missionMenu = new MissionMenu(interiorManager);



        settings = ScriptSettings.Load("ImportExportMod.ini");
        LoadSettings();

        // Interior Setup
        interiorManager = new InteriorManager();

        // Initialize available and owned warehouses
        availableWarehouses = new List<Warehouse>();
        ownedWarehouses = new List<Warehouse>();

        // Initialize the menu pool and main menu
        menuPool = new MenuPool();
        mainMenu = new UIMenu("Warehouse Manager", "Select an option:");
        
        
        menuPool.Add(mainMenu);

        // Create menu items
        SetupWarehouseMenuItems();

        // Add warehouses to the availableWarehouses list
        WarehouseInitializer.InitializeWarehouses(availableWarehouses);
        LoadOwnedWarehouses();
        SetupExitInteriorMenu();

        // Subscribe to Tick event
        Tick += OnTick;

        // Handle menu input
        this.KeyDown += (o, e) => menuPool.ProcessKey(e.KeyCode);
    }


    private void OnTick(object sender, EventArgs e)
    {
        CheckPlayerInteractionWithWarehouses();
        CheckPlayerInteractionWithExitPoint();
        menuPool.ProcessMenus();
        this.KeyDown += (o, e) => menuPool.ProcessKey(e.KeyCode);
    }

    private void CheckPlayerInteractionWithWarehouses()
    {
        var player = Game.Player.Character;
        currentWarehouse = null;
        bool isCloseToAnyWarehouse = false;

        foreach (var warehouse in availableWarehouses)
        {
            if (player.Position.DistanceTo(warehouse.ExteriorLocation) < 5.0f)
            {
                isCloseToAnyWarehouse = true;
                currentWarehouse = warehouse;
                break;
            }
        }

        if (isCloseToAnyWarehouse)
        {
            if (!mainMenu.Visible)
            {
                mainMenu.Visible = true;
                UpdateMenuOptions();
            }
        }
        else
        {
            mainMenu.Visible = false;
        }
    }

    private void SetupWarehouseMenuItems()
    {
        sellWarehouseItem = new UIMenuItem("Sell Warehouse");
        purchaseWarehouseItem = new UIMenuItem("Purchase Warehouse");
        enterWarehouseItem = new UIMenuItem("Enter Warehouse");

        mainMenu.AddItem(sellWarehouseItem);
        mainMenu.AddItem(purchaseWarehouseItem);
        mainMenu.AddItem(enterWarehouseItem);

        // Attach the event handler for item selection
        mainMenu.OnItemSelect += MainMenu_OnItemSelect;

        // Set the initial states for the menu items
        purchaseWarehouseItem.Enabled = false;
        enterWarehouseItem.Enabled = false;
        sellWarehouseItem.Enabled = false;

        // Set the main menu to be initially invisible
        mainMenu.Visible = false;
    }

    private async void MainMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
{
    if (currentWarehouse != null)
    {
        if (selectedItem == purchaseWarehouseItem)
        {
            PurchaseWarehouse();
        }
        else if (selectedItem == enterWarehouseItem)
        {
            // Apply the 'await' operator and call EnterWarehouse with the currentWarehouse parameter
            await interiorManager.EnterWarehouse(currentWarehouse);
        }
        else if (selectedItem == sellWarehouseItem)
        {
            SellWarehouse();
        }
    }
}


    private void UpdateMenuOptions()
    {
                if (currentWarehouse != null)
        {
            if (!ownedWarehouses.Contains(currentWarehouse))
            {
                purchaseWarehouseItem.Enabled = true;
                enterWarehouseItem.Enabled = false;
                sellWarehouseItem.Enabled = false;
                SetSelectedMenuItem(mainMenu, purchaseWarehouseItem);
            }
            else
            {
                purchaseWarehouseItem.Enabled = false;
                enterWarehouseItem.Enabled = true;
                sellWarehouseItem.Enabled = true;
                SetSelectedMenuItem(mainMenu, enterWarehouseItem);
            }
        }
    }

    private void SetSelectedMenuItem(UIMenu menu, UIMenuItem item)
    {
        int index = menu.MenuItems.IndexOf(item);
        while (menu.CurrentSelection != index)
        {
            menu.GoDown();
        }
    }

    private void SetupExitInteriorMenu()
    {
        exitInteriorMenu = new UIMenu("Exit Warehouse", "Select an option:");
        menuPool.Add(exitInteriorMenu);

        UIMenuItem exitWarehouseItem = new UIMenuItem("Exit Warehouse");
        exitInteriorMenu.AddItem(exitWarehouseItem);
        exitInteriorMenu.OnItemSelect += ExitMenu_OnItemSelect;
        exitInteriorMenu.Visible = false;
    }

    private void ExitMenu_OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {
        if (selectedItem.Text == "Exit Warehouse")
        {
            Game.Player.Character.Position = enteredWarehouse.ExteriorLocation;
            exitInteriorMenu.Visible = false;
            enteredWarehouse = null;
        }
    }

    private void CheckPlayerInteractionWithExitPoint()
    {
        if (enteredWarehouse != null)
        {
            var player = Game.Player.Character;
            if (player.Position.DistanceTo(enteredWarehouse.InteriorLocation) < 5.0f)
            {
                if (!exitInteriorMenu.Visible)
                {
                    exitInteriorMenu.Visible = true;
                }
            }
            else
            {
                exitInteriorMenu.Visible = false;
            }
        }
    }

    public async Task EnterWarehouse(Warehouse warehouse)
{
    GTA.UI.Notification.Show("Loading interior IPL...");
    await IPLHelper.LoadIPLAsync(warehouse.InteriorIPLName);
    GTA.UI.Notification.Show("Interior IPL loaded.");

    // Spawn a temporary invisible car
    Vehicle tempCar = World.CreateVehicle(warehouse.TempCarModel, warehouse.InteriorLocation);

    tempCar.IsVisible = false;
    tempCar.IsPersistent = false;
    Game.Player.Character.SetIntoVehicle(tempCar, VehicleSeat.Driver);

    // Add a delay to give the game some time to load the interior properly
    await Delay(5000);

    // Teleport the player to the interior location
    GTA.UI.Notification.Show("Teleporting player...");
    tempCar.Position = warehouse.InteriorLocation;

    // Add another delay to make sure the player is in the correct position
    await Delay(2000);

    // Remove the player from the temporary car and delete the car
    Game.Player.Character.Task.LeaveVehicle();
    tempCar.Delete();
    WarehouseInitializer.LoadInteriorProps(warehouse);
}





private async Task Delay(int milliseconds)
{
    await Task.Delay(milliseconds);
}


public void LoadSettings()
    {
        // Read addon car model names from INI file
        addonCarModelNames = settings.GetValue("CARS", "AddonCarModelNames", new List<string>()).ToList();

        // Read the setting to use only addon cars, only GTA default cars, or a mix of both
        carSetting = settings.GetValue<int>("SETTINGS", "CarSetting", 0);

        // Other initialization code goes here
    }



    // Add your other methods such as PurchaseWarehouse, SellWarehouse, and LoadOwnedWarehouses.













































    private void PurchaseWarehouse()
    {
        if (currentWarehouse == null)
        {
            GTA.UI.Notification.Show("Current warehouse is null.");
            return;
        }

        if (Game.Player.Money >= currentWarehouse.Price)
        {
            Game.Player.Money -= currentWarehouse.Price;
            ownedWarehouses.Add(currentWarehouse);
            GTA.UI.Notification.Show($"You have purchased {currentWarehouse.Name}.");
            UpdateMenuOptions();
        }
        else
        {
            GTA.UI.Notification.Show("You do not have enough money to purchase this warehouse.");
    }
}


private void SellWarehouse()
{
    if (currentWarehouse == null)
    {
        GTA.UI.Notification.Show("Current warehouse is null.");
        return;
    }

    if (ownedWarehouses.Contains(currentWarehouse))
    {
        int sellPrice = (int)(currentWarehouse.Price * 0.75); // Adjust the selling price percentage as desired
        Game.Player.Money += sellPrice;
        ownedWarehouses.Remove(currentWarehouse);
        GTA.UI.Notification.Show($"You have sold {currentWarehouse.Name} for ${sellPrice}.");
        UpdateMenuOptions();
    }
    else
    {
        GTA.UI.Notification.Show("You do not own this warehouse.");
    }
}





private void SaveOwnedWarehouses()
{
    ScriptSettings config = ScriptSettings.Load("ImportExportMod.ini");
    config.SetValue("Warehouses", "OwnedWarehouseCount", ownedWarehouses.Count);
    int warehouseIndex = 0;

    foreach (Warehouse warehouse in ownedWarehouses)
    {
        config.SetValue("Warehouses", $"OwnedWarehouse{warehouseIndex}", warehouse.Name);
        warehouseIndex++;
    }

    config.Save();
}

private void LoadOwnedWarehouses()
{
    ScriptSettings config = ScriptSettings.Load("ImportExportMod.ini");
    int ownedWarehouseCount = config.GetValue<int>("Warehouses", "OwnedWarehouseCount", 0);

    for (int i = 0; i < ownedWarehouseCount; i++)
    {
        string warehouseName = config.GetValue<string>("Warehouses", $"OwnedWarehouse{i}", "");

        // Find the warehouse with the matching name
        Warehouse warehouse = availableWarehouses.FirstOrDefault(w => w.Name == warehouseName);
        
        if (warehouse != null)
        {
            ownedWarehouses.Add(warehouse);
        }
    }
}

}


