using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using System.Reflection;
using WTTServerCommonLib;
using Range = SemanticVersioning.Range;

namespace TEST;

public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "com.test.test";
    public string Name { get; init; } = "Test";
    public string Author { get; init; } = "Test";
    public List<string>? Contributors { get; init; } = ["Test"];
    public SemanticVersioning.Version Version { get; init; } = new(typeof(ModMetadata).Assembly.GetName().Version?.ToString(3));
    public Range SptVersion { get; init; } = new("~4.1.6");
    public string? Url { get; init; } = "";
    public List<string>? Incompatibilities { get; init; }
    public Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~3.0.6") },
    };
    public bool? IsBundleMod { get; init; } = false;
    public string? License { get; init; } = "MIT";
    public bool HasPrepatcher { get; init; } = false;
}

[Injectable(TypePriority = OnLoadOrder.Preload + 4)]
public class Test(
    WTTServerCommonLib.WTTServerCommonLib wttCommon) : IOnLoad
{
    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        wttCommon.CustomQuestItemService.CreateCustomQuestItems(assembly);
        wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        wttCommon.CustomQuestService.CreateCustomQuests(assembly);
        wttCommon.CustomLootspawnService.CreateCustomLootSpawns(assembly);
        wttCommon.CustomLocaleService.CreateCustomLocales(assembly);
        wttCommon.CustomQuestZoneService.CreateCustomQuestZones(assembly);
        wttCommon.CustomAssortSchemeService.CreateCustomAssortSchemes(assembly);
        wttCommon.CustomBuffService.CreateCustomBuffs(assembly);

        return Task.CompletedTask;
    }
}
