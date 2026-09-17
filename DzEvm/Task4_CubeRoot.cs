namespace DzEvm.Tasks;


public class Task4_CubeRoot
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 4: Кубический корень ===");
        Console.WriteLine("Введите число (объём):");
        
        double volume = ReadDouble();
        double cubeRoot = Math.Pow(volume, 1.0 / 3.0);
        
        Console.WriteLine($"Кубический корень: {cubeRoot:F2}");
    }
    
    private static double ReadDouble()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double number))
                return number;
            
            Console.WriteLine("Ошибка: введите число!");
        }
    }
}
