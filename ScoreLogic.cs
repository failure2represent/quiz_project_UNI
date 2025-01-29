using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace quiz_gui
{
    public class ScoreLogic
    {
        private readonly string fileName = "scoreboard.xml";

        public void SaveScoreToXml(string playerName, int score)
        {
            XDocument doc;

            if (File.Exists(fileName))
            {
                doc = XDocument.Load(fileName);
            }
            else
            {
                doc = new XDocument(new XElement("Scoreboard"));
            }

            doc.Root.Add(new XElement("Player",
                new XElement("Nickname", playerName),
                new XElement("Score", score)
            ));

            doc.Save(fileName);
        }

        public void LoadScoreboard(DataGridView sbGrid)
        {
            if (!File.Exists(fileName))
            {
                XDocument newDoc = new XDocument(new XElement("Scoreboard"));
                newDoc.Save(fileName);
            }

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(fileName);

            if (dataSet.Tables.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Nickname");
                dt.Columns.Add("Score");
                sbGrid.DataSource = dt; 
            }
            else
            {
                sbGrid.DataSource = dataSet.Tables[0]; 
            }

        }
    }
}
