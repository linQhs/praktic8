using System.Collections.Generic;
using System.Linq;

namespace DungeonGame
{
    class Program
    {
        static Random random = new Random();

        // структура для представления предмета инвентаря
        struct Item
        {
            public string Name;
            public string Description;
        }

        static void Main(string[] args)
        {
            // параметры игрока
            int playerHealth = 100;
            int potions = 3;
            int gold = 0;
            int arrows = 5;
            bool hasSword = true;
            bool hasBow = true;
            Item[] inventory = new Item[5];
            int inventorySize = 0;

            // карта подземелья
            string[] dungeonMap = GenerateDungeonMap();

            // игровой цикл
            for (int roomNumber = 0; roomNumber < dungeonMap.Length; roomNumber++)
            {
                Console.WriteLine($"\n--- Комната {roomNumber + 1} ---");
                Console.WriteLine($"Вы вошли в комнату: {dungeonMap[roomNumber]}");
                Console.WriteLine($"Здоровье: {playerHealth}, Зелья: {potions}, Золото: {gold}, Стрелы: {arrows}");

                switch (dungeonMap[roomNumber])
                {
                    case "Монстр":
                        if (!FightMonster(ref playerHealth, ref arrows, ref gold, hasSword, hasBow))
                        {
                            Console.WriteLine("Игра окончена!");
                            return;
                        }
                        break;
                    case "Ловушка":
                        HandleTrap(ref playerHealth);
                        if (playerHealth <= 0)
                        {
                            Console.WriteLine("Игра окончена!");
                            return;
                        }
                        break;
                    case "Сундук":
                        HandleChest(inventory, ref inventorySize, ref arrows, ref gold, ref potions);
                        break;
                    case "Торговец":
                        HandleTrader(ref playerHealth, ref gold, inventory, ref inventorySize, ref potions);
                        break;
                    case "Пустая комната":
                        Console.WriteLine("В комнате ничего не произошло.");
                        break;
                    case "Босс":
                        if (!FightMonster(ref playerHealth, ref arrows, ref gold, hasSword, hasBow))
                        {
                            Console.WriteLine("Игра окончена!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\n!!! Вы победили босса и выиграли игру! !!!");
                            return;
                        }
                }

                // Действия после комнаты
                Console.WriteLine("\nЧто вы хотите сделать?");
                Console.WriteLine("1. Продолжить");
                Console.WriteLine("2. Использовать зелье (Зелий: " + potions + ")");
                Console.Write("Выберите действие: ");

                string actionChoice = Console.ReadLine();

                if (actionChoice == "2" && potions > 0)
                {
                    playerHealth += 20;
                    potions--;
                    Console.WriteLine("Вы использовали зелье и восстановили 20 HP. Ваше здоровье: " + playerHealth);
                }

                Console.WriteLine("Нажмите Enter, чтобы продолжить...");
                Console.ReadLine();
            }
        }
        static string[] GenerateDungeonMap()
        {
            string[] map = new string[10];

            string[] events = { "Монстр", "Ловушка", "Сундук", "Торговец", "Пустая комната" };

            for (int i = 0; i < map.Length - 1; i++)
            {
                map[i] = events[random.Next(events.Length)];
            }

            map[9] = "Босс"; // босс всегда в последней комнате
            return map;
        }

        // бой с монстром
        static bool FightMonster(ref int playerHealth, ref int arrows, ref int gold, bool hasSword, bool hasBow)
        {
            int monsterHealth = random.Next(20, 51);
            Console.WriteLine($"\nВы столкнулись с монстром! У него {monsterHealth} HP.");

            while (playerHealth > 0 && monsterHealth > 0)
            {
                Console.WriteLine("\nВаши действия:");
                Console.WriteLine("1. Атаковать мечом (10-20 урона)");
                if (hasBow)
                    Console.WriteLine("2. Атаковать луком (5-15 урона, требуется стрела, у вас: " + arrows + ")");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        int damage = random.Next(10, 21);
                        monsterHealth -= damage;
                        Console.WriteLine($"Вы нанесли {damage} урона мечом.");
                        break;
                    case "2":
                        if (hasBow && arrows > 0)
                        {
                            damage = random.Next(5, 16);
                            monsterHealth -= damage;
                            arrows--;
                            Console.WriteLine($"Вы нанесли {damage} урона из лука.");
                        }
                        else
                        {
                            Console.WriteLine("У вас нет стрел или лука!");
                            continue;
                        }
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        continue;
                }

                if (monsterHealth <= 0)
                {
                    Console.WriteLine("Вы победили монстра!");
                    gold += random.Next(5, 16);
                    Console.WriteLine($"Вы нашли {gold} золота.");
                    return true;
                }

                // атака монстра
                int monsterDamage = random.Next(5, 16);
                playerHealth -= monsterDamage;
                Console.WriteLine($"Монстр нанес вам {monsterDamage} урона.");
                Console.WriteLine($"Ваше здоровье: {playerHealth} HP.");

                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вы погибли в бою!");
                    return false;
                }
            }
            return false;
        }

        // ловушка
        static void HandleTrap(ref int playerHealth)
        {
            int damage = random.Next(10, 21);
            playerHealth -= damage;
            Console.WriteLine($"\nВы попали в ловушку и потеряли {damage} HP.");
            Console.WriteLine($"Ваше здоровье: {playerHealth} HP.");

            if (playerHealth <= 0)
            {
                Console.WriteLine("Вы погибли!");
            }
        }
        static void HandleChest(Item[] inventory, ref int inventorySize, ref int arrows, ref int gold, ref int potions)
        {
            Console.WriteLine("\nВы нашли сундук! Чтобы его открыть, решите загадку.");

            Console.WriteLine("Сколько будет 1 + 1?");
            Console.Write("Ваш ответ: ");
            string answer = Console.ReadLine();

            if (answer == "2")
            {
                Console.WriteLine("Правильно! Вы открыли сундук.");

                int rewardType = random.Next(1, 4);

                switch (rewardType)
                {
                    case 1: // Зелье
                        potions++;
                        Console.WriteLine("Вы нашли зелье здоровья!");
                        break;
                    case 2: // Золото
                        int goldAmount = random.Next(20, 41);
                        gold += goldAmount;
                        Console.WriteLine($"Вы нашли {goldAmount} золота!");
                        break;
                    case 3: // Стрелы
                        int arrowAmount = random.Next(5, 11);
                        arrows += arrowAmount;
                        Console.WriteLine($"Вы нашли {arrowAmount} стрел!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неправильно! Сундук остался закрытым.");
            }
        }

        // торговец
        static void HandleTrader(ref int playerHealth, ref int gold, Item[] inventory, ref int inventorySize, ref int potions)
        {
            Console.WriteLine("\nВы встретили торговца.");
            Console.WriteLine("Он предлагает зелье здоровья за 30 золота (восстанавливает 20 HP).");
            Console.WriteLine($"У вас {gold} золота.");

            Console.Write("Купить зелье? (y/n): ");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "y")
            {
                if (gold >= 30)
                {
                    gold -= 30;
                    potions++;

                    Console.WriteLine("Вы купили зелье здоровья.");

                }
                else
                {
                    Console.WriteLine("Недостаточно золота!");
                }
            }
            else
            {
                Console.WriteLine("Вы решили ничего не покупать.");
            }
        }
    }
}