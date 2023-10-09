namespace Blueprints
{
    using System.Collections.Generic;
    using BlueprintFlow.BlueprintReader;

    [CsvHeaderKey("Id")]
    [BlueprintReader("Enemy")]
    public class EnemyBlueprint : GenericBlueprintReaderByRow<string, EnemyRecord>
    {
    }

    public class EnemyRecord
    {
        public string       Id;
        public string       PrefabKey;
        public int          Health;
        public int          Damage;
        public int          AttackRange;
        public List<string> AbilityList;
    }
}