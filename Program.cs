using System.Xml.Serialization;

String path = @"C:\VScode\Kunskapskontroll 2\items.txt";

ShoppingList list = new ShoppingList(path); //Create the object ShoppingList, the argument is a textfile that contain unformatted shoppinglist data
list.Load(); //First the ShoppingList method "Load()" is called, its purpose is to read the external text file containing unformatted items data to be added to the object shoppinglist

int choice;
bool invalidChoice = true;

do
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
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
        Console.WriteLine("Invalid choice, choose a valid number");
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
        int number = int.Parse(Console.ReadLine());
        list.RemoveAt(number);                          //Note - parameter nummer and list index might be incongruent (list nr - 1 = index)
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