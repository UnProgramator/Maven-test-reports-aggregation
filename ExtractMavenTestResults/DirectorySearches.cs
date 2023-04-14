namespace ExtractMavenTestResults;

class DirectorySearches
{
    public static List<Model> agregateTestResults()
    {
        var results = new List<Model>();

        var config = ProjectConfig.getConfig();

        if (config.fullSearch ?? false)
            DirectorySearches.parseAllDirectoriesFromRoot(config.rootDirectory, results);

        else if (config.subdirectories is not null)
            DirectorySearches.searchDirectories(config.rootDirectory, config.subdirectories, results);

        else if (config.standardSearch ?? true)
            DirectorySearches.searchForDirectories(config.rootDirectory, results);

        else DirectorySearches.parseDirectory(config.rootDirectory, results);

        return results;
    }

    private static void parseAllDirectoriesFromRoot(string root, List<Model> results)
    {
        foreach (var files in Directory.EnumerateFiles(root, "*Test-*.xml", SearchOption.AllDirectories))
        {
            TestResultParser.readResults(files, results);
        }
    }

    private static void searchDirectories(string root, string[] directories, List<Model> results)
    {
        foreach (string dirName in directories)
        {
            parseDirectory(root + "/" + dirName, results);
        }
    }

    private static void searchForDirectories(string root, List<Model> results)
    {
        foreach (var dir in Directory.GetDirectories(root))
        {
            var target = Directory.GetDirectories(dir, "target");
            if (target.Length == 0)
                continue; // no target
            if (target.Length > 1) // if the target dir is not on target[0] them move it on target[0], if it exists at all
            {
                string tDir = target[0];
                if(!tDir.EndsWith("/target") && !tDir.EndsWith("\\target"))
                    for(int i=1; i<target.Length; i++)
                    {
                        tDir = target[i];
                        if (tDir.EndsWith("/target") || tDir.EndsWith("\\target"))
                        {
                            target[0] = tDir;
                        }
                    }
            }

            var surefire_report = Directory.GetDirectories(target[0], "surefire-reports");
            if (surefire_report.Length == 0)
                continue; // no target
            if (surefire_report.Length > 1) // if the actual surefire-report is not on the first position and exists, moves it on the first position
            {
                string tDir = surefire_report[0];
                if (!tDir.EndsWith("/target") && !tDir.EndsWith("\\target"))
                    for (int i = 1; i < surefire_report.Length; i++)
                    {
                        tDir = surefire_report[i];
                        if (tDir.EndsWith("/target") || tDir.EndsWith("\\target"))
                        {
                            surefire_report[0] = tDir;
                        }
                    }
            }

            parseDirectory(surefire_report[0], results);
        }
    }

    private static void parseDirectory(string path, List<Model> results)
    {
        foreach (var files in Directory.EnumerateFiles(path, "*Test-*.xml"))
        {
            TestResultParser.readResults(files, results);
        }
    }
}
