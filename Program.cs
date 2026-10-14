String path = @"C:\VScode\Kunskapskontroll 2\items.txt";

ShoppingList list = new ShoppingList(path); //Create the object ShoppingList, the argument is a textfile that contain unformatted shoppinglist data
list.Load(); //First the ShoppingList method "Load()" is called, its purpose is to read the external text file containing unformatted items data to be added to the object shoppinglist

int choice;
bool invalidChoice = true;

do
{
    Console.WriteLine("----------------------------------");
    Console.WriteLine("Shopping Lista:");
    Console.WriteLine("----------------------------------");
    list.Print();
    Console.WriteLine();
    Console.WriteLine("----------------------------------");
    Console.WriteLine("Menu:");
    Console.WriteLine("----------------------------------");
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

    //Test for valid input, integer between 1 and 5
    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
    {
        Console.WriteLine("______________________________________________________");
        Console.WriteLine("Invalid choice, choose a valid menu number");
    }
    else if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
        Console.Write("Pris: ");
        int price = int.Parse(Console.ReadLine());
        list.Add(new Item(name, price));                //create a new item and add it to the list object
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        var (boolOut, intOut) = CheckInputFunctions.checkInputIsInteger(); //static function to check for integer

        if (boolOut == true && intOut <= list.items.Count && intOut > 0) //if integer, and if exist then
        {
            list.RemoveAt(intOut);
        }
        else if (boolOut == false)
        {
            Console.WriteLine("______________________________________________________");
            Console.WriteLine("Invalid input, key in an existing row in the list to remove an item");
        }
        else
        {
            Console.WriteLine("______________________________________________________");
            Console.WriteLine($"Row number {intOut} does not exist, key in an existing row in the list to remove an item");
        }
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
} while (invalidChoice); //test for user input integer, and for valid range (1 - 5)