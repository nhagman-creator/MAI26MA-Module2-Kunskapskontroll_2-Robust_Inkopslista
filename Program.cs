String path = @"C:\VScode\Kunskapskontroll 2\items.txt";

ShoppingList list = new ShoppingList(path); //Create the object ShoppingList, the argument is a path that points to (if existing) a textfile that contain unformatted shoppinglist data, or (if it does not exist) the empty .txt-file that will created

//Check if existing file
if (File.Exists(path)) list.Load(); //First the ShoppingList method "Load()" is called, its purpose is to read the external text file containing unformatted items data to be added to the object shoppinglist

else list.NewFile(); //call a method that creates a new file in current working directory

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
        Console.WriteLine("\n\nOgiltigt val, välj ett nummer under menyn\n");
    }
    else if (choice == 1)
    {
        Console.Write("Namn: ");
        //Check for invalid input characters (null/whitespace) - while not valid
        string validatedName = "";
        while (!CheckInputFunctions.checkInputIsString(out validatedName))
        {
            Console.WriteLine("Ogiltigt namn. Namnet måste inehålla bokstäver eller siffror. Försök igen.");
            Console.Write("Namn:");
        }

        Console.Write("Pris: ");
        //Check for invalid input characters (non-numeric)
        int validatedPrice = 0;
        while (!CheckInputFunctions.checkInputIsInteger(out validatedPrice))
        {
            Console.WriteLine("Ogiltigt pris. Endast numeriska värden tillåtna. Försök igen.");
            Console.Write("Pris:");
        }

        Console.WriteLine($"Vill du lägga till {validatedName} - {validatedPrice} till listan? (y/n)");
        string validatedAnswer = "";

        //call generic function that ask for user confirmation (yes or no)
        while (!CheckInputFunctions.checkAnswerYesOrNo(out validatedAnswer))
        {
            Console.WriteLine("Skriv Y (yes) or N (no)");
        }
        if (validatedAnswer == "y")
        {
            try
            {
                list.Add(new Item(validatedName, validatedPrice));                //create a new item and add it to the list object
            }
            //Catches negative values
            catch (ArgumentOutOfRangeException ex)
            {
                if (ex.ParamName == "price")
                {
                    Console.WriteLine($"Priset får inte vara negativt. Teknisk info: {ex.Message}");
                }
                else if (ex.ParamName == "Price")
                {
                    Console.WriteLine($"Varan är för dyr. Teknisk info: {ex.Message}");
                }
            }
            //Catches null or blankspace name (redundant, since the name has been validated already), but added here for the sake of the task in kunskapskontroll 2 - del 2
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Otillåtet namn, måste innehålla tecken: {ex.Message}");
            }

        }
        else if (validatedAnswer == "n")
        {
            break;
        }
        else Console.WriteLine("Oväntat fel - inget sparades, försök igen");
    }
    else if (choice == 2)
    {
        Console.Write("Nummer: ");
        bool boolInt = CheckInputFunctions.checkInputIsInteger(out int validatedInteger); //Check for invalid input characters (null/whitespace/non-numeric)

        if (boolInt == true && validatedInteger <= list.items.Count && validatedInteger > 0) //if integer is valid, and if it is within the range then
        {
            list.RemoveAt(validatedInteger);
        }
        else if (boolInt == false)
        {
            Console.WriteLine("\n\nOgiltiga tecken. Skriv in en giltig nummer för att ta bort varan från listan.\n");
        }
        else
        {
            Console.WriteLine($"\n\nVaran med numret {validatedInteger} existerar ej. Skriv ett giltigt nummer för att ta bort varan från listan.\n");
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
            Console.WriteLine("\n\nVaran finns inte i listan.\n");
        }
        else
        {
            Console.WriteLine($"\n\nHittade: {found}\n");
        }
    }
    else if (choice == 5)
    {
        break;
    }
} while (invalidChoice); //test for user input integer, and for valid range (1 - 5)