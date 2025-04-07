using System.Text;
using System.IO;

namespace Lieferverwaltung
{
    class Program
    {
        static List<Lieferung> lieferungen = new List<Lieferung>();

        static void Main(string[] args)
        {
            BeispielobjekteAnlegen();
            Console.WriteLine(lieferungen.Count);
            string json = ErstelleJson();
            File.WriteAllText("lieferungen.json", json);
            Console.WriteLine("JSON-Datei wurde erstellt.");
        }

        static void BeispielobjekteAnlegen()
        {
            lieferungen.Add(new Lieferung(
                new DateOnly(2024, 06, 22),
                "HHX05NNW0ZP",
                "86309"
            ));

            lieferungen.Add(new Lieferung(
                new DateOnly(2024, 09, 4),
                "GSV18EDC4BR",
                "91139"
            ));

            lieferungen.Add(new Lieferung(
                new DateOnly(2023, 04, 8),
                "CQX55KMY5RW",
                "07708"
            ));
        }

        static string ErstelleJson()
        {
            StringBuilder json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("\t\"anzahl\": " + lieferungen.Count + ",");
            json.AppendLine("\t\"lieferungen\": \n\t[");

            for (int i = 0; i < lieferungen.Count; i++)
            {
                Lieferung lieferung = lieferungen[i];
                string datum = lieferung.Datum.ToString("yyyy-MM-d");
                int plz = Convert.ToInt32(lieferung.PLZ);

                json.AppendLine("\t\t{");
                json.AppendLine($"\t\t\t\"datum\": \"{datum}\",");
                json.AppendLine($"\t\t\t\"sendungsnummer\": \"{lieferung.Sendungsnummer}\",");
                json.AppendLine($"\t\t\t\"plz\": {plz}");
                json.Append("\t\t}");

                if (i < lieferungen.Count - 1)
                {
                    json.AppendLine(",");
                }
            }

            json.AppendLine("\n\t]");
            json.AppendLine("}");
            return json.ToString();
        }
    }
}