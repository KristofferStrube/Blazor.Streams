using System.Reflection;

namespace KristofferStrube.Blazor.FileAPI.Options;

public class StreamsOptions
{
    public const string DefaultBasePath = "./_content/";
    public static readonly string DefaultNamespace = Assembly.GetExecutingAssembly().GetName().Name ?? "KristofferStrube.Blazor.Streams";
    public static readonly string DefaultScriptPath = $"{DefaultNamespace}/{DefaultNamespace}.js";

    public string BasePath { get; set; } = DefaultBasePath;
    public string ScriptPath { get; set; } = DefaultScriptPath;

    public string FullScriptPath => Path.Combine(this.BasePath, this.ScriptPath);

    internal static StreamsOptions DefaultInstance = new();

}