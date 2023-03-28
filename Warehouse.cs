// Warehouse.cs
using GTA.Math;
using GTA;
using GTA.Native;

public class Warehouse
{
    public Vector3 ExteriorLocation { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public Vector3 InteriorExitLocation { get; set; }
    public string InteriorIPLName { get; set; }
    public Vector3 InteriorLocation { get; set; }
    public Vector3 LaptopLocation { get; set; }
    public Vector3 ExitLocation { get; set; }
    public VehicleHash TempCarModel { get; set; }

    public Warehouse(Vector3 exteriorLocation, string name, int price, Vector3 interiorExitLocation, string interiorIPLName, Vector3 interiorLocation, Vector3 laptopLocation, Vector3 exitLocation, Vector3 exteriorExitLocation, uint temporaryCarModel, string commonInteriorIPL)
{
    ExteriorLocation = exteriorLocation;
    Name = name;
    Price = price;
    InteriorExitLocation = interiorExitLocation;
    InteriorIPLName = interiorIPLName;
    InteriorLocation = interiorLocation;
    LaptopLocation = laptopLocation;
    ExitLocation = exitLocation;
    TempCarModel = (VehicleHash)temporaryCarModel;
    Function.Call(Hash.REQUEST_IPL, commonInteriorIPL);
}

}
