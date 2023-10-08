namespace Gameplay.Ability
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Gameplay.Ability.AllAbility;
    using Gameplay.Model;

    public class ProcessAbilitySystem
    {
        private Dictionary<string, IAbility> abilities = new();

        public ProcessAbilitySystem(List<IAbility> listAbilities)
        {
            foreach (var item in listAbilities)
            {
                this.abilities.Add(item.AbilityName, item);
            }
        }

        public async UniTask ProcessAbility(string abilityName, IData data, IData target)
        {
            if (this.abilities.TryGetValue(abilityName, out var ability))
            {
                await ability.Execute(data, target);
            }
        }
    }
}