// Mission.cs
using GTA;
using NativeUI;
using System;

public class MissionScript : Script
{
    private MenuPool _menuPool;
    private UIMenu _mainMenu;

    public MissionScript()
    {
        _menuPool = new MenuPool();
        _mainMenu = new UIMenu("Mission", "CHOOSE A MISSION");
        _menuPool.Add(_mainMenu);

        UIMenuItem stealCarMission = new UIMenuItem("Steal a car");
        _mainMenu.AddItem(stealCarMission);
        _mainMenu.OnItemSelect += OnItemSelect;

        Tick += OnTick;
    }

    private void OnItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
    {
        if (selectedItem == sender.MenuItems[index])
        {
            // Start the mission to steal a car
            StartStealCarMission();
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        _menuPool.ProcessMenus();

        // Check if a specific key is pressed (for example, the 'N' key)
        if (Game.IsControlJustPressed(Control.ReplayStartStopRecordingSecondary))
        {
            StartStealCarMission();            
        }
    }


private void StartStealCarMission()
{
    // Define car model names and spawn locations
    string[] carModels = { "adder", "zentorno", "t20" };
    GTA.Math.Vector3[] spawnLocations = {
        new GTA.Math.Vector3(-1000.0f, -800.0f, 13.0f),
        new GTA.Math.Vector3(600.0f, 600.0f, 90.0f),
        new GTA.Math.Vector3(-150.0f, 1200.0f, 55.0f),
    };

    // Choose a random car model and spawn location
    Random random = new Random();
    string carModel = carModels[random.Next(carModels.Length)];
    GTA.Math.Vector3 spawnLocation = spawnLocations[random.Next(spawnLocations.Length)];

    // Spawn the car
    Vehicle vehicle = World.CreateVehicle(carModel, spawnLocation);
    vehicle.PlaceOnNextStreet();

    // Set a blip for the car
    Blip carBlip = vehicle.AddBlip();
    carBlip.Sprite = BlipSprite.PersonalVehicleCar;
    carBlip.Color = BlipColor.Yellow;
    carBlip.IsShortRange = true;
}


private bool IsVehicleInWarehouse(Vehicle vehicle)
{
    GTA.Math.Vector3 warehouseLocation = new GTA.Math.Vector3(200.0f, 200.0f, 30.0f); // Replace with your warehouse location
    float distanceToWarehouse = vehicle.Position.DistanceTo(warehouseLocation);
    float warehouseRadius = 10.0f; // Adjust this value based on the size of your warehouse

    return distanceToWarehouse <= warehouseRadius;
}
}
    
