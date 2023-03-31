// SimpleMenuHandler.cs
using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class SimpleMenuHandler : Script
{
    private UIMenu _simpleMenu;
    private MenuPool _menuPool;

    public SimpleMenuHandler()
    {
        _menuPool = new MenuPool();
        InitializeMenu();

        // Subscribe to the Tick event
        this.Tick += OnTick;
    }

    private void InitializeMenu()
    {
        _simpleMenu = new UIMenu("Simple Menu", "SIMPLE MENU OPTIONS");
        _menuPool.Add(_simpleMenu);

        UIMenuItem testItem = new UIMenuItem("Test Item");
        _simpleMenu.AddItem(testItem);
    }

    private void OnTick(object sender, EventArgs e)
{
    _menuPool.ProcessMenus();

    if (Game.IsControlJustReleased(0, Control.InteractionMenu)) // Change "Control.InteractionMenu" to another key if you prefer
    {
        _simpleMenu.Visible = !_simpleMenu.Visible;
    }
}


}

