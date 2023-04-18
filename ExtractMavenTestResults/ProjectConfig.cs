namespace ExtractMavenTestResults;

class ProjectConfig
{
    public string rootDirectory { get; set; } = ".";

    public bool? fullSearch { get; set; }

    public bool? standardSearch { get; set; }

    public string[]? subdirectories { get; set; }

    public string output { get; set; }

    private static ProjectConfig? _instance;

    public static ProjectConfig getConfig()
    {
        if (_instance == null)
        {
            _instance = FileSerializer.readYaml<ProjectConfig>(Config.getInstance().configFilename);

            _instance.validate();
        }

        return _instance;
    }

    private void validate()
    {
        if (rootDirectory is null || rootDirectory.Equals(""))
            rootDirectory = ".";

        if (output is null)
            output = "results.csv";

        if (fullSearch is null)
            fullSearch = false;

        if (standardSearch is null)
            standardSearch = true;
    }

    public ProjectConfig() { }
}
