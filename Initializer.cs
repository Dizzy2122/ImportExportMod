// Initializer.cs
using GTA;
using System.Collections.Generic;

public class Initializer : Script
{
    private MenuHandler _menuHandler;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;
    private SimpleMenuHandler _simpleMenuHandler;

    public Initializer()
    {
        _interiorManager = new InteriorManager();
        _importExportMod = new ImportExportMod(_interiorManager);
        _menuHandler = _importExportMod.MenuHandlerInstance;

        _simpleMenuHandler = new SimpleMenuHandler();
    



    // Subscribe to the Tick event
    this.Tick += _menuHandler.OnTick;
}
}
