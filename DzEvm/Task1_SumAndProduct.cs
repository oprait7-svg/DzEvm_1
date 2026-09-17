namespace DzEvm.Tasks;


public class Task1_SumAndProduct
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 1: Сумма и произведение ===");
        Console.WriteLine("Введите три числа:");
        
        int firstNumber = ReadInteger();
        int secondNumber = ReadInteger();
        int thirdNumber = ReadInteger();
        
        int sum = firstNumber + secondNumber + thirdNumber;
        int product = firstNumber * secondNumber * thirdNumber;
        
        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Произведение: {product}");
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
