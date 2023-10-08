namespace Blueprints
{
    using BlueprintFlow.BlueprintReader;

    [CsvHeaderKey("Id")]
    [BlueprintReader("Ability")]
    public class AbilityBlueprint : GenericBlueprintReaderByRow<string, AbilityRecord>
    {
    }

    public class AbilityRecord
    {
        public string Id;
        public float  TimeFrequency;
    }
}