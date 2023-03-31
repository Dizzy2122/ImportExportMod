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
    }
}

