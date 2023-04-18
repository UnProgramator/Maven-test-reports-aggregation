
using System.Globalization;
using System.Xml;

namespace ExtractMavenTestResults;

class TestResultParser
{
    public static void readResults(string docPath, List<Model> models)
    {
        XmlDocument xmlDoc = new XmlDocument();
        bool display = Config.getInstance().printScreen;

        xmlDoc.Load(docPath);

        foreach(XmlNode readLines in xmlDoc.GetElementsByTagName("testsuite"))
        {
            var atribs = readLines.Attributes;
            if (atribs == null)
            { // exception: no atributes => move along, for now
                Console.WriteLine("node " + readLines.ToString() + " is taged testsuite, but has no atributes");
                continue;
            }
            Model elem = new Model();
            elem.testClassName = atribs["name"].InnerText;
            elem.testNumber = int.Parse(atribs["tests"].InnerText);
            elem.errors     = int.Parse(atribs["errors"].InnerText);
            elem.skipped    = int.Parse(atribs["skipped"].InnerText);
            elem.failures   = int.Parse(atribs["failures"].InnerText);
            elem.time       = double.Parse(atribs["time"].InnerText, CultureInfo.InvariantCulture);

            elem.generateCandidateName();

            if (display)
                Console.WriteLine(elem.toString());

            models.Add(elem);
        }
    }
}
