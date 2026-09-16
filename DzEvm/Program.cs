using DzEvm.Tasks;



while (true)
{
    DisplayMenu();
    string choice = Console.ReadLine()?.Trim() ?? "";
    
    switch (choice)
    {
        case "1":
            Task1_SumAndProduct.Execute();
            break;
        case "2":
            Task2_ReverseDigits.Execute();
            break;
        case "3":
            Task3_Hypotenuse.Execute();
            break;
        case "4":
            Task4_CubeRoot.Execute();
            break;
        case "5":
            Task5_DaysInMonth.Execute();
            break;
        case "6":
            Task6_TicketPrice.Execute();
            break;
        case "7":
            Task7_Triangle.Execute();
            break;
        case "0":
            Console.WriteLine("\nСпасибо за использование программы! До свидания!");
            return;
        default:
            Console.WriteLine("\n Некорректный выбор. Пожалуйста, попробуйте снова.");
            break;
    }
}

void DisplayMenu()
{
    Console.WriteLine("\n┌────────────────────────────────────────┐");
    Console.WriteLine("│          Выберите задание:             │");
    Console.WriteLine("├────────────────────────────────────────┤");
    Console.WriteLine("│ 1 - Сумма и произведение трёх чисел    │");
    Console.WriteLine("│ 2 - Обратный порядок цифр              │");
    Console.WriteLine("│ 3 - Гипотенуза                         │");
    Console.WriteLine("│ 4 - Кубический корень                  │");
    Console.WriteLine("│ 5 - Дни в месяце                       │");
    Console.WriteLine("│ 6 - Цена билета по возрасту            │");
    Console.WriteLine("│ 7 - Проверка треугольника              │");
    Console.WriteLine("│ 0 - Выход                              │");
    Console.WriteLine("└────────────────────────────────────────┘");
    Console.Write("Ввод: ");
}
