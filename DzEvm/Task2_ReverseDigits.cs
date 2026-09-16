namespace DzEvm.Tasks;

/// <summary>
/// Задание 2: Обратный порядок цифр двузначного числа
/// </summary>
public class Task2_ReverseDigits
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 2: Обратный порядок цифр ===");
        Console.WriteLine("Введите двузначное число:");
        
        int number = ReadInteger();
        int reversedNumber = ReverseNumber(number);
        
        Console.WriteLine($"Обратное число: {reversedNumber}");
    }
    
    private static int ReverseNumber(int number)
    {
        int sign = number < 0 ? -1 : 1;
        int absoluteNumber = Math.Abs(number);
        
        int firstDigit = absoluteNumber / 10;
        int secondDigit = absoluteNumber % 10;
        
        return sign * (secondDigit * 10 + firstDigit);
    }
    
    private static int ReadInteger()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int number))
                return number;
            
            Console.WriteLine("Ошибка: введите целое число!");
        }
    }
}
