namespace Blueprints
{
    using System.Collections.Generic;
    using BlueprintFlow.BlueprintReader;

    [CsvHeaderKey("Id")]
    [BlueprintReader("Weapon")]
    public class WeaponBlueprint : GenericBlueprintReaderByRow<string, WeaponBlueprintRecord>
    {
    }

    public class WeaponBlueprintRecord
    {
        public string       Id;
        public string       PrefabKey;
        public string Ability;
    }
}