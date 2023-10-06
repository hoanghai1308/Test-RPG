namespace Blueprints
{
    using BlueprintFlow.BlueprintReader;

    [BlueprintReader("MiscParam")]
    public class MiscParamBlueprint : GenericBlueprintReaderByCol
    {
        public string PlayerPrefab { get; set; }
    }
}