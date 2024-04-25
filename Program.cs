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
            Console.WriteLine("\nДобро пожаловать в викторину!\n");                       // приветствие и ввод пользователем "имени"
            Console.Write("Пожалуйста, введите ваш никнейм: ");
            string playerName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"\nПривет, {playerName}! Давай же начнем викторину!");


            QuizQuestion[] questions = ReadFromFile("questions.txt");              // считывание вопросов+вариантов+ответов из .txt, сохранение в массив questions
            int score = 0;
            int questionNumber = 1;

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

                do        // запрос на корректный формат ответа
                {
                    Console.Write("\nВведите ваш ответ (a, b, c, d): ");
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
                            Console.WriteLine("Верно!".PadLeft(30));
                            Console.ResetColor();
                            Console.WriteLine("\nНажмите <Enter>, чтобы продолжить.");                        // if правильный вариант
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Неверно!".PadLeft(30));
                            Console.ResetColor();                                                        //if неправильный вариант
                            Console.WriteLine("\nНажмите <Enter>, чтобы продолжить.");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nНекорректный ввод. Пожалуйста, введите a, b, c или d.");     // все любый input кроме а б с д
                    }
                    Console.ResetColor();

                } while (!isValid);
                Console.Clear();
            }
            
            Console.WriteLine($"Игра окончена, {playerName}!\n");
            Console.WriteLine($"Ваш результат: {score} из {questions.Length}");                       // Вывод результатов
            SaveScoreToXml(playerName, score);                                                        // вывод в xml
            Console.WriteLine("\nНажмите <Enter>, чтобы закрыть приложение.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nПожалуйста! Нажмите <Enter>, чтобы закрыть приложение.");
            }
        }

        private static QuizQuestion[] ReadFromFile(string fileName)            // метод вывода вопросов из .txt 
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
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");            // взял это из лекции (выводит какое-то сообщение при ошибки считывания файла)
                return null;
            }
        }
        static void SaveScoreToXml(string playerName, int score)
        {

            XDocument doc;

            if (File.Exists(@"scoreboard.xml"))
            {
                doc = XDocument.Load(@"scoreboard.xml");
            }
            else doc = new XDocument(new XElement("Scoreboard"));

            doc.Root.Add(new XElement("Player", new XElement("Nickname", playerName), new XElement("Score", score)));
            doc.Save(@"scoreboard.xml");
        }

    /* 

            
    XmlWriterSettings set = new XmlWriterSettings();
        set.Indent = true; 

        using (XmlWriter xw = XmlWriter.Create(@"scoreboard.xml", set))
        {
            xw.WriteStartDocument();
            xw.WriteStartElement("Scoreboard");

            xw.WriteStartElement("Player");
            xw.WriteElementString("Nickname", playerName);
            xw.WriteElementString("Score", score.ToString());
            xw.WriteEndElement();

            xw.WriteEndElement();
            xw.WriteEndDocument();
        }
    */
}
}