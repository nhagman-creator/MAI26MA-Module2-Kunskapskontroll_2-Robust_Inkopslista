ShoppingList list = new ShoppingList("items.txt"); //Create the object ShoppingList, the argument is a textfile that contain unformatted shoppinglist data
list.Load(); //First the ShoppingList method "Load()" is called, its purpose is to read the external text file containing unformatted items data to be added to the object shoppinglist

while (true) //Main loop that will be true as long as the program is running (or as long as option 5 has not been choosen)
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

    int choice = int.Parse(Console.ReadLine());

    if (choice == 1)
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
        list.RemoveAt(number);                          //Note - parameter number and list index might be incongruent (list nr - 1 = index)
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
}
