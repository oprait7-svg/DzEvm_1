namespace DzEvm.Tasks;

public class Task7_Triangle
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 7: Треугольник ===");
        Console.WriteLine("Введите три стороны треугольника:");
        
        int sideA = ReadPositiveInteger();
        int sideB = ReadPositiveInteger();
        int sideC = ReadPositiveInteger();
        
        if (!IsValidTriangle(sideA, sideB, sideC))
        {
            Console.WriteLine("Треугольник не может быть сформирован!");
            return;
        }
        
        Console.WriteLine("Треугольник может быть сформирован!");
        
        if (IsRightTriangle(sideA, sideB, sideC))
        {
            Console.WriteLine("Треугольник является прямоугольным!");
        }
        else
        {
            Console.WriteLine("Треугольник не является прямоугольным!");
        }
    }
    
    private static bool IsValidTriangle(int a, int b, int c)
    {
        return a + b > c && a + c > b && b + c > a;
    }
    
    private static bool IsRightTriangle(int a, int b, int c)
    {
        // Проверяем, удовлетворяет ли треугольник теореме Пифагора
        int[] sides = { a, b, c };
        Array.Sort(sides);
        
        return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
    }
    
    private static int ReadPositiveInteger()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int number) && number > 0)
                return number;
            
            Console.WriteLine("Ошибка: введите положительное целое число!");
        }
    }
}
