
using System.Text.RegularExpressions;
using System.Text;


public class Program
{
    public static void Main()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    Task1_PrintAbbreviations();
                    break;
                case "2":
                    Task2_ValidateLicensePlate();
                    break;
                case "3":
                    Task3_ReplaceQuotes();
                    break;
                case "4":
                    List<int> list3 = new List<int> { 41, 33, 15, 25, 5, 541, 4 };
                    var query = list3
                        .Where(x => x >= 20)
                        .Select(x => x * 2);
                    list3.Add(40);
                    int count = query.Count();
                    Console.WriteLine($" Кол-во элементов в результате: {count}");
                    list3.Remove(25);
                    Console.WriteLine(" Удалили 25 из списка");
                    Console.WriteLine($"\n Результат запроса:");
                    foreach (var item in query)
                    {
                        Console.WriteLine($"  {item}");
                    }
                    break;
                case "5":
                    List<int> list2 = new();
                    Random random1 = new Random();
                    for (int i = 0; i < 20; i++)
                    {
                        list2.Add(random1.Next(2, 6));
                    }
                    
                    Console.WriteLine("\nРаспределение:");
                    Enumerable.Range(2, 4)
                        .Reverse().ToList().ForEach(grade => Console.WriteLine($"{grade}:  ({list2.Count(x => x == grade)})"));
                    break;
                case "6":
                    Dictionary<string, int> dict = new Dictionary<string, int>()
                    {
                       { "Иван", 85 },
                        { "Мария", 92 },
                        { "Петр", 78 },
                         { "Анна", 100 },
                        { "Сергей", 39 }
                    };
                    double average = dict.Values.Average();
                    Console.WriteLine($"Средний балл: {average:F2}");
                    int minScore = dict.Values.Min();
                    int maxScore = dict.Values.Max();
                    Console.WriteLine($"Минимальный балл: {minScore}");
                    Console.WriteLine($"Максимальный балл: {maxScore}");
                    var actions = new Dictionary<bool, Action>()
                        {
                            {  dict.Values.Any(x => x == 100), () => Console.WriteLine("Студенты с баллом 100: " + string.Join(", ", dict.Where(x => x.Value == 100).Select(x => x.Key))) },
                            { !dict.Values.Any(x => x == 100), () => Console.WriteLine("Нет студентов с баллом = 100.") }
                        };
                    actions[true].Invoke();
                    (minScore >= 40
                        ? (Action)(() => Console.WriteLine("Все студенты имеют балл не ниже 40."))
                        : () => Console.WriteLine("Студенты с баллом ниже 40: " + string.Join(", ", dict.Where(x => x.Value < 40).Select(x => x.Key))))
                        .Invoke();
                    break;
                case "7":
                    Task4_PrintWords();
                    break;
                case "8":
                    Task5_InsertCharacter();
                    break;
                case "9":
                    Task6_RemoveCharacter();
                    break;
                case "10":
                    Task7_SwapCharacters();
                    break;
                case "11":
                    Task8_ReplaceDots();
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
            Console.WriteLine("\n┌──────────────────────────────────────────────┐");
            Console.WriteLine("│          Выберите задание:                     │");
            Console.WriteLine("├────────────────────────────────────────────────┤");
            Console.WriteLine("│ 1 - Вывести аббревиатуры                       │");
            Console.WriteLine("│ 2 - Проверить номер автомобиля                 │");
            Console.WriteLine("│ 3 - Заменить прямые кавычки на ёлочки          │");
            Console.WriteLine("│ 4 - LINQ запрос со списком                     │");
            Console.WriteLine("│ 5 - Распределение случайных чисел              │");
            Console.WriteLine("│ 6 - Студенты и их баллы                        │");
            Console.WriteLine("│ 7 - Вывести слова и их количество              │");
            Console.WriteLine("│ 8 - Вставить символ x после y                  │");
            Console.WriteLine("│ 9 - Удалить все вхождения символа              │");
            Console.WriteLine("│ 10 - Поменять символы попарно                  │");
            Console.WriteLine("│ 11 - Заменить точки на многоточие              │");
            Console.WriteLine("│ 12 - Работа с файлами и папками                │");
            Console.WriteLine("│ 13 - Работа с файлами и папками                │");
            Console.WriteLine("│ 0 - Выход                                      │");
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.Write("Ввод: ");
        }
    }

    static void Task1_PrintAbbreviations()
    {
        Console.WriteLine("\n=== Задача 1: Вывести аббревиатуры ===");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine() ?? "";

        MatchCollection matches = Regex.Matches(text, @"\b[А-Я]{2,}\b");

        if (matches.Count > 0)
        {
            Console.WriteLine($"Найденные аббревиатуры: {string.Join(", ", matches.Cast<Match>().Select(m => m.Value))}");
        }
        else
        {
            Console.WriteLine("Аббревиатур не найдено.");
        }
    }

    static void Task2_ValidateLicensePlate()
    {
        Console.WriteLine("\n=== Задача 2: Проверить номер автомобиля ===");
        Console.Write("Введите номер: ");
        string plate = Console.ReadLine() ?? "";

        string pattern = @"^[АБЕКМНОРСТУХ][0-9]{3}[АБЕКМНОРСТУХ]{2}[0-9]{2,3}$";

        if (Regex.IsMatch(plate, pattern))
        {
            Console.WriteLine(" Корректный автомобильный номер!");
        }
        else
        {
            Console.WriteLine(" Некорректный номер.");
        }
    }

    static void Task3_ReplaceQuotes()
    {
        Console.WriteLine("\n=== Задача 3: Заменить прямые кавычки на ёлочки ===");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine() ?? "";

        string result = Regex.Replace(text, "\"([^\"]*)\"", "«$1»");

        Console.WriteLine($"Результат: {result}");
    }

    static void Task4_PrintWords()
    {
        Console.WriteLine("\n=== Задача 7: Вывести все слова и их количество ===");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine() ?? "";

        MatchCollection matches = Regex.Matches(text, @"[а-яА-ЯёЁ0-9]+");

        if (matches.Count > 0)
        {
            Console.WriteLine($"Количество слов: {matches.Count}");
            Console.WriteLine($"Слова: {string.Join(", ", matches.Cast<Match>().Select(m => m.Value))}");
        }
        else
        {
            Console.WriteLine("Слов не найдено.");
        }
    }

    static void Task5_InsertCharacter()
    {
        Console.WriteLine("\n=== Задача 8: Вставить символ x после каждого y ===");
        Console.Write("Введите строку: ");
        string text = Console.ReadLine() ?? "";
        Console.Write("Введите символ y (после которого вставлять): ");
        char y = Console.ReadLine()?[0] ?? ' ';
        Console.Write("Введите символ x (для вставки): ");
        char x = Console.ReadLine()?[0] ?? ' ';

        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            sb.Append(c);
            if (c == y)
                sb.Append(x);
        }

        Console.WriteLine($"Результат: {sb}");
    }

    static void Task6_RemoveCharacter()
    {
        Console.WriteLine("\n=== Задача 9: Удалить все вхождения символа x ===");
        Console.Write("Введите строку: ");
        string text = Console.ReadLine() ?? "";
        Console.Write("Введите символ x для удаления: ");
        char x = Console.ReadLine()?[0] ?? ' ';

        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            if (c != x)
                sb.Append(c);
        }

        Console.WriteLine($"Результат: {sb}");
    }

    static void Task7_SwapCharacters()
    {
        Console.WriteLine("\n=== Задача 10: Поменять местами первый со вторым, третий с четвёртым ===");
        Console.Write("Введите строку: ");
        string text = Console.ReadLine() ?? "";

        StringBuilder sb = new StringBuilder(text);
        for (int i = 0; i < sb.Length - 1; i += 2)
        {
            var temp = sb[i];
            sb[i] = sb[i + 1];
            sb[i + 1] = temp;
        }

        Console.WriteLine($"Результат: {sb}");
    }

    static void Task8_ReplaceDots()
    {
        Console.WriteLine("\n=== Задача 11: Заменить группы точек на многоточие ===");
        Console.Write("Введите текст: ");
        string text = Console.ReadLine() ?? "";

        StringBuilder sb = new StringBuilder();
        int dotCount = 0;

        foreach (char c in text)
        {
            if (c == '.')
            {
                dotCount++;
            }
            else
            {
                if (dotCount >= 2)
                    sb.Append("...");
                else if (dotCount == 1)
                    sb.Append(".");
                dotCount = 0;
                sb.Append(c);
            }
        }

        if (dotCount >= 2)
            sb.Append("...");
        else if (dotCount == 1)
            sb.Append(".");

        Console.WriteLine($"Результат: {sb}");
    }


}