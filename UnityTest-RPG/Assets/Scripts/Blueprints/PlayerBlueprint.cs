namespace Blueprints
{
    using System.Collections.Generic;
    using BlueprintFlow.BlueprintReader;

    [CsvHeaderKey("Id")]
    [BlueprintReader("PlayerBlueprint")]
    public class PlayerBlueprint : GenericBlueprintReaderByRow<string, PlayerBlueprintRecord>
    {
    }

    public class PlayerBlueprintRecord
    {
        public string        Id;
        public int           Health;
        public int           Damage;
        public Dictionary<string,string> WeaponList;
    }
}