using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;

namespace TEST;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.test.test";
    public override string Name { get; init; } = "Test";
    public override string Author { get; init; } = "Test";
    public override List<string>? Contributors { get; init; } = ["Test"];
    public override SemanticVersioning.Version Version { get; init; } = new(typeof(ModMetadata).Assembly.GetName().Version?.ToString(3));
    public override Range SptVersion { get; init; } = new("~4.0.13");
    public override string? Url { get; init; } = "";
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.20") },
    };
    public override bool? IsBundleMod { get; init; } = false;
    public override string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 4)]
public class SOCOM(
    WTTServerCommonLib.WTTServerCommonLib wttCommon) : IOnLoad
{
    public async Task OnLoad()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        await wttCommon.CustomQuestService.CreateCustomQuests(assembly);
        await wttCommon.CustomLootspawnService.CreateCustomLootSpawns(assembly);
        await wttCommon.CustomLocaleService.CreateCustomLocales(assembly);
        await wttCommon.CustomQuestZoneService.CreateCustomQuestZones(assembly);
        await wttCommon.CustomAssortSchemeService.CreateCustomAssortSchemes(assembly);
        await wttCommon.CustomBuffService.CreateCustomBuffs(assembly);
    }
}
