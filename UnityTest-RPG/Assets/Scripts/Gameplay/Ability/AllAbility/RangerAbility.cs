namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using GameFoundation.Scripts.Utilities.ObjectPool;
    using Gameplay.Model;
    using UnityEngine;

    public class RangerAbility : BaseAbility
    {
        public override string AbilityName => "RangerAttack";

        public override async UniTask Execute(AbilityDataState abilityDataState, IData attacker, IData hitAttack)
        {
            var projectTile = await this.GameAssets.LoadAssetAsync<GameObject>(this.AbilityBlueprint[abilityDataState.AbilityKey].ProjectTile);
            var view        = projectTile.Spawn();
            view.transform.position = attacker.View.transform.position;
            var targetPos = hitAttack.CurrentPosition;

            view.transform.DOMove(targetPos, 0.5f).OnComplete(() =>
            {
                view.Recycle();
                this.CreateHitTarget(abilityDataState, attacker, hitAttack);
            });
        }
    }
}