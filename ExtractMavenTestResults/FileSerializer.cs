using CsvHelper;
using System.Collections;
using System.Globalization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ExtractMavenTestResults
{
    internal class FileSerializer
    {
        public static void writeCsv(object content, string filename)
        {
            using (var writer = new StreamWriter(filename))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (content is IEnumerable)
                    csv.WriteRecords((IEnumerable)content);
                else
                    csv.WriteRecord(content);
            }
        }

        public static void writeYaml(object content, string filename)
        {
            var serializer = new SerializerBuilder()
                //.WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(content);
            File.WriteAllText(filename, yaml);
        }

        public static T readYaml<T>(string path)
        { 
            var deserializer = new DeserializerBuilder()
                                    //.WithNamingConvention(CamelCaseNamingConvention.Instance)
                                    .Build();
            return deserializer.Deserialize<T>(File.ReadAllText(path));
        }
    }
}
