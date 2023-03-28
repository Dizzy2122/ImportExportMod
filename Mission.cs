// Mission.cs
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class ImportExportMission : Script
{

    private Vector3 _warehouseEntrance = new Vector3(861.7031f, -3208.131f, 5.993301f); // Replace this with the actual entrance location



    // Constants
    private const string IniFileName = "ImportExportMission.ini";
    // Fields
    private MenuPool _menuPool;
    private UIMenu _mainMenu;

    private ScriptSettings settings;
    private List<string> _addonCarModelNames;
    private List<string> _defaultCarModelNames;
    private List<string> _mixedCarModelNames;
    private int _carSetting;


    private List<StoredVehicle> _warehouseVehicles;

    List<Vector3> spawnLocations = new List<Vector3>
    {
        new Vector3(-255.3119f, -792.8151f, 31.52799f),
        new Vector3(403.4358f, -1626.484f, 28.29194f),
        new Vector3(861.7031f, -3208.131f, 5.993301f),
        // Add more locations as needed
    };

    public ImportExportMission()
    {
        // Load settings from the INI file
        settings = ScriptSettings.Load(IniFileName);
        LoadSettings();

        // Create the NativeUI menu
        _menuPool = new MenuPool();
        _mainMenu = new UIMenu("Import/Export Mission", "CHOOSE AN OPTION");
        _menuPool.Add(_mainMenu);

        SetupMenu();

        Tick += OnTick;
        Aborted += OnAborted;
    }

    private void SetupMenu()
    {
        UIMenuItem sourceCarItem = new UIMenuItem("Source a Car");
        _mainMenu.AddItem(sourceCarItem);
        _mainMenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == sourceCarItem)
            {
                StartSourceCarMission();
            }
        };

        UIMenuItem sellCarItem = new UIMenuItem("Sell a Car from Inventory");
        _mainMenu.AddItem(sellCarItem);
        _mainMenu.OnItemSelect += (sender, item, index) =>
        {
            if (item == sellCarItem)
            {
                StartSellCarMission();
            }
        };
    }

    private void LoadSettings()
    {
        // Read addon car model names from INI file
        _addonCarModelNames = settings.GetValue("ADDON_CARS", "CarModels", "").Split(',').ToList();

        // Read default car model names from INI file
        _defaultCarModelNames = settings.GetValue("DEFAULT_CARS", "CarModels", "").Split(',').ToList();

        // Combine both addon and default car model names for mixed list
        _mixedCarModelNames = _addonCarModelNames.Concat(_defaultCarModelNames).ToList();

        // Read the setting to use only addon cars, only GTA default cars, or a mix of both
        _carSetting = settings.GetValue("SETTINGS", "CarSetting", 2);

        // Debug notifications
        GTA.UI.Notification.Show($"Loaded {_addonCarModelNames.Count} addon car(s) from INI file.");
        GTA.UI.Notification.Show($"Loaded {_defaultCarModelNames.Count} default car(s) from INI file.");
}


    private void OnTick(object sender, EventArgs e)
{
    // Update the menu
    _menuPool.ProcessMenus();

    // Mission logic

    // Check if the player is near the warehouse entrance
    if (Game.Player.Character.Position.DistanceTo(_warehouseEntrance) < 5.0f)
    {
        SpawnStoredVehicles();
    }
}


    private void OnAborted(object sender, EventArgs e)
    {
    // Save data when the script is unloaded
    SaveWarehouseVehiclesData();
    }


    // Mission methods

    private void StartSourceCarMission()
    {
        // Start the car sourcing mission
    }

    private void StartSellCarMission()
    {
        // Start the car selling mission
    }

    // Helper methods

    private Vector3 GetRandomSpawnLocation()
{
    int randomIndex = new Random().Next(spawnLocations.Count);
    return spawnLocations[randomIndex];
}



private string GetRandomCarModel()
{
    List<string> carModelList;
    switch (_carSetting)
    {
        case 1: // Addon cars only
            carModelList = _addonCarModelNames;
            break;
        case 2: // Default cars only
            carModelList = _defaultCarModelNames;
            break;
        default: // Mixed (both addon and default cars)
            carModelList = _mixedCarModelNames;
            break;
    }

    if (carModelList.Count == 0) return null;

    int randomIndex = new Random().Next(carModelList.Count);
    return carModelList[randomIndex];
}



private void SpawnCarAtRandomLocation()
{
    string randomCarModel = GetRandomCarModel();
    if (randomCarModel != null)
    {
        Vector3 spawnLocation = GetRandomSpawnLocation();
        Vehicle spawnedVehicle = World.CreateVehicle(randomCarModel, spawnLocation);
    }
}




















