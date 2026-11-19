using System.Text.Json;

namespace ValidationApp;

/// <summary>
/// Checks that the file named by the only argument is strict JSON with an "sdk" object holding a "version"
/// string, the shape global.json.template must have. The validation build runs it on the template.
/// </summary>
internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: ValidationApp <path to global.json>");
            return 2;
        }

        var path = args[0];
        try
        {
            using var document = JsonDocument.Parse(File.ReadAllText(path));
            var root = document.RootElement;
            if (root.ValueKind == JsonValueKind.Object
                && root.TryGetProperty("sdk", out var sdk)
                && sdk.ValueKind == JsonValueKind.Object
                && sdk.TryGetProperty("version", out var version)
                && version.ValueKind == JsonValueKind.String)
            {
                return 0;
            }

            Console.Error.WriteLine($"{path} has no \"sdk\" object with a \"version\" string.");
            return 1;
        }
        catch (JsonException e)
        {
            Console.Error.WriteLine($"{path} is not strict JSON: {e.Message}");
            return 1;
        }
    }
}
