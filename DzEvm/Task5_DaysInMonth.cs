namespace DzEvm.Tasks;


public class Task5_DaysInMonth
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 5: Дни в месяце ===");
        Console.Write("Введите номер месяца (1-12): ");
        
        int month = ReadMonth();
        int daysInMonth = GetDaysInMonth(month);
        
        Console.WriteLine($"Дней в месяце: {daysInMonth}");
    }
    
    private static int GetDaysInMonth(int month)
    {
        return month switch
        {
            1 or 3 or 5 or 7 or 8 or 10 or 12 => 31,
            2 => 28,
            4 or 6 or 9 or 11 => 30,
            _ => throw new ArgumentException("Некорректный номер месяца")
        };
    }
    
    private static int ReadMonth()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int month) && month >= 1 && month <= 12)
                return month;
            
            Console.WriteLine("Ошибка: введите число от 1 до 12!");
        }
    }
}