public class StoredVehicle
{
    public string ModelName { get; set; }
    public Vector3 Location { get; set; }

    // Tuning options
    public int PrimaryColor { get; set; }
    public int SecondaryColor { get; set; }
    public int WheelColor { get; set; }
    public int PearlescentColor { get; set; }
    public int WindowTint { get; set; }
    public int WheelType { get; set; }
    public int FrontWheel { get; set; }
    public int BackWheel { get; set; }
    public bool CustomTires { get; set; }
    public int NeonsColor { get; set; }
    public int SmokeColor { get; set; }
    public bool XenonLights { get; set; }

    // Additional tuning options
    public int FrontBumper { get; set; }
    public int RearBumper { get; set; }
    public int SideSkirt { get; set; }
    public int Hood { get; set; }
    public int Exhaust { get; set; }
    public int Spoiler { get; set; }
    public int Roof { get; set; }
    public int Grille { get; set; }
    public int FrontWings { get; set; }
    public int RearWings { get; set; }
    public int FrontSeats { get; set; }
    public int RearSeats { get; set; }
    public int SteeringWheels { get; set; }
    public int Dashboard { get; set; }
    public int DialDesign { get; set; }
    public int Ornaments { get; set; }
    public int TrimDesign { get; set; }
    public int TrimColor { get; set; }
    public int DoorSpeakers { get; set; }
    public int RearSpeakers { get; set; }
    public int Trunk { get; set; }
    public int Hydraulics { get; set; }
    public int EngineBlock { get; set; }
    public int AirFilter { get; set; }
    public int StrutBrace { get; set; }
    public int CamCover { get; set; }
    public int Coilovers { get; set; }
    public int EngineTunes { get; set; }
    public int Brakes { get; set; }
    public int Transmission { get; set; }
    public int Suspension { get; set; }
    public int Armor { get; set; }
    public int Turbo { get; set; }

    public Dictionary<string, int> ModDict { get; set; }

    // Constructor
    public StoredVehicle(string modelName, Vector3 location)
    {
        ModelName = modelName;
        Location = location;

        ModDict = new Dictionary<string, int>();
    }

