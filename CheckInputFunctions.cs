static internal class CheckInputFunctions
{
    //Function to check for valid user input (string). Parameter = question, return true or false.
    static internal (bool, string) checkInputIsString()
    {
        string input = Console.ReadLine();
        //Check for invalid input characters (null/whitespace)
        if (!string.IsNullOrWhiteSpace(input))
        {
            return (false, input);
        }
        else
        {
            return (true, input);
        }
    }

    //Function to check for valid user input (integer). Parameter = question, return true or false.
    static internal (bool, int) checkInputIsInteger()
    {
        //Check for invalid input characters (null/whitespace/non-numeric/negatives or zeroes)
        if (!int.TryParse(Console.ReadLine(), out int Int) /*&& Int > 0*/)
        {
            return (false, Int);
        }
        else
        {
            return (true, Int);
        }
    }
}
