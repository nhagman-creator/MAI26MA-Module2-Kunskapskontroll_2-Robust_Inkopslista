static internal class CheckInputFunctions
{
    //Function to check for valid user input (string). Return true or false and the value.
    static internal bool checkInputIsString(out string validatedInput)
    {
        string input = Console.ReadLine();
        //Check for invalid input characters (null/whitespace)
        if (!string.IsNullOrWhiteSpace(input))
        {
            validatedInput = input;
            return true;
        }
        else
        {
            validatedInput = input;
            return false;
        }
    }

    //Function to check for valid user input (integer). Return true or false and the value.
    static internal bool checkInputIsInteger(out int validatedInteger)
    {
        //Check for invalid input characters (null/whitespace/non-numeric)
        if (int.TryParse(Console.ReadLine(), out int Int) /*&& Int > 0*/)
        {
            validatedInteger = Int;
            return true;
        }
        else
        {
            validatedInteger = Int;
            return false;
        }
    }

    //Function to check for valid user input (Y(es) or N(o)) Return true or false and the value.
    static internal bool checkAnswerYesOrNo(out string validatedAnswer)
    {
        string input = Console.ReadLine();
        //Check for invalid input characters (null/whitespace)
        if (!string.IsNullOrWhiteSpace(input) && (input.ToLower() == "y" || input.ToLower() == "yes"))
        {
            validatedAnswer = input.ToLower();
            return true;
        }
        else if (!string.IsNullOrWhiteSpace(input) && (input.ToLower() == "n" || input.ToLower() == "no"))
        {
            validatedAnswer = input.ToLower();
            return true;
        }
        else
        {
            validatedAnswer = input.ToLower();
            return false;
        }
    }
}
