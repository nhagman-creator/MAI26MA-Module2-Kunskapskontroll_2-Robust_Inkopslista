// Holds the items and takes care of loading and saving them.
using System.Dynamic;

class ShoppingList
{
    internal List<Item> items = new List<Item>();    //internal field in the ShoppingList object. Used to store Item objects (that contain name and price fields). Set by the Add method
    private string path;    //private field that is set by the constructor 

    internal int maxBudget { get; set; } = 200;//auto property Price that wwrap the hidden fieldthat store the budget-limit, preset to 200

    public ShoppingList(string path)
    {
        //Check if file exist, else create a new one
        if (File.Exists(path))
        {
            this.path = path;   //Constructor accept the path parameter. The argument "path" is defined for each ShoppingList object that is created, and stored in the private field "path" (or this.path)
        }
        else
        {
            this.path = "items.txt";
        }
    }

    public void Add(Item item)  //method used to make changes to the private "items" field (the actual shopping list)
    {
        if (item.Price + Total() > maxBudget)
        {
            throw new ArgumentOutOfRangeException(
                nameof(item.Price),                         //skickar parameternamn "Price"
                item.Price,
                "Varan spräcker budgeten. Varan lades inte till. "
            );
        }
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

        for (int i = 0; i < items.Count; i++)
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
            if (item.Name.ToLower() == name.ToLower())
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

    // Writes a new empty file items.txt (if it does not already exist).
    public void NewFile()
    {
        //could be wrapped with try-catch, but the path is current working directory, and it is unlikely that another program will use the same items.txt file, or that the the shoppinglist will cause a full disk
        Console.WriteLine("Creating new empty items.txt file, to read from and write to");
        File.WriteAllText(path, "");
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
        catch (DirectoryNotFoundException ex)
        {
            //Executed only if the folder path is missing/invalid
            Console.WriteLine($"Error: target folder could not be found.");
            Console.WriteLine($"Technical Details: {ex.Message}");
        }

        Console.WriteLine("\n\nListan är sparad.\n");
    }

    // Reads the file back into the list.
    public void Load()
    {
        string fullText = File.ReadAllText(path);//Opens, reads the file, save data as string, closes the file. This method accepts a parameter "path". When the method is called in i.e. Program.cs, the argument (relative file-path) is specified. 

        string[] allLines = fullText.Split("\r\n");      //the Split function scans through the string in search for the argument (\r\n). The string before a "\r\n" expression is stored and indexed in the "allLines" array. 

        foreach (string line in allLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;   //skip empty lines
            string[] cleanParts = line.Trim().Split(';');
            if (cleanParts.Length < 2) continue; //skip any other unexpected format that does not result in an item paired with a price  

            items.Add(new Item(cleanParts[1], int.Parse(cleanParts[0]))); //create items objects with "parts" arguments (index 0, index 1)
        }
    }
}
