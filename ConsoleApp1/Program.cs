namespace Pratica_8_RPG
{
    internal class Program
    {
        public static int playerHealth;
        public static int playerGold;
        public static int swordDamageAmount = 10;
        public static int bowDamageAmount = 8;
        public static bool gameRunning;
        public static int potionCount;
        public static int arrowCount;

        public static int[] roomTypes = new int[15];
        static void Main(string[] args)
        {
            StartGame();

            InitializeGame();
            if (gameRunning == true)
            {
                ProcessRoom();
            }
            if (gameRunning == true)
            {
                FightBoss();
            }
        }
        public static void InitializeGame()
        {
            playerHealth = 100;
            playerGold = 10;
            arrowCount = 5;
            potionCount = 5;

        }
        public static void StartGame()
        {
            gameRunning = true;
            Random random = new Random();
            for (int i = 0; i < roomTypes.Length; i++)
            {
                roomTypes[i] = random.Next(1, 6);
            }

        }
        public static void ProcessRoom()
        {

            for (int i = 0; i < roomTypes.Length; i++)
            {
                switch (roomTypes[i])
                {
                    case 1:
                        ShowStats();
                        FightMonster();
                        break;
                    case 2:
                        ShowStats();
                        OpenChest();
                        break;
                    case 3:
                        ShowStats();
                        VisitAltar();
                        break;
                    case 4:
                        ShowStats();
                        MeetDarkMage();
                        break;
                    case 5:
                        ShowStats();
                        VisitMerchant();
                        break;
                    case 6:
                        {
                            ShowStats();
                            riddle();
                            break;
                        }
                }
                if (playerHealth <= 0)
                {
                    Console.WriteLine("Вы проиграли");
                    EndGame();
                    return;
                }
            }
        }

        public static void FightMonster()
        {
            Random random = new Random();
            int monsterHP = random.Next(30, 100);

            Console.WriteLine("Монстр!");
            Console.WriteLine("У монстра " + monsterHP + " здоровья");
            while (monsterHP > 0)
            {
                int monsterAttack = random.Next(10, 20);


                bool b = true;
                while (b == true)
                {
                    Console.WriteLine("(1) Атака  мечем, (2) Атака луком");
                    string a = Console.ReadLine();

                    {
                        switch (a)
                        {
                            case "1":
                                monsterHP -= swordDamageAmount;
                                b = false;
                                break;
                            case "2":
                                if (arrowCount > 0)
                                {
                                    monsterHP -= bowDamageAmount;
                                    arrowCount -= 1;
                                    b = false;
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно стрел");
                                }
                                break;

                            default:

                                Console.WriteLine("Выбор не коректен");
                                break;
                        }

                    }

                }
                if (monsterHP > 0)
                {
                    Console.WriteLine("У монстра осталось " + monsterHP + " здоровья");
                    playerHealth -= monsterAttack;
                    // ограничение
                    if (playerHealth < 0)
                    {
                        playerHealth = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Вы победили монстра");
                    UsePotion();
                    return;
                }

                if (playerHealth > 0)
                {
                    Console.WriteLine("Монстр нанес вам " + monsterAttack + " У вас осталось " + playerHealth);
                }
                else
                {
                    Console.WriteLine("Вы проиграли");
                    EndGame();
                    return;
                }



            }


        }
        public static void OpenChest()
        {


            Console.WriteLine("Проклятый сундук!");
            Console.WriteLine("Потратить 10 здоровья что бы открыть? (1) да (2) пройти мимо");
            bool b = true;
            while (b == true)
            {
                string r = Console.ReadLine();
                switch (r)
                {
                    case "1":
                        if (playerHealth >= 10)
                        {
                            playerHealth -= 10;
                            b = false;
                            playerGold += 15;
                            if (playerHealth < 0)
                            {
                                playerHealth = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не достаточно здоровья");
                        }
                        break;
                    case "2":
                        b = false;
                        break;
                    default:

                        Console.WriteLine("Выбор не коректен");
                        break;

                }
            }
        }
        public static void VisitMerchant()
        {
            Console.WriteLine("Перед вами торговец");
            Console.WriteLine("Можно купить зелья (10 золота,нажмите 1) или стрелы (5 золота за 3 штуки,нажмите 2)");



            string a = Console.ReadLine();
            switch (a)
            {
                case "1":
                    if (playerGold >= 10)
                    {
                        potionCount += 2;
                        playerGold -= 10;

                    }
                    else
                    {
                        Console.WriteLine("Не достаточно золота");
                    }
                    break;
                case "2":
                    if (playerGold >= 5)
                    {
                        arrowCount += 3;
                        playerGold -= 5;
                    }
                    else
                    {
                        Console.WriteLine("Не достаточно золота");
                    }

                    break;
                default:
                    break;

            }
        }
        public static void Bonus()
        {
            Console.WriteLine("Вы посетили бонеусную комнату!");
            Random random = new Random();
            int a = random.Next(0, 2);
            switch (a)
            {
                case 0:
                    swordDamageAmount += 5;
                    Console.WriteLine("Урон меча увеличен на 5");
                    break;
                case 1:
                    bowDamageAmount += 5;
                    Console.WriteLine("Урон лука увеличен на 5");
                    break;
                case 2:
                    playerHealth += 10;
                    Console.WriteLine("Ваше здоровье увеличено на 10");
                    break;
            }

        }
        public static void riddle()
        {
            Console.WriteLine("Сундук!");
            int a = 3;
            int b = 1;
            Random rnd = new Random();
            int c = rnd.Next(1, 3);

            if (c == 1)
            {
                Console.WriteLine("1+2=?");
                int p = Convert.ToInt32(Console.ReadLine());
                if (p == a)
                {
                    playerGold += 10;
                    Console.WriteLine("Сундук открыт вы получили 10 золота");
                    return;
                }
                else { Console.WriteLine("Не удалось открыть"); }
            }
            else if (c == 2)
            {
                Console.WriteLine("3-2=?");
                int p = Convert.ToInt32(Console.ReadLine());
                if (p == b)
                {
                    playerGold += 10;
                    Console.WriteLine("Сундук открыт вы получили 10 золота");
                    return;
                }
                else { Console.WriteLine("Не удалось открыть"); }
            }
        }
        public static void MeetDarkMage()
        {
            Console.WriteLine("Вы встретились с темным магом");
            Console.WriteLine("Он предлагает сделку ваши 10 здоровья за 2 зелья и 5 стрел");
            Console.WriteLine("(1)согласится (2) нет ");

            bool b = true;
            while (b == true)
            {
                string a = Console.ReadLine();
                switch (a)
                {
                    case "1":
                        if (playerHealth >= 10)
                        {
                            playerHealth -= 10;
                            b = false;
                            potionCount += 2;
                            arrowCount += 5;
                            if (playerHealth < 0)
                            {
                                playerHealth = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не достаточно здоровья");
                        }
                        break;
                    case "2":
                        b = false;
                        break;
                    default:
                        Console.WriteLine("Выбор не коректен");
                        break;

                }
            }
        }
        public static void VisitAltar()
        {
            Console.WriteLine("Вы пришли к алтарю");
            if (playerGold < 10)
            {
                Console.WriteLine("Кажется ничего не происходит");
            }
            else
            {
                Console.WriteLine("Можете потратить 10 золота (нажмите 1) что бы увеличить урон меча, восстановить здоровье или пройити мимо (нажмите 2) ");


                bool b = true;
                while (b == true)
                {
                    string a = Console.ReadLine();
                    switch (a)
                    {
                        case "1":
                            Console.WriteLine("(1) улучшить урон, (2) восстановить здоровье");

                            bool d = true;
                            while (d == true)
                            {
                                string c = Console.ReadLine();
                                switch (c)
                                {
                                    case "1":
                                        swordDamageAmount += 5;
                                        playerGold -= 5;
                                        d = false;
                                        break;
                                    case "2":
                                        if (playerHealth < 80)
                                        {
                                            playerHealth += 20;
                                            d = false;
                                        }
                                        else if (playerHealth < 100 && playerHealth >= 80)
                                        {
                                            playerHealth = 100;
                                            d = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Здоровье полное");
                                        }
                                        break;
                                }

                            }
                            b = false;
                            break;
                        case "2":
                            b = false;
                            break;
                        default:

                            Console.WriteLine("Выбор не коректен");
                            break;
                    }

                }
            }
        }
        public static void UsePotion()
        {
            if (potionCount > 0)
            {
                Console.WriteLine("Нажмите (1) что б воссатановить здоровье или любую другую кнопку если не хотите");
                string a = Console.ReadLine();
                switch (a)
                {
                    case "1":
                        potionCount--;
                        playerHealth += 30;
                        if (playerHealth > 100)
                        {
                            playerHealth = 100;
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        public static void FightBoss()
        {
            int ultimate = 0;
            Console.WriteLine("Босс!");

            Random random = new Random();
            int bossHp = 100;
            Console.WriteLine("У босса " + bossHp + " здоровья");

            while (bossHp > 0 && playerHealth > 0)
            {
                int monsterAttack = random.Next(10, 20);


                bool b = true;
                while (b == true)
                {
                    Console.WriteLine("(1) Атака  мечем, (2) Атака луком");
                    string a = Console.ReadLine();
                    {
                        switch (a)
                        {
                            case "1":
                                bossHp -= swordDamageAmount;
                                b = false;
                                break;
                            case "2":
                                if (arrowCount > 0)
                                {
                                    bossHp -= bowDamageAmount;
                                    arrowCount -= 1;
                                    b = false;
                                }
                                else
                                {
                                    Console.WriteLine("Недостаточно стрел");
                                }
                                break;
                            default:

                                Console.WriteLine("Выбор не коректен");
                                break;
                        }

                    }

                }

                if (bossHp > 0)
                {
                    Console.WriteLine("У монстра осталось " + bossHp + " здоровья");

                    ultimate++;
                    if (ultimate == 3)
                    {
                        ultimate = 0;
                        playerHealth -= monsterAttack * 2;
                    }
                    else
                    {
                        playerHealth -= monsterAttack;
                    }
                    if (playerHealth < 0)
                    {
                        playerHealth = 0;
                    }

                }
                else
                {
                    Console.WriteLine("Вы победили");
                    UsePotion();
                    return;
                }
                if (playerHealth > 0)
                {
                    if (ultimate == 3)
                    {
                        Console.WriteLine("Монстр нанес вам " + monsterAttack * 2 + " У вас осталось " + playerHealth);
                    }
                    else
                    {
                        Console.WriteLine("Монстр нанес вам " + monsterAttack + " У вас осталось " + playerHealth);
                    }

                }
                else
                {
                    Console.WriteLine("Вы проиграли");
                    EndGame();
                    return;
                }
            }
        }
        public static void EndGame()
        {
            gameRunning = false; 
        }
        public static void ShowStats()
        {
            Console.WriteLine("здоровье " + playerHealth);
            Console.WriteLine("стрелы " + arrowCount);
            Console.WriteLine("зелья " + potionCount);
            Console.WriteLine("золото " + playerGold);
        }

    }
}