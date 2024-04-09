using System.IO;

namespace Almond.Application.Utilities;

public static class Utilities
{
    public static void CopyConfigureFile(string source, string dest)
    {
        var dir = Path.GetDirectoryName(dest);

        if (dir is not null)
        {
            Directory.CreateDirectory(dir);
            File.Copy(source, dest);
        }
    }
}