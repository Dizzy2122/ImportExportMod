using GTA;

public class Initializer : Script
{
    private MenuHandler _menuHandler;
    private InteriorManager _interiorManager;
    private ImportExportMod _importExportMod;

    public Initializer()
    {
        _interiorManager = new InteriorManager();
        _menuHandler = new MenuHandler(_interiorManager);
        _importExportMod = new ImportExportMod(_interiorManager);
    }
}
