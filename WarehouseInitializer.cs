// WarehouseInitializer.cs
using GTA;
using GTA.Native;
using System.Collections.Generic;

public static class WarehouseInitializer
{


    public static void InitializeWarehouses(List<Warehouse> availableWarehouses)
    {
        GTA.Math.Vector3 commonInteriorLocation = new GTA.Math.Vector3(971.0275f, -2987.564f, -39.64695f);
        GTA.Math.Vector3 commonLaptopLocation = new GTA.Math.Vector3(-85.0f, -825.0f, 32.0f);
        GTA.Math.Vector3 commonInteriorExitLocation = new GTA.Math.Vector3(-80.0f, -815.0f, 32.0f);
        LoadGenericIPLs();
        string commonInteriorIPLName = "imp_impexp_interior_placement_interior_1_impexp_intwaremed_milo_";

    Warehouse elysianIslandWarehouse = new Warehouse(
        new GTA.Math.Vector3(144.3558f, -3004.987f, 7.030922f),
        "Elysian Island Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(144.3558f, -3004.987f, 9.030922f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(elysianIslandWarehouse);

    Warehouse cypressFlatsWarehouse = new Warehouse(
        new GTA.Math.Vector3(804.5468f, -2220.445f, 29.44725f),
        "Cypress Flats Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(804.5468f, -2220.445f, 31.44725f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(cypressFlatsWarehouse);

    Warehouse murrietaHeightsWarehouse = new Warehouse(
        new GTA.Math.Vector3(1211.428f, -1262.586f, 35.22675f),
        "Murrieta Heights Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(1211.428f, -1262.586f, 37.22675f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(murrietaHeightsWarehouse);

    Warehouse elBuroHeightsWarehouse = new Warehouse(
        new GTA.Math.Vector3(1764.536f, -1647.494f, 112.6444f),
        "El Buro Heights Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(1764.536f, -1647.494f, 114.6444f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(elBuroHeightsWarehouse);

    Warehouse davisStreetWarehouse = new Warehouse(
        new GTA.Math.Vector3(-71.89099f, -1821.3256f, 26.94197f),
        "Davis Street Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(-71.89099f, -1821.3256f, 28.94197f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(davisStreetWarehouse);

    Warehouse losSantosIntlAirportWarehouse = new Warehouse(
        new GTA.Math.Vector3(-1152.891f, -2173.466f, 13.26305f),
        "Los Santos International Airport Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(-1152.891f, -2173.466f, 15.26305f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(losSantosIntlAirportWarehouse);

    Warehouse losSantosIntlAirportWarehouse2 = new Warehouse(
        new GTA.Math.Vector3(-513.2588f, -2199.715f, 6.394024f),
        "Los Santos International Airport Warehouse 2",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(-513.2588f, -2199.715f, 8.394024f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(losSantosIntlAirportWarehouse2);

    Warehouse laPuertaWarehouse = new Warehouse(
        new GTA.Math.Vector3(-636.0688f, -1774.854f, 24.0514f),
        "La Puerta Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(-636.0688f, -1774.854f, 26.0514f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(laPuertaWarehouse);

    Warehouse laMesaWarehouse = new Warehouse(
        new GTA.Math.Vector3(998.7968f, -1855.474f, 31.03981f),
        "La Mesa Warehouse",
        0, // remember to replace when script is final
        commonInteriorExitLocation,
        commonInteriorIPLName,
        commonInteriorLocation,
        commonLaptopLocation,
        new GTA.Math.Vector3(998.7968f, -1855.474f, 33.03981f),
        commonInteriorExitLocation,
        (uint)VehicleHash.Faggio,
        commonInteriorIPLName
    );
    availableWarehouses.Add(laMesaWarehouse);


         // Create blips for each warehouse
        foreach (var warehouse in availableWarehouses)
        {
            Blip warehouseBlip = World.CreateBlip(warehouse.ExteriorLocation);
            warehouseBlip.Sprite = BlipSprite.Warehouse;
            warehouseBlip.Color = BlipColor.Blue;
            warehouseBlip.Name = warehouse.Name;
            warehouseBlip.IsShortRange = true;
            LoadInteriorProps(warehouse);
    }
        // Request the IPL for the warehouse interior
        Function.Call(Hash.REQUEST_IPL, commonInteriorIPLName);

        }
    public static void LoadGenericIPLs()
{
    string[] commonIpls = new string[]
    {
        "imp_dt1_02_cargarage_a",
        "imp_dt1_11_cargarage_c",
        "imp_sm_13_cargarage_a",
        "imp_sm_15_cargarage_c",
        "imp_ex_sm_13_office_01a",
        "imp_ex_sm_13_office_02a",
        "imp_ex_dt1_11_office_01a",
        "imp_ex_dt1_11_office_02a",
        "imp_ex_sm_15_office_01a",
        "imp_ex_sm_15_office_02a",
        "imp_dt1_02_modgarage",
        "imp_dt1_11_modgarage",
        "imp_sm_13_modgarage",
        "imp_sm_15_modgarage"
    };

    foreach (var ipl in commonIpls)
    {
        Function.Call(Hash.REQUEST_IPL, ipl);
    }
}


public static void LoadInteriorProps(Warehouse warehouse)
{
    int interiorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, warehouse.InteriorLocation.X, warehouse.InteriorLocation.Y, warehouse.InteriorLocation.Z);

    List<string> propList = new List<string>
    {
        "basic_style_set",
        "door_blocker",
        "tool_draw",
        "tool_board",
        "tools1",
        "tools2",
        "tools3",
        "spray_1",
        "spray_2",
        "spray_3",
        "imp_dt1_02_cargarage_a",
        "imp_dt1_11_cargarage_c",
        "imp_sm_13_cargarage_a",
        "imp_sm_15_cargarage_c",
        "imp_ex_sm_13_office_01a",
        "imp_ex_sm_13_office_02a",
        "imp_ex_dt1_11_office_01a",
        "imp_ex_dt1_11_office_02a",
        "imp_ex_sm_15_office_01a",
        "imp_ex_sm_15_office_02a",
        "imp_dt1_02_modgarage",
        "imp_dt1_11_modgarage",
        "imp_sm_13_modgarage",
        "imp_sm_15_modgarage",
        "imp_impexp_interior_placement_interior_3_impexp_int_02_milo_",
        "pump_01",
        "pump_02",
        "pump_03",
        "pump_04",
        "pump_05",
        "pump_06",
        "pump_07",
        "pump_08"
    };

    foreach (string propName in propList)
    {
        Function.Call((GTA.Native.Hash)0x55E86AF2712B36A1, interiorID, propName);
    }

    Function.Call(Hash.REFRESH_INTERIOR, interiorID);
}


}
