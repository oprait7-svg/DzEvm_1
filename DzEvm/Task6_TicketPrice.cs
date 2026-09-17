namespace DzEvm.Tasks;


public class Task6_TicketPrice
{
    public static void Execute()
    {
        Console.WriteLine("\n=== Задание 6: Цена билета по возрасту ===");
        Console.Write("Введите возраст: ");
        int age = ReadAge();
        
        Console.Write("Введите цену билета: ");
        double basePrice = ReadPrice();
        
        double ticketPrice = CalculateTicketPrice(age, basePrice);
        Console.WriteLine($"Цена билета: {ticketPrice:F2}");
    }
    
    private static double CalculateTicketPrice(int age, double basePrice)
    {
        return age switch
        {
            < 7 => 0,                    
            < 18 => basePrice / 2,      
            <= 60 => basePrice,          
            _ => basePrice / 2            
        };
    }
    
    private static int ReadAge()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int age) && age >= 0)
                return age;
            
            Console.WriteLine("Ошибка: введите неотрицательное число!");
        }
    }
    
    private static double ReadPrice()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double price) && price > 0)
                return price;
            
            Console.WriteLine("Ошибка: введите положительное число!");
        }
    }
}
