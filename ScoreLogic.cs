using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace quiz_gui
{
    public class ScoreLogic
    {
        private readonly string fileName = "scoreboard.xml";

        public void SaveScoreToXml(string playerName, int score)   // metoda ukládání skóre do XML 
        {
            XDocument doc;                                          // https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument?view=net-8.0

            if (File.Exists(fileName))
            {
                doc = XDocument.Load(fileName);                     // stahování předchozího obsahu (pokud soubor existuje)
            }
            else
            {
                doc = new XDocument(new XElement("Scoreboard"));           // vytvoření souboru scoreboard.xml (pokud soubor chybí)
            }

            doc.Root.Add(new XElement("Player",                                  // přidání 2 potomků (playerName a score) do kolekce "player"
                new XElement("Nickname", playerName),
                new XElement("Score", score)
            ));

            doc.Save(fileName);                                             // save 
        }

        public void LoadScoreboard(DataGridView sbGrid)                              // metoda nahrání skóre xml 
        {
            if (!File.Exists(fileName))                                             // kontrola existence
            {
                XDocument newDoc = new XDocument(new XElement("Scoreboard"));       // vytvoření souboru scoreboard.xml (pokud soubor chybí)
                newDoc.Save(fileName);                                              // save
            }

            DataSet dataSet = new DataSet();                                            // Vytvoření objektu DataSet pro načítání
            dataSet.ReadXml(fileName);                                              //načítání z XML

            if (dataSet.Tables.Count == 0)                                          // kontrola
            {
                DataTable dt = new DataTable();                                     //nová table
                dt.Columns.Add("Nickname");
                dt.Columns.Add("Score");
                sbGrid.DataSource = dt;                                             //  přiřazení v DataGridView
            }
            else
            {
                sbGrid.DataSource = dataSet.Tables[0];                              //načítání v DataGridView
            }

        }
    }
}
