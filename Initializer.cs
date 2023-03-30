// Initializer.cs
using GTA;
using System.Collections.Generic;


public class Initializer : Script
{
    private MenuHandler _menuHandler;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;
    private List<Warehouse> availableWarehouses;
    private List<Warehouse> ownedWarehouses;

    public Initializer()
    {
        // Initialize the lists if needed
        availableWarehouses = new List<Warehouse>();
        ownedWarehouses = new List<Warehouse>();

         _interiorManager = new InteriorManager();
        _importExportMod = new ImportExportMod(_interiorManager);
        _menuHandler = new MenuHandler(_interiorManager, _availableWarehouses, _ownedWarehouses, _importExportMod);
    }
}

