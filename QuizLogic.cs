using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace quiz_gui
{
    public class QuizLogic
    {
        private List<string[]> questionsList;
        private int currentQuestionIndex;

        public QuizLogic()
        {
            questionsList = [];
            currentQuestionIndex = 0;
        }

        public void LoadQuestions(string fileName)
        {
            questionsList.Clear();

            if (File.Exists(fileName))
            {
                var lines = File.ReadAllLines(fileName);

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

                        questionsList.Add([question, optionA, optionB, optionC, optionD, correctAnswer]);
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Soubor s otázkou nebyl nalezen!", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(result == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        public string[] GetCurrentQuestion()
        {
            if (currentQuestionIndex < questionsList.Count)
            {
                return questionsList[currentQuestionIndex];
            }
            return null;
        }

        public void NextQuestion()
        {
            currentQuestionIndex++;
        }

        public bool HasQuestions()
        {
            return currentQuestionIndex < questionsList.Count;
        }

        public void ResetQuiz()
        {
            currentQuestionIndex = 0;
        }
    }
}
