using CsvHelper.Configuration.Attributes;

namespace ExtractMavenTestResults;

internal class Model
{
    [Name("Test Class Name")]
    public string testClassName { get; set; }
    [Name("Candidate name for the tested class")]
    public string? candidateClassName { get; set; }
    public int testNumber { get; set; } = 0;

    public int errors { get; set; } =0;
    public int skipped { get; set; } = 0;
    public int failures { get; set; } = 0;
    public double time { get; set; } = 0;

    public String toString()
    {
        return $"{testClassName} posible testing \"{candidateClassName}\" with {testNumber} tests resulting in {errors} errors, {skipped} skipped," +
            $"{failures} failures; computed in {time} seconds";
    }

    public void generateCandidateName()
    {
        int index = testClassName.IndexOf("Test");
        candidateClassName = testClassName.Remove(index,4);
    }
}
