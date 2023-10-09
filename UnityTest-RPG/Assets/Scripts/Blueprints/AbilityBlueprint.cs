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
        public string ProjectTile;
        public string HitTarget;
        public string EffectId;
        public int    Value;
    }
}