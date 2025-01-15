namespace quiz_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string playerName;
        private int playerScore;

        private void startButton_Click(object sender, EventArgs e)
        {
            playerScore = 0;

            startButton.Visible = false;
            scoreButton.Visible = false;
            exitButton.Visible = false;
            pictureBox1.Visible = false;

            usernameLabel.Visible = true;
            usernameTextBox.Visible = true;
            usernameOkButton.Visible = true;
            btmButton.Visible = true;

            nextQuButton.Visible = false;
            questionLabel.Visible = false;
            optionALabel.Visible = false;
            optionBLabel.Visible = false;
            optionCLabel.Visible = false;
            optionDLabel.Visible = false;
            answerTextBox.Visible = false;

            LoadQuestions("questions.txt");
            DisplayQuestion();
        }

        private void usernameOkButton_Click(object sender, EventArgs e)
        {
            playerName = usernameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Zadejte přezdívku!", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            usernameLabel.Visible = false;
            usernameTextBox.Visible = false;
            usernameOkButton.Visible = false;

            btmButton.Visible = true;
            nextQuButton.Visible = true;
            questionLabel.Visible = true;
            optionALabel.Visible = true;
            optionBLabel.Visible = true;
            optionCLabel.Visible = true;
            optionDLabel.Visible = true;
            answerTextBox.Visible = true;

            playerScore = 0;

            LoadQuestions("questions.txt");
            DisplayQuestion();
        }

        private List<string[]> questionsList;
        private int currentQuestionIndex = 0;

        private void SaveScoreToXml(string playerName, int score)
        {
            string fileName = "scoreboard.xml";

            var doc = System.Xml.Linq.XDocument.Load(fileName);

            doc.Root.Add(new System.Xml.Linq.XElement("Player",
                new System.Xml.Linq.XElement("Nickname", playerName),
                new System.Xml.Linq.XElement("Score", score)
            ));

            doc.Save(fileName);
        }

        private void LoadQuestions(string fileName)
        {
            questionsList = new List<string[]>();

            if (System.IO.File.Exists(fileName))
            {
                var lines = System.IO.File.ReadAllLines(fileName);

                for (int i = 0; i < lines.Length; i += 6)
                {
                    if (i + 5 < lines.Length)
                    {
                        string question = lines[i].Trim();
                        string optionA = lines[i + 1].Trim();
                        string optionB = lines[i + 2].Trim();
                        string optionC = lines[i + 3].Trim();
                        string optionD = lines[i + 4].Trim();
                        string correctAnswer = lines[i + 5].Trim().ToUpper();

                        questionsList.Add(new string[] { question, optionA, optionB, optionC, optionD, correctAnswer });
                    }
                }
            }
            else
            {
                MessageBox.Show("Soubor s otázkou nebyl nalezen!", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < questionsList.Count)
            {
                var currentQuestion = questionsList[currentQuestionIndex];

                questionLabel.Text = currentQuestion[0];
                optionALabel.Text = $"A: {currentQuestion[1]}";
                optionBLabel.Text = $"B: {currentQuestion[2]}";
                optionCLabel.Text = $"C: {currentQuestion[3]}";
                optionDLabel.Text = $"D: {currentQuestion[4]}";
            }
        }

        private void ResetQuiz()
        {
            nextQuButton.Visible = false;
            questionLabel.Visible = false;
            optionALabel.Visible = false;
            optionBLabel.Visible = false;
            optionCLabel.Visible = false;
            optionDLabel.Visible = false;
            answerTextBox.Visible = false;

            startButton.Visible = true;
            scoreButton.Visible = true;
            exitButton.Visible = true;
            pictureBox1.Visible = true;

            currentQuestionIndex = 0;
            usernameTextBox.Text = "";
            playerName = "";
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            scoreButton.Visible = false;
            exitButton.Visible = false;
            pictureBox1.Visible = false;
            sbGrid.Visible = true;
            btmButton.Visible = true;

            DisplayScoreboard();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btmButton_Click(object sender, EventArgs e)
        {
            sbGrid.Visible = false;
            btmButton.Visible = false;
            nextQuButton.Visible = false;
            questionLabel.Visible = false;
            optionALabel.Visible = false;
            optionBLabel.Visible = false;
            optionCLabel.Visible = false;
            optionDLabel.Visible = false;
            answerTextBox.Visible = false;
            usernameOkButton.Visible = false;
            usernameLabel.Visible = false;
            usernameTextBox.Visible = false;


            pictureBox1.Visible = true;
            startButton.Visible = true;
            scoreButton.Visible = true;
            exitButton.Visible = true;
            ResetQuiz();
        }

        private void DisplayScoreboard()
        {
            string fileName = "scoreboard.xml";

            if (System.IO.File.Exists(fileName))
            {
                System.Data.DataSet dataSet = new System.Data.DataSet();
                dataSet.ReadXml(fileName);

                sbGrid.AllowUserToAddRows = false;
                sbGrid.RowHeadersVisible = false;
                sbGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                sbGrid.DataSource = dataSet.Tables[0];
            }
        }

        private void nextQuButton_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questionsList.Count)
            {
                var currentQuestion = questionsList[currentQuestionIndex];
                string correctAnswer = currentQuestion[5];
                string userAnswer = answerTextBox.Text.Trim().ToUpper();

                if (userAnswer == correctAnswer)
                {
                    playerScore++;
                    MessageBox.Show("Správně!", "Odpověď");
                }
                else
                {
                    MessageBox.Show($"Špatně! Správná odpověď je: {correctAnswer}", "Odpověď");
                }

                answerTextBox.Text = "";
                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                
                SaveScoreToXml(playerName, playerScore);

                MessageBox.Show($"Dokončili jste kvíz! Váš výsledek: {playerScore} bodů.", "Ukončení");

                sbGrid.Visible = true;
                btmButton.Visible = true;

                startButton.Visible = false;
                scoreButton.Visible = false;
                exitButton.Visible = false;
                pictureBox1.Visible = false;
                nextQuButton.Visible = false;
                questionLabel.Visible = false;
                optionALabel.Visible = false;
                optionBLabel.Visible = false;
                optionCLabel.Visible = false;
                optionDLabel.Visible = false;
                answerTextBox.Visible = false;

                DisplayScoreboard();

            }
        }


    }
}
