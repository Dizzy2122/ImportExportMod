// InteriorStyles.cs
using GTA;
using NativeUI;
using System.Collections.Generic;

namespace ImportExportModNamespace
{
    public class InteriorStyles
    {
        public bool StyleSelected { get; private set; }
        private MenuPool _menuPool;
        private UIMenu _warehouseStylesMenu;
        private List<string> _warehouseStyles;
        private WarehouseInterior _interior;

        public InteriorStyles(MenuPool menuPool, WarehouseInterior interior)
        {
            _menuPool = menuPool;
            _interior = interior;
            SetupWarehouseStylesMenu();
        }

        public bool IsMenuVisible
        {
            get { return _warehouseStylesMenu.Visible; }
        }

        private void SetupWarehouseStylesMenu()
        {
            _warehouseStylesMenu = new UIMenu("Warehouse Styles", "SELECT STYLE");
            _menuPool.Add(_warehouseStylesMenu);

            _warehouseStyles = new List<string>
            {
                "imp_dt1_11_modgarage",
                // Add other warehouse styles here
            };

            foreach (string style in _warehouseStyles)
            {
                UIMenuItem styleItem = new UIMenuItem(style);
                _warehouseStylesMenu.AddItem(styleItem);
            }

            _warehouseStylesMenu.OnItemSelect += (sender, item, index) =>
            {
                GTA.UI.Notification.Show("OnItemSelect triggered");
                string selectedStyle = item.Text;
                _interior.SwitchInteriorStyle(selectedStyle);
                StyleSelected = true;
                _warehouseStylesMenu.Visible = false;
            };

            _warehouseStylesMenu.OnMenuClose += (sender) =>
            {
                GTA.UI.Notification.Show("OnMenuClose triggered");
                if (_warehouseStylesMenu != null)
                {
                    _warehouseStylesMenu.Visible = true;
                }
            };

            _warehouseStylesMenu.RefreshIndex();
        }

        public void ShowMenu(UIMenu parentMenu)
        {
            _warehouseStylesMenu.Visible = true;

            if (parentMenu != null)
            {
                parentMenu.Visible = false;
            }
        }

    }
}
