namespace ExtractMavenTestResults;

internal class Config
{
    public string configFilename = "config.yaml";
    public bool printScreen = true;

    public void parseArgs(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].Equals("-c"))
                configFilename = args[++i];
            if (args[i].StartsWith("--config"))
                configFilename = args[i].Substring(args[i].IndexOf("=") + 1);

            if (args[i].Equals("-n") || args[i].Equals("--no-console"))
                printScreen = false;

            if (args[i].Equals("-h") || args[i].Equals("--help"))
            {
                Help.display();
            }
        }
    }

    private Config() { }

    private static Config? _instance;

    public static Config getInstance()
    {
        if (_instance == null)
            _instance = new Config();
        return _instance;
    }
}

internal class Help
{
    public static void display()
    {
        Console.WriteLine(helpMsg);
    }

    private static string helpMsg = " not yet implemented ";
}
