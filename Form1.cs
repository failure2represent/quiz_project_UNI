using System;
using System.Windows.Forms;

namespace quiz_gui
{
    public partial class Form1 : Form
    {
        private string playerName;
        private int playerScore;
        private QuizLogic quizLogic;
        private ScoreLogic scoreLogic;

        public Form1()
        {
            InitializeComponent();
            quizLogic = new QuizLogic();
            scoreLogic = new ScoreLogic();

            if (sbGrid != null)
            {
                sbGrid.ColumnAdded += (s, e) => e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            ResetGame();
            ShowNicknameInput();
        }

        private void usernameOkButton_Click(object sender, EventArgs e)
        {
            playerName = usernameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Zadejte přezdívku!", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowQuizElements();
            quizLogic.LoadQuestions("questions.txt");
            DisplayQuestion();
        }

        private void nextQuButton_Click(object sender, EventArgs e)
        {
            if (quizLogic.HasQuestions())
            {
                var currentQuestion = quizLogic.GetCurrentQuestion();
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
                quizLogic.NextQuestion();
                DisplayQuestion();
            }
            else
            {
                scoreLogic.SaveScoreToXml(playerName, playerScore);
                DisplayScoreboard();
                MessageBox.Show($"Dokončili jste kvíz! Váš výsledek: {playerScore} bodů.", "Ukončení");
                VisualizeScoreboard();
            }
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            HideAllElements();
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
            ResetQuiz();
            ShowMainMenu();
        }

        private void DisplayScoreboard()
        {
            scoreLogic.LoadScoreboard(sbGrid);
        }

        private void DisplayQuestion()
        {
            var currentQuestion = quizLogic.GetCurrentQuestion();

            if (currentQuestion != null)
            {
                questionLabel.Text = currentQuestion[0];
                optionALabel.Text = $"A: {currentQuestion[1]}";
                optionBLabel.Text = $"B: {currentQuestion[2]}";
                optionCLabel.Text = $"C: {currentQuestion[3]}";
                optionDLabel.Text = $"D: {currentQuestion[4]}";
            }
        }

        private void ResetQuiz()
        {
            HideAllElements();
            quizLogic.ResetQuiz();
            usernameTextBox.Text = "";
            playerName = "";
        }

        private void ResetGame()
        {
            playerScore = 0;
            quizLogic.ResetQuiz();
        }

        private void ShowMainMenu()
        {
            startButton.Visible = true;
            scoreButton.Visible = true;
            exitButton.Visible = true;
            pictureBox1.Visible = true;
            sbGrid.Visible = false;
            btmButton.Visible = false;
        }

        private void HideAllElements()
        {
            usernameLabel.Visible = false;
            usernameTextBox.Visible = false;
            usernameOkButton.Visible = false;
            questionLabel.Visible = false;
            optionALabel.Visible = false;
            optionBLabel.Visible = false;
            optionCLabel.Visible = false;
            optionDLabel.Visible = false;
            answerTextBox.Visible = false;
            nextQuButton.Visible = false;
            sbGrid.Visible = false;
            btmButton.Visible = false;
            pictureBox1.Visible = false;
            startButton.Visible = false;
            scoreButton.Visible = false;
            exitButton.Visible = false;
        }

        private void ShowNicknameInput()
        {
            HideAllElements();
            usernameLabel.Visible = true;
            usernameTextBox.Visible = true;
            usernameOkButton.Visible = true;
            btmButton.Visible = true;
        }

        private void ShowQuizElements()
        {
            HideAllElements();
            questionLabel.Visible = true;
            optionALabel.Visible = true;
            optionBLabel.Visible = true;
            optionCLabel.Visible = true;
            optionDLabel.Visible = true;
            answerTextBox.Visible = true;
            nextQuButton.Visible = true;
        }

        private void VisualizeScoreboard()
        {
            HideAllElements();
            sbGrid.Visible = true;
            btmButton.Visible = true;
        }
    }
}