namespace DzEvm.Tasks;


public class Task3_Hypotenuse
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 3: Гипотенуза ===");
        Console.WriteLine("Введите две стороны прямоугольного треугольника:");
        
        double sideA = ReadDouble();
        double sideB = ReadDouble();
        
        double hypotenuse = Math.Sqrt(sideA * sideA + sideB * sideB);
        
        Console.WriteLine($"Гипотенуза: {hypotenuse:F2}");
    }
    
    private static double ReadDouble()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double number) && number > 0)
                return number;
            
            Console.WriteLine("Ошибка: введите положительное число!");
        }
    }
}
