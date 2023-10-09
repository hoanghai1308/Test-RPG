namespace Blueprints
{
    using System.Collections.Generic;
    using BlueprintFlow.BlueprintReader;

    [BlueprintReader("MiscParam")]
    public class MiscParamBlueprint : GenericBlueprintReaderByCol
    {
        public string      PlayerPrefab        { get; set; }
        public float       TimeAutoSpawnEnemy  { get; set; }
        public List<int>   SpawnEnemyRate      { get; set; }
        public List<float> SpawnRangerPosition { get; set; }
        public float       TurnSmoothTime      { get; set; }
    }
}