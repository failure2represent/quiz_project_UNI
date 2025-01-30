using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace quiz_gui
{
    public class QuizLogic
    {
        private List<string[]> questionsList;                   // list otazek
        private int currentQuestionIndex;                       // index stávající otazky

        public QuizLogic()                                      //konstrukce (prazdný list, 0 index otazky)
        {
            questionsList = [];                                 
            currentQuestionIndex = 0;
        }

        public void LoadQuestions(string fileName)                      //načitání otázky z textového souboru 
        {
            questionsList.Clear();                                          // vymazání před nahráním 

            if (File.Exists(fileName))                                         //kontrola existence
            {
                var lines = File.ReadAllLines(fileName);                //čtení všech řádků

                for (int i = 0; i < lines.Length; i += 6)                   //čtení po 6 řádků
                {
                    if (i + 5 < lines.Length)
                    {
                        string question = lines[i].Trim();                      //řádek otazky
                        string optionA = lines[i + 1].Trim();                       //řádek A
                        string optionB = lines[i + 2].Trim();                                       //řádek B
                        string optionC = lines[i + 3].Trim();                       //řádek C
                        string optionD = lines[i + 4].Trim();                       //řádek D
                        string correctAnswer = lines[i + 5].Trim().ToUpper();           //spravná odpoved'

                        questionsList.Add([question, optionA, optionB, optionC, optionD, correctAnswer]); //přidání otázek
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Soubor s otázkou nebyl nalezen!", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(result == DialogResult.OK)                                           // opatření
                {
                    Application.Exit();
                }
            }
        }

        public string[] GetCurrentQuestion()                                    // získání aktuální otazky
        {
            if (currentQuestionIndex < questionsList.Count)         //kontorla 
            {
                return questionsList[currentQuestionIndex];         //vrácení aktuální otázky
            }
            return null;                    // Vrácení null (pokud není další otazka)
        }

        public void NextQuestion()                  // next otazka
        {
            currentQuestionIndex++;
        }

        public bool HasQuestions()                              //kontrola zbytku otazek
        {
            return currentQuestionIndex < questionsList.Count;
        }

        public void ResetQuiz()                             //reset indexu
        {
            currentQuestionIndex = 0;
        }
    }
}
