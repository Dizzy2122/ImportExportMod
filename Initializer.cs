// Initializer.cs
using GTA;
using System.Collections.Generic;

public class Initializer : Script
{
    private MenuHandler _menuHandler;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;

    public Initializer()
{
    _interiorManager = new InteriorManager();
    _importExportMod = new ImportExportMod(_interiorManager);
    _menuHandler = _importExportMod.MenuHandlerInstance;


    // Add a debug notification
    GTA.UI.Notification.Show("Initializer constructor called.");

    // Subscribe to the Tick event
    this.Tick += _menuHandler.OnTick;
    this.Tick += _importExportMod.OnTick;
}
}