    // Load tuning options from a dictionary
    private void LoadTuningOptions(Vehicle vehicle, TuningOptions tuningOptions)
{
    // Performance
    vehicle.SetMod(VehicleMod.Engine, tuningOptions.engine);
    vehicle.SetMod(VehicleMod.Transmission, tuningOptions.transmission);
    vehicle.SetMod(VehicleMod.Suspension, tuningOptions.suspension);
    vehicle.SetMod(VehicleMod.Brakes, tuningOptions.brakes);

    // Lights
    vehicle.SetXenonLights(true, tuningOptions.headlights);
    vehicle.SetNeonLightsOn(VehicleNeonLight.Front, tuningOptions.neons[0]);
    vehicle.SetNeonLightsOn(VehicleNeonLight.Back, tuningOptions.neons[1]);
    vehicle.SetNeonLightsOn(VehicleNeonLight.Left, tuningOptions.neons[2]);
    vehicle.SetNeonLightsOn(VehicleNeonLight.Right, tuningOptions.neons[3]);
    vehicle.SetNeonColor(tuningOptions.neonColor);

    // Exterior
    vehicle.SetMod(VehicleMod.FrontBumper, tuningOptions.frontBumper);
    vehicle.SetMod(VehicleMod.RearBumper, tuningOptions.rearBumper);
    vehicle.SetMod(VehicleMod.SideSkirt, tuningOptions.sideSkirt);
    vehicle.SetMod(VehicleMod.Exhaust, tuningOptions.exhaust);
    vehicle.SetMod(VehicleMod.Frame, tuningOptions.frame);
    vehicle.SetMod(VehicleMod.Grille, tuningOptions.grille);
    vehicle.SetMod(VehicleMod.Hood, tuningOptions.hood);
    vehicle.SetMod(VehicleMod.Fender, tuningOptions.fender);
    vehicle.SetMod(VehicleMod.RightFender, tuningOptions.rightFender);
    vehicle.SetMod(VehicleMod.Roof, tuningOptions.roof);
    vehicle.SetMod(VehicleMod.EngineBay, tuningOptions.engineBay);
    vehicle.SetMod(VehicleMod.Trunk, tuningOptions.trunk);
    vehicle.SetMod(VehicleMod.Hydraulics, tuningOptions.hydraulics);
    vehicle.SetMod(VehicleMod.PlateHolder, tuningOptions.plateHolder);
    vehicle.SetMod(VehicleMod.VanityPlates, tuningOptions.vanityPlates);
    vehicle.SetMod(VehicleMod.TrimDesign, tuningOptions.trimDesign);
    vehicle.SetMod(VehicleMod.Ornaments, tuningOptions.ornaments);
    vehicle.SetMod(VehicleMod.Dashboard, tuningOptions.dashboard);
    vehicle.SetMod(VehicleMod.DialDesign, tuningOptions.dials);
    vehicle.SetMod(VehicleMod.DoorSpeakers, tuningOptions.doorSpeaker);
    vehicle.SetMod(VehicleMod.Seats, tuningOptions.seats);
    vehicle.SetMod(VehicleMod.SteeringWheels, tuningOptions.steeringWheels);
    vehicle.SetMod(VehicleMod.ColumnShifterLevers, tuningOptions.columnShifterLevers);
    vehicle.SetMod(VehicleMod.Plaques, tuningOptions.plaque);
    vehicle.SetMod(VehicleMod.Speakers, tuningOptions.speakers);
    vehicle.SetMod(VehicleMod.TrunkSpeakers, tuningOptions.trunk);
    vehicle.SetMod(VehicleMod.Hydraulics2, tuningOptions.hydraulics);
    vehicle.SetMod(VehicleMod.EngineBlock, tuningOptions.engineBlock);
    vehicle.SetMod(VehicleMod.AirFilter, tuningOptions.airFilter);
    vehicle.SetMod(VehicleMod.Struts, tuningOptions.struts);
    vehicle.SetMod(VehicleMod.Tank, tuningOptions.tank);
    vehicle.SetMod(VehicleMod.Windows, tuningOptions.windows);
    vehicle.SetLivery(tuningOptions.livery);

    // Wheels
vehicle.SetMod(VehicleMod.FrontWheels, tuningOptions.frontWheels);
vehicle.SetMod(VehicleMod.RearWheels, tuningOptions.rearWheels);
vehicle.SetMod(VehicleMod.WheelType, tuningOptions.wheelType);
vehicle.SetMod(VehicleMod.WheelVariation, tuningOptions.wheelVariation);
vehicle.SetCustomTires(tuningOptions.customTires);
vehicle.SetTireSmokeColor(tuningOptions.tireSmokeColor);

// Extras
vehicle.SetMod(VehicleMod.Trim, tuningOptions.trim);
vehicle.SetMod(VehicleMod.Armor, tuningOptions.armor);
vehicle.SetMod(VehicleMod.Turbo, tuningOptions.turbo);
vehicle.ToggleMod(VehicleToggleMod.TireSmoke, tuningOptions.tireSmoke);
vehicle.ToggleMod(VehicleToggleMod.XenonHeadlights, tuningOptions.xenonHeadlights);

// Other settings
vehicle.NumberPlate = tuningOptions.numberPlate;
vehicle.SetPrimaryColor(tuningOptions.primaryColor);
vehicle.SetSecondaryColor(tuningOptions.secondaryColor);
vehicle.SetPearlescentColor(tuningOptions.pearlescentColor);
vehicle.SetWheelColor(tuningOptions.wheelColor);
vehicle.SetDashboardColor(tuningOptions.dashboardColor);
vehicle.SetTrimColor(tuningOptions.trimColor);
vehicle.SetRimColor(tuningOptions.rimColor);
vehicle.SetModColor1(tuningOptions.modColor1);
vehicle.SetModColor2(tuningOptions.modColor2);
vehicle.SetCustomPrimaryColor(tuningOptions.customPrimaryColor);
vehicle.SetCustomSecondaryColor(tuningOptions.customSecondaryColor);
}



private void LoadWarehouseVehiclesData()
{
    string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WarehouseVehicles.json");

    if (File.Exists(jsonFilePath))
    {
        string json = File.ReadAllText(jsonFilePath);
        _warehouseVehicles = JsonConvert.DeserializeObject<List<StoredVehicle>>(json);
    }
    else
    {
        _warehouseVehicles = new List<StoredVehicle>();
    }
}

private void SaveWarehouseVehiclesData()
{
    string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WarehouseVehicles.json");
    string json = JsonConvert.SerializeObject(_warehouseVehicles, Formatting.Indented);
    File.WriteAllText(jsonFilePath, json);
}


private void SpawnStoredVehicles()
{
    foreach (StoredVehicle storedVehicle in _warehouseVehicles)
    {
        Vehicle spawnedVehicle = World.CreateVehicle(storedVehicle.ModelName, storedVehicle.Location);
        spawnedVehicle.IsPersistent = true;
    }
}



}
