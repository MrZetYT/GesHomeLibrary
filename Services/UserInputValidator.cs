namespace GesHomeLibrary.Services;

public class UserInputValidator
{
    public int NumberInput(int minNumber, int maxNumber)
    {
        int input = 0;
        while (input <= minNumber-1 || input > maxNumber)
        {
                            
            Console.Write("Ввод: ");
            try
            {
                input = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Неправильный ввод! Попробуйте еще раз...");
            }
        }
        
        return input;
    }

    public string StringInput()
    {
        string input;
        while (true)
        {
            Console.Write("Ввод: ");
            input = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(input))
                break;
            
            Console.WriteLine("Неправильный ввод! Попробуйте еще раз...");
        }
        return input;
    }
}