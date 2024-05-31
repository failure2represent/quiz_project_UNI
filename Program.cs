namespace quiz_project_UNI
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;

    class QuizQuestion
    {
        public string QuestionText { get; set; }       // текст вопроса
        public string[] Options { get; set; }          // массив с вариантами ответов
        public char CorrectOption { get; set; }        // правильные ответы

        public QuizQuestion(string questionText, string[] options, char correctOption)          // конструктор для приема
        {
            QuestionText = questionText;
            Options = options;
            CorrectOption = correctOption;
        }

        public bool Check(char selectedOption)        
        {
            return char.ToUpper(selectedOption) == char.ToUpper(CorrectOption);     // сравнение выбраного ответа с CorrectOption
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Welcome to the quiz!".PadLeft(75));     // welcome 
                Console.ResetColor();

                bool showMenu = true;

                while (showMenu)
                {
                    DisplayMenu();

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.G)
                    {
                        StartQuiz();
                        break;
                    }
                    else if (key == ConsoleKey.S)
                    {
                        DisplayScoreboard();
                        break;
                    }
                    else if (key == ConsoleKey.E)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nChybné zadávání. Stiskněte <G>, <S> nebo <E>.");
                        Console.ResetColor();
                        continue;
                    }
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("\nStiskněte <G> pro spuštění hry.");
            Console.WriteLine("\nStiskněte <S> pro zobrazení tabulky bodů (XML).");            // MeNu O_o
            Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");
        }
        static void StartQuiz()                 // метод инициализации "начать игру"
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nZadejte prosím svou přezdívku: ");                           // ввод пользователем "имени"
            Console.ResetColor();
            string playerName = Console.ReadLine();
            Console.Clear();
            

            QuizQuestion[] questions = ReadFromFile("questions.txt");              // считывание вопросов+вариантов+ответов из .txt, сохранение в массив questions
            if(questions.Length == 0 || questions == null)                          //  "|" оба       "||" 1 потом 2
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNepodařilo se načíst otázky z <questions.txt> !!!\n");
                Console.WriteLine($"Zkontrolujte, že soubor existuje a není prázdný.\n");           // проверка на "пустоту" файла
                Console.ResetColor();
                Console.WriteLine("Stiskněte <E> pro ukončení aplikace.");
                while (Console.ReadKey(true).Key != ConsoleKey.E)
                {
                    Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");                   // Повторное сообщение, если нажата другая клавиша
                }
                Environment.Exit(0);
            }

            int score = 0;
            int questionNumber = 1;

            Console.ForegroundColor = ConsoleColor.DarkYellow;                                      // приветствие игрока по нику перед 1м вопросом.
            Console.Write("\nAhoj, ");
            Console.ResetColor();
            Console.Write($"{playerName}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("! Kvíz je spuštěn ! Good luck !\n");
            Console.ResetColor();

            foreach (var question in questions)
            {
                Console.WriteLine();
                Console.WriteLine($"{questionNumber}. {question.QuestionText}\n");           // вывод номера + текста вопроса
                foreach (var option in question.Options)
                {
                    Console.WriteLine(option);                  // вывод вариантов
                }
                questionNumber++;
                char selectedOption;
                bool isValid = false;

                do        
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\nZadejte odpověď:");
                    Console.ResetColor();                                                                   // запрос на корректный формат ответа
                    char input = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    selectedOption = char.ToUpper(input);

                    if (selectedOption == 'A' || selectedOption == 'B' || selectedOption == 'C' || selectedOption == 'D')
                    {

                        isValid = true;
                        if (question.Check(selectedOption))
                        {
                            score++;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Správně!".PadLeft(30));
                            Console.ResetColor();
                            Console.WriteLine("\nPro pokračování stiskněte <Enter>.");                        // if правильный вариант
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Špatně!".PadLeft(30));
                            Console.ResetColor();                                                        //if неправильный вариант
                            Console.WriteLine("\nPro pokračování stiskněte <Enter>.");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nChybné zadávání. Zadejte a, b, c nebo d.");     // все любый input кроме а б с д
                    }
                    Console.ResetColor();

                } while (!isValid);
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nKonec hry, ");
            Console.ResetColor();
            Console.Write($"{playerName}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("!\n");

            Console.Write($"\nTvůj výsledek:");
            Console.ResetColor();
            Console.Write($"{score}");                                                   // Вывод результатов
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" z ");
            Console.ResetColor();
            Console.Write($"{questions.Length}\n");

            SaveScoreToXml(playerName, score);                                                        // вывод в xml
            DisplayMenu();                                                  
            
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.E)
                {
                    Environment.Exit(0);
                }
                else if (key == ConsoleKey.S)
                {
                    DisplayScoreboard();
                    break;
                }
                else if (key == ConsoleKey.G)
                {
                    StartQuiz();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nChybné zadávání. Stiskněte <G>, <S> nebo <E>.");
                    Console.ResetColor();
                    DisplayMenu();
                }
            }
        }
        static QuizQuestion[] ReadFromFile(string fileName)            // метод вывода вопросов из .txt 
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))            // считывание 6 строк в .txt файле (для ouput вопросов, вариантов и ответа)
                {
                    List<QuizQuestion> questionList = new List<QuizQuestion>();     // создание списка вопросов с контентом объекта QuizQuestion (динамическое кол-во вопросов)
                    while (!sr.EndOfStream)                                     // считывание (пока не будет конец списка)
                    {
                        string questionText = sr.ReadLine();                    // считывание 1й строки (вопрос)
                        string[] options = new string[4];                           // cчитывание 4х строк (варианты ответа)
                        for (int i = 0; i < 4; i++)
                        {
                            options[i] = sr.ReadLine();
                        }
                        char correctOption = char.ToUpper(sr.ReadLine()[0]);    // считывание 1 символа каждой 6й строчки в .txt (правильный ответ)

                        QuizQuestion question = new QuizQuestion(questionText, options, correctOption);            
                        questionList.Add(question);                                                                         
                    }
                    return questionList.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNepodařilo se načíst otázky z <questions.txt> !!!\n");
                Console.WriteLine($"Zkontrolujte, že soubor existuje a není prázdný.\n");           // проверка на "пустоту" файла
                Console.ResetColor();
                Console.WriteLine("Stiskněte <E> pro ukončení aplikace.");
                while (Console.ReadKey(true).Key != ConsoleKey.E)
                {
                    Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");                   // Повторное сообщение, если нажата другая клавиша
                }
                Environment.Exit(0);           // взял это из лекции (выводит какое-то сообщение при ошибки считывания файла)
                return null;
            }
        }
        static void SaveScoreToXml(string playerName, int score)            // метод сохранения score в XML 
        {
            XDocument doc;                                          //объявление переменной doc xml

            if (File.Exists(@"scoreboard.xml"))
            {
                doc = XDocument.Load(@"scoreboard.xml");                // загрузка предыдущего контента (если файл существует)                               
            }
            else doc = new XDocument(new XElement("Scoreboard"));       // создание файла scoreboard.xml (если файл отсутствует)

            doc.Root.Add(new XElement("Player", new XElement("Nickname", playerName), new XElement("Score", score)));           // добавление в коллекцию player 2 потомка (playerName и score)
            doc.Save(@"scoreboard.xml");                                        // save    
        }
        static void DisplayScoreboard()                    // метод вывода таблицы результатов из scoreboard.xml
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nTabulka bodů\n");
            Console.ResetColor();

            if (File.Exists(@"scoreboard.xml"))
            {
                XDocument doc = XDocument.Load(@"scoreboard.xml");          // загрузка файла scoreboard.xml
                var players = doc.Descendants("Player");                //загрузка коллекции player 

                foreach (var player in players)
                {
                    string nickname = player.Element("Nickname").Value;      // инициализация значения nickname(playerName)
                    string score = player.Element("Score").Value;           // инициализация значения score(score)
                    Console.WriteLine($"{nickname}: {score} body");        // вывод nickname и score
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nejsou k dispozici žádné výsledky k zobrazení.");              // проверка на отсутствие значений внутри файла
                Console.ResetColor(); 
            }

            Console.WriteLine("\n\nStiskněte <Enter> pro návrat do menu.");

            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nStiskněte <Enter> pro návrat do menu.");
                Console.ResetColor();
            }
            Console.Clear();
        }
    }
}