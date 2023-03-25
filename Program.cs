using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace OperatorOverloadInventory
{
    public class Inventory
    {
        public string Item { get; set; }

        public int Quantity { get; set; }

        //Overloaded operator ++ and operator --
        public static Inventory operator ++(Inventory obj)
        {
            obj.Quantity++;
            return obj;
        }
        public static Inventory operator --(Inventory obj)
        {
            obj.Quantity--;
            return obj;
        }

        //Overloaded operator + and operator +
        public static Inventory operator +(Inventory lhs, int updateQty)
        {
            Inventory qty1 = new Inventory();
            qty1.Quantity = lhs.Quantity + updateQty;
            return qty1;
        }
        
        public static Inventory operator -(Inventory lhs, int updateQty)
        {
            Inventory qty2 = new Inventory();
            qty2.Quantity = lhs.Quantity - updateQty;
            return qty2;
        }

        //Overloaded operator > and operator <
        public static bool operator >(Inventory lhs, int qtyCompare)
        {
            bool order = false;
            if (lhs.Quantity > qtyCompare) 
            {
                order = true;
            }
            return order;
        }
        public static bool operator <(Inventory lhs, int qtyCompare) 
        {
            bool order = true;
            if (lhs.Quantity < qtyCompare) 
            {
                order = false;
            }
            return order;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory[] storeInv = new Inventory[50];
            for (int i = 0; i < storeInv.Length; i++)
            {
                storeInv[i] = new Inventory();
            }

            int index = 0, entry = 0;
            string update = "";
            int option = Menu();
            while (option != 5)
            {
                switch(option)
                {
                    case 1:
                        if (index < 50)
                        {
                            Console.WriteLine("Enter an item: ");
                            storeInv[index].Item = Console.ReadLine();
                            Console.WriteLine("Enter quantity: ");
                            storeInv[index].Quantity = int.Parse(Console.ReadLine());
                            index++;
                        }
                        else
                            Console.WriteLine("You have run out of entries, please contact your system administrator.");
                        break;
                    
                    case 2:
                        entry = pickEntry(index);

                        Console.Write("Increase Quantity by 1?  Y for Yes ");
                        update = Console.ReadLine();
                        if (update == "Y" || update == "y")
                        {
                            storeInv[entry]++;
                            Console.WriteLine();
                            break;
                        }

                        Console.Write("Decrease Quantity by 1?  Y for Yes ");
                        update = Console.ReadLine();
                        if (update == "Y" || update == "y")
                        {
                            storeInv[entry]--;
                            Console.WriteLine();
                            break;
                        }

                        Console.Write("Increase Quantity by more than 1?  Y for Yes ");
                        update = Console.ReadLine();
                        if (update == "Y" || update == "y")
                        {
                            Console.Write("Enter the new quanity: ");
                            int qty;
                            while (!int.TryParse(Console.ReadLine(), out qty))
                                Console.WriteLine($"Please a number");
                            storeInv[entry].Quantity += qty;
                            Console.WriteLine();
                            break;
                        }

                        Console.Write("Decrease quantity by more than 1?  Y for Yes ");
                        update = Console.ReadLine();
                        if (update == "Y" || update == "y")
                        {
                            Console.Write("Enter the new quantity: ");
                            int qty;
                            while (!int.TryParse(Console.ReadLine(), out qty))
                                Console.WriteLine($"Please a number");
                            storeInv[entry].Quantity -= qty;
                            Console.WriteLine();
                            break;
                        }
                        break;
                    
                    case 3:
                        Inventory restock = new Inventory();
                        restock.Quantity = 5;
                        Console.WriteLine($"If our inventory falls below {restock.Quantity}, we will need to restock.");
                        for (int i = 0; i < storeInv.Length; i++)
                        {
                            if (storeInv[i].Item != "" && storeInv[i].Item != null)
                            {
                                if (storeInv[i] > restock.Quantity)
                                    Console.WriteLine($"We do not need to restock {storeInv[i].Item}.");
                                else
                                    Console.WriteLine($"Please place an order for {storeInv[i].Item}.");
                            }
                        }
                        Console.WriteLine();
                        break;

                    case 4:
                        for (int i = 0; i < storeInv.Length; i++)
                        {
                            if (storeInv[i].Item != "" && storeInv[i].Item != null)
                            {
                                Console.WriteLine($"Item: {storeInv[i].Item}");
                                Console.WriteLine($"Quantity: {storeInv[i].Quantity}");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("You made an invalid selection, please try again");
                        break;
                }
                option = Menu();
            }
        } 
        public static int Menu()
        {
            int choice = 0;
            Console.WriteLine("Please make a selection from the menu");
            Console.WriteLine("1 - Add an Inventory Item");
            Console.WriteLine("2 - Update Quantity");
            Console.WriteLine("3 - Compare for restock");
            Console.WriteLine("4 - Print All");
            Console.WriteLine("5 - Quit");
            while (!int.TryParse(Console.ReadLine(), out choice))
                Console.WriteLine("Please select 1 - 5");
            return choice;
        }
        public static int pickEntry(int index)
        {
            Console.WriteLine("What entry would you like to change?");
            Console.WriteLine($"1 through {index}");
            int entry;
            while (!int.TryParse(Console.ReadLine(), out entry))
                Console.WriteLine($"Please select 1 - {index}");
            entry -= 1;
            return entry;
        }
    }
}