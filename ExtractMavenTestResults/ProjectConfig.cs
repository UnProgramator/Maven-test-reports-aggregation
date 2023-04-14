namespace ExtractMavenTestResults;

class ProjectConfig
{
    public string rootDirectory { get; set; } = ".";

    public bool? fullSearch { get; set; }

    public bool? standardSearch { get; set; }

    public string[]? subdirectories { get; set; }

    public string output { get; set; }

    public static ProjectConfig getConfig()
    {
        ProjectConfig config = FileSerializer.readYaml<ProjectConfig>(Config.getInstance().configFilename);

        config.validate();

        return config;
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
