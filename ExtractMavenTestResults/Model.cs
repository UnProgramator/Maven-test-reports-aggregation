using CsvHelper.Configuration.Attributes;

namespace ExtractMavenTestResults;

internal class Model
{
    [Name("Test Class Name")]
    public string testClassName { get; set; }

    [Name("First candidate name for the tested class")]
    public string? candidateClassName1 { get; set; } // the test class with the "Test" string removed from the test class name

    [Name("Second candidate name for the tested class")]
    public string? candidateClassName2 { get; set; } // not null if there are digits at the end of the first candidate, or the class name if no 1st candidate

    [Name("Third candidate name for the tested class")]
    public string? candidateClassName3 { get; set; } // not null if there is an _* at the end of the name of the first candidate, or the class name if no 1st candidate

    public int testNumber { get; set; } = 0;

    public int errors { get; set; } =0;
    public int skipped { get; set; } = 0;
    public int failures { get; set; } = 0;
    public double time { get; set; } = 0;

    public String toString()
    {
        return $"{testClassName} posible testing {{\"{candidateClassName1}\", \"{candidateClassName2}\", \"{candidateClassName3}\" }} with {testNumber} tests resulting in {errors} errors, {skipped} skipped," +
            $"{failures} failures; computed in {time} seconds";
    }

    public void generateCandidateName()
    {
        int index;
        if (testClassName.Contains("Test"))
        {
            index = testClassName.IndexOf("Test");
            candidateClassName1 = testClassName.Remove(index, 4);
        }
        else
            candidateClassName1 = testClassName; // it is used to simplifie the computation of the 2nd and 3rd candidates

        //remove digits at the end of the name, if there are any
        for (index = candidateClassName1.Length - 1; index >= 0 && char.IsDigit(candidateClassName1[index]); index--)
            if (candidateClassName1[index] == '.') // we no longer study the class name, and we proced to the package name, so no digit at the end
                index = -1;

        if (index > 0 && index != candidateClassName1.Length - 1)
            candidateClassName2 = candidateClassName1.Remove(index);

        if (candidateClassName1.Contains('_'))
        {
            int idx_ = candidateClassName1.LastIndexOf('_');
            int idxP = candidateClassName1.LastIndexOf('.');

            if(idx_ > idxP) // only if the _ is after the last . from the java fully qualified name
                candidateClassName3 = candidateClassName1.Remove(idx_);
        }

        if(candidateClassName1 == testClassName) // if we no Test was found in the name, we reset candidateClassName1 to null
            candidateClassName1 = null;
    }
}
