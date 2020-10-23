using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouses
{
    class Inventory
    {
        private Dictionary<string, int> partCatalog = new Dictionary<string, int>();
        public Inventory()
        {

        }
        public void SetItems(int part1, int part2, int part3, int part4, int part5)
        {
            partCatalog["102"] = part1;
            partCatalog["215"] = part2;
            partCatalog["410"] = part3;
            partCatalog["525"] = part4;
            partCatalog["711"] = part5;
        }

        public void PrintEndOfDayReport(string wareHouseName)
        {

            string line = partCatalog["102"].ToString() + ", " + partCatalog["215"].ToString() + ", " + partCatalog["410"].ToString() + ", " + partCatalog["525"].ToString() + ", " + partCatalog["711"].ToString();
            System.Console.WriteLine(string.Format("{0}: {1}", wareHouseName, line));
        }
        public int GetItemQuantity(string partname)
        {
            return partCatalog[partname];
        }

        public void SetItemQuantity(string partname, int quantity)
        {
            partCatalog[partname] = quantity;
        }

    }
}
