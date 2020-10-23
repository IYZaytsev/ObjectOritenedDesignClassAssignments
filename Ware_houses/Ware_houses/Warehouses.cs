using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using System.Data;

namespace Warehouses
{
    class Warehouses
    {
        public static string[] wareHouseNames = new String[] { "Atlanta", "Baltimore", "Chicago", "Denver", "Ely", "Fargo" };
        static void Main(string[] args)
        {
            Dictionary<string, Inventory> wareHouseList = new Dictionary<string, Inventory>();
            System.IO.StreamReader inventoryFile = new System.IO.StreamReader(@".\Inventory.txt");
            string currentLine;
            // Setting all the intial values in the 6 warehouses from inventory file

            int indexForWhile = 0;
            while ((currentLine = inventoryFile.ReadLine()) != null)
            {
                string[] intialInventory = currentLine.Split(',');
                wareHouseList[wareHouseNames[indexForWhile]] = new Inventory();
                System.Console.WriteLine(string.Format("{0}: {1}", wareHouseNames[indexForWhile], currentLine));
                wareHouseList[wareHouseNames[indexForWhile]].SetItems(int.Parse(intialInventory[0]), int.Parse(intialInventory[1]), int.Parse(intialInventory[2]), int.Parse(intialInventory[3]), int.Parse(intialInventory[4]));
                indexForWhile++;
            }
            System.Console.WriteLine();
            System.Console.WriteLine("------------------------------------------------------------------");
            // Reading Transaction file and handling 
            System.IO.StreamReader transactions = new System.IO.StreamReader(@".\Transactions.txt");
            while ((currentLine = transactions.ReadLine()) != null)
            {
                string[] currentTransaction = currentLine.Split(',');
                HandleTransaction(wareHouseList, currentTransaction[0], int.Parse(currentTransaction[1]), int.Parse(currentTransaction[2]));

            }
            System.Console.WriteLine();
            System.Console.WriteLine("------------------------------------------------------------------");
            //printing end of day report
            for (int i = 0; i < 6; i++)
            {
                wareHouseList[wareHouseNames[i]].PrintEndOfDayReport(wareHouseNames[i]);
            }
        }
        //Finds the warehouse with the largest ammount of the that item
        static void Sale(Dictionary<string, Inventory> warehouselist, string partname, int ammount)
        {   // finding warehouse with largest ammount of part
            string nameOfLargestSoFarName = "";
            int largestValueSoFarValue = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    largestValueSoFarValue = warehouselist[wareHouseNames[i]].GetItemQuantity(partname);
                    nameOfLargestSoFarName = wareHouseNames[i];
                    continue;
                }
                if (warehouselist[wareHouseNames[i]].GetItemQuantity(partname) > largestValueSoFarValue)
                {
                    largestValueSoFarValue = warehouselist[wareHouseNames[i]].GetItemQuantity(partname);
                    nameOfLargestSoFarName = wareHouseNames[i];
                }
            }

            //making the transaction
            int oldValue = warehouselist[nameOfLargestSoFarName].GetItemQuantity(partname);
            System.Console.WriteLine(string.Format("WareHouse {0} has made a sale for {1} part {2}, new quantity is:{3} ", nameOfLargestSoFarName, ammount, partname, oldValue - ammount));
            warehouselist[nameOfLargestSoFarName].SetItemQuantity(partname, oldValue - ammount);
        }

        //Finds the warehouse with the smallest ammount of the that item
        static void Purchase(Dictionary<string, Inventory> warehouselist, string partname, int ammount)
        {
            // finding warehouse with smallest ammount of part
            string nameOfSmallestSoFarName = "";
            int smallestValueSoFarValue = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    smallestValueSoFarValue = warehouselist[wareHouseNames[i]].GetItemQuantity(partname);
                    nameOfSmallestSoFarName = wareHouseNames[i];
                    continue;
                }
                if (warehouselist[wareHouseNames[i]].GetItemQuantity(partname) < smallestValueSoFarValue)
                {
                    smallestValueSoFarValue = warehouselist[wareHouseNames[i]].GetItemQuantity(partname);
                    nameOfSmallestSoFarName = wareHouseNames[i];
                }
            }

            //making the transaction
            int oldValue = warehouselist[nameOfSmallestSoFarName].GetItemQuantity(partname);
            System.Console.WriteLine(string.Format("WareHouse {0} has made a purchase for {1} part {2}, new quantity is:{3} ", nameOfSmallestSoFarName, ammount, partname, oldValue + ammount));
            warehouselist[nameOfSmallestSoFarName].SetItemQuantity(partname, oldValue + ammount);
        }

        static void HandleTransaction(Dictionary<string, Inventory> warehouselist, string operation, int partname, int ammount)
        {
            switch (operation)
            {
                case "P":
                    Purchase(warehouselist, partname.ToString(), ammount);
                    break;
                case "S":
                    Sale(warehouselist, partname.ToString(), ammount);
                    break;
                default:
                    break;
            }
        }
    }
}

