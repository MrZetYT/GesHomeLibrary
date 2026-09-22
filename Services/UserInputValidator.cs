namespace GesHomeLibrary.Services;

public class UserInputValidator
{
    public int NumberInput(int minNumber, int maxNumber)
    {
        int result = 0;
        while (result <= minNumber-1 || result > maxNumber)
        {
            Console.Write("Ввод: ");
            
            string? input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Неправильный ввод! Попробуйте еще раз...");
                continue;
            }
            
            try
            {
                result = int.Parse(input);
            }
            catch
            {
                Console.WriteLine("Нечисловое значение! Попробуйте еще раз...");
            }
        }
        
        return result;
    }

    public string StringInput()
    {
        string? input;
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