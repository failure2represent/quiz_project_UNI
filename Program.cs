namespace quiz_project_UNI
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;
    using Microsoft.VisualBasic.FileIO;

    class QuizQuestion
    {                                                  // Encapsulation (7 lekce)
        public string QuestionText { get; set; }       //  uložení textu otázky
        public string[] Options { get; set; }          //  array možností odpovědí
        public char CorrectOption { get; set; }        // správné odpovědi

        public QuizQuestion(string questionText, string[] options, char correctOption)          // konstruktoru, který přijímá parametry.
        {
            QuestionText = questionText;    
            Options = options;                                                           // inicializace příslušných vlastností třídy
            CorrectOption = correctOption;
        }

        public bool Check(char selectedOption)        
        {
            return char.ToUpper(selectedOption) == char.ToUpper(CorrectOption);     // check odpovědi
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            while (true)                                            // kontinuální provádění programu
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Welcome to the quiz!".PadLeft(75));              // welcome 
                Console.ResetColor();

                bool showMenu = true;

                while (showMenu)
                {
                    DisplayMenu();                                              // zobrazení menu prostřednictvím metody

                    var key = Console.ReadKey(true).Key;                        

                    if (key == ConsoleKey.G)
                    {
                        StartQuiz();                                            // G pro začátek 
                        break;  
                    }
                    else if (key == ConsoleKey.S)
                    {
                        DisplayScoreboard();                                        // S pro scoreboard
                        break;
                    }
                    else if (key == ConsoleKey.E)
                    {
                        Environment.Exit(0);                                // E pro nouzové uzavření 
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Welcome to the quiz!".PadLeft(75));              // Chybné zadání  
                        Console.ResetColor();
                        continue;
                    }
                }
            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("\nStiskněte <G> pro spuštění hry.");
            Console.WriteLine("\nStiskněte <S> pro zobrazení tabulky bodů (XML).");            //  metoda MeNu O_o
            Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");
        }
        static void StartQuiz()                 // metoda inicializace quizu
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nZadejte prosím svou přezdívku: ");                           // input přesdívky
            Console.ResetColor();
            string playerName = Console.ReadLine();
            Console.Clear();
            

            QuizQuestion[] questions = ReadFromFile("questions.txt");              // načtení otázek ze souboru questions.txt pomocí metody ReadFromFile.
            if (questions.Length == 0 || questions == null)                          
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNepodařilo se načíst otázky z <questions.txt> !!!\n");
                Console.WriteLine($"Zkontrolujte, že soubor existuje a není prázdný.\n");           // kontrola prázdnosti souboru
                Console.ResetColor();
                Console.WriteLine("Stiskněte <E> pro ukončení aplikace.");
                while (Console.ReadKey(true).Key != ConsoleKey.E)
                {
                    Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");                   // Opakování zprávy při stisknutí jiné klávesy
                }
                Environment.Exit(0);
            }

            int score = 0;
            int questionNumber = 1;

            Console.ForegroundColor = ConsoleColor.DarkYellow;                                      // pozdrav hráče před první otázkou.
            Console.Write("\nAhoj, ");
            Console.ResetColor();
            Console.Write($"{playerName}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("! Kvíz je spuštěn ! Good luck !\n");
            Console.ResetColor();

            foreach (var question in questions)
            {
                bool isValid = false;
                bool showError = false;
                while (!isValid)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"{questionNumber}. {question.QuestionText}\n");              // číslo + text otázky
                    foreach (var option in question.Options)
                    {
                        Console.WriteLine(option);                                // varianty
                    }

                    if (showError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nChybné zadávání. Zadejte a, b, c nebo d.");          // opatření jiného inputu kromě A B C D
                        Console.ResetColor();
                    }

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\nZadejte odpověď: ");
                    Console.ResetColor();
                    string input = Console.ReadLine();
                    Console.WriteLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        showError = true;
                        continue;
                    }

                    char selectedOption = char.ToUpper(input[0]);

                    if (selectedOption == 'A' || selectedOption == 'B' || selectedOption == 'C' || selectedOption == 'D')            // požadavek na správný formát odpovědi
                    {
                        isValid = true;
                        if (question.Check(selectedOption))
                        {
                            score++;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine();
                            Console.WriteLine("Správně!".PadLeft(30));                                                      // if spravná
                            Console.ResetColor();
                            Console.WriteLine("\nPro pokračování stiskněte <Enter>.");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Špatně!".PadLeft(30));                                                //if špatná
                            Console.ResetColor();
                            Console.WriteLine("\nPro pokračování stiskněte <Enter>.");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                    }
                    else
                    {
                        showError = true;
                    }
                }
                questionNumber++;
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
            Console.Write($"{score}");                                                   // display results
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($" z ");
            Console.ResetColor();
            Console.Write($"{questions.Length}\n");

            SaveScoreXml(playerName, score);                                                        // zápis do XML přes metodu SaveScoreXML
            DisplayMenu();                                                                          // zobrazení menu po hře
            
            while (true)                                                                            // kontinuální zobrazení menu
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
        static QuizQuestion[] ReadFromFile(string fileName)            // čte otázky a vrací pole objektů
        {
            try
            {                           
                using (StreamReader sr = new StreamReader(fileName))                 // Lekce 7
                {                                                                   // čtení 6 řádků v souboru .txt (pro výstupní otázky, možnosti a odpovědi)
                    List<QuizQuestion> questionList = new List<QuizQuestion>();     // Lekce 4 list pro ukládání objektů (dynamický počet otázek)
                    while (!sr.EndOfStream)                                    
                    {
                        string questionText = sr.ReadLine();                    // čtení 1 řadku (otázka)
                        string[] options = new string[4];                           // čtení čtyř řádků (možnosti odpovědí)
                        for (int i = 0; i < 4; i++)
                        {
                            options[i] = sr.ReadLine();
                        }
                        char correctOption = char.ToUpper(sr.ReadLine()[0]);    // čtení 1 znaku na každém 6. řádku v souboru .txt (správná odpověď)

                        QuizQuestion question = new QuizQuestion(questionText, options, correctOption);            
                        questionList.Add(question);                 // objekt s načtenými daty přidán do seznamu otázek.                                                        
                    }
                    return questionList.ToArray();                  // List otázek se převede na pole a vrátí se zpět
                }
            }
            catch (Exception ex)
            {                                                                       // Lekce 7 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nNepodařilo se načíst otázky z <questions.txt> !!!\n");
                Console.WriteLine($"Zkontrolujte, že soubor existuje a není prázdný.\n");           // handler - odchytnutí vyjímky při čtení z questions.txt
                Console.ResetColor();
                Console.WriteLine("Stiskněte <E> pro ukončení aplikace.");
                while (Console.ReadKey(true).Key != ConsoleKey.E)
                {
                    Console.WriteLine("\nStiskněte <E> pro ukončení aplikace.");                   
                }
                Environment.Exit(0);           // stisknutí E pro nouzové uzavření 
                return null;                   // vracení prazdné hodnoty
            }
        }
        static void SaveScoreXml(string playerName, int score)            // metoda ukládání skóre do XML 
        {
            XDocument doc;                                          // https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument?view=net-8.0

            if (File.Exists(@"scoreboard.xml"))
            {
                doc = XDocument.Load(@"scoreboard.xml");                // stahování předchozího obsahu (pokud soubor existuje)                              
            }
            else doc = new XDocument(new XElement("Scoreboard"));       // vytvoření souboru scoreboard.xml (pokud soubor chybí)

            doc.Root.Add(new XElement("Player", new XElement("Nickname", playerName), new XElement("Score", score)));           // přidání 2 potomků (playerName a score) do kolekce "player"
            doc.Save(@"scoreboard.xml");                                        // save    
        }
        static void DisplayScoreboard()                    // metoda výstupu tabulky výsledků z scoreboard.xml
        {
            bool isValid = false;
            while(!isValid){
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nTabulka bodů\n");
                Console.ResetColor();

                if (File.Exists(@"scoreboard.xml"))
                {
                    XDocument doc = XDocument.Load(@"scoreboard.xml");          // načtení souboru scoreboard.xml
                    var players = doc.Descendants("Player");                // inicializace hraču jako kolekce "player"

                    foreach (var player in players)
                    {
                        string nickname = player.Element("Nickname").Value;      // inicializace hodnoty nickname(playerName)
                        string score = player.Element("Score").Value;           // inicializace hodnoty score(skóre)
                        Console.WriteLine($"{nickname}: {score} body");        // output hodnoty nickname(playerName) a hodnoty score(skóre)
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nejsou k dispozici žádné výsledky k zobrazení.");              // kontrola chybějících hodnot uvnitř souboru
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\nStiskněte <Enter> pro návrat do menu.");
                Console.ResetColor();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    isValid = true;
                }
                
            }
            Console.Clear();
        }
    }
}