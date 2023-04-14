using ExtractMavenTestResults;

class Program
{ 
    private static void Main(string[] args)
    {
        Config.getInstance().parseArgs(args);

        List<Model> results = DirectorySearches.agregateTestResults();

        FileSerializer.writeCsv(results, "result.csv");
    }
}




