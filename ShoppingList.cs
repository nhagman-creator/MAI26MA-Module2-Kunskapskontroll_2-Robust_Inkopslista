// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();    //private field in the ShoppingList object. Used to store Item objects (that contain name and price fields). Set by the Add method
    private string path;    //private field that is set by the constructor 

    public ShoppingList(string path)
    {
        this.path = path;   //Constructor accept the path parameter. The argument "path" is defined for each ShoppingList object that is created, and stored in the private field "path" (or this.path)
    }

    public void Add(Item item)  //method used to make changes to the private "items" field (the actual shopping list)
    {
        items.Add(item);
    }

    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 1; i < items.Count; i++) //skipping index 0, merchandise 1 is not added to the total
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
        }
        catch
        {
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
    public void Load()
    {
        string text = File.ReadAllText(path);   //Opens, reads the file, save data as string, closes the file. This method accepts a parameter "path". When the method is called in i.e. Program.cs, the argument (relative file-path) is specified. 
        string[] lines = text.Split("\r\n");      //the Split function scans through the string in search for the argument (new line character). The string before a new line character is stored and indexed in the "lines" array. 

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');   //split on argument ";" and remove it. Save each element as they appear and index them in the array "parts". Pattern: price item 1 (index 0), item 1 (index 1), price item 2 (index 2), item 2 (index 3), etc..
            items.Add(new Item(parts[1], int.Parse(parts[0]))); //create items objects with "parts" arguments (index 0, index 1)
        }
    }
}
