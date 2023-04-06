// WarehouseInterior.cs
using GTA;
using GTA.Native;
using GTA.Math;
using System.Collections.Generic;

namespace ImportExportModNamespace
{
    public class WarehouseInterior
    {
        private List<string> _loadedIPLs;
        // Add a property to store the warehouse interior position
        public Vector3 WarehouseInteriorPosition { get; private set; }

        public WarehouseInterior()
        {
            WarehouseInteriorPosition = new Vector3(970.7842f, -2987.536f, -39.6470f);
            LoadInterior();
            _loadedIPLs = new List<string>();
        }


        public void LoadInteriorIPLs()
        {
            List<string> ipls = new List<string>
            {
                "imp_dt1_11_modgarage",
                "imp_dt1_11_office",
                "imp_dt1_11_mod_garage",
                "imp_dt1_11_mod_office",
                "imp_dt1_11_modoffice_garage",
                "imp_dt1_11_mod_garage_office",
                "imp_dt1_11_mod_office_garage",
                "imp_dt1_11_mod_garage_office_2",
            };

            foreach (string ipl in ipls)
            {
                Function.Call(Hash.REQUEST_IPL, ipl);
                _loadedIPLs.Add(ipl);
            }
        }






        public void LoadInterior()
        {
            Function.Call(Hash.REQUEST_IPL, "imp_impexp_interior_placement_interior_1_impexp_intwaremed_milo_");
            // Add any required props loading here
        }




        // Add this method to switch the interior style
        public void SwitchInteriorStyle(string newStyleIpl)
        {
            // Unload the current IPL
            Function.Call(Hash.REMOVE_IPL, _loadedIPLs[0]);

            // Load the new IPL
            Function.Call(Hash.REQUEST_IPL, newStyleIpl);
            _loadedIPLs[0] = newStyleIpl;
        }


        public void Dispose()
            {
                // Unload the loaded IPLs
                foreach (string iplName in _loadedIPLs)
                {
                    Function.Call(Hash.REMOVE_IPL, iplName);
                }
            }
        }
}