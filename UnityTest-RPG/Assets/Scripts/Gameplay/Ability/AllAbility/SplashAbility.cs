namespace Gameplay.Ability.AllAbility
{
    using System;
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Model;
    using UnityEngine;

    public class SplashAbility : BaseAbility
    {
        private readonly ILogService      logger;
        private readonly AbilityBlueprint abilityBlueprint;
        public override  string           AbilityName => "SplashAttack";

        public SplashAbility(ILogService logger, AbilityBlueprint abilityBlueprint)
        {
            this.logger           = logger;
            this.abilityBlueprint = abilityBlueprint;
        }

        public override async UniTask Execute(AbilityDataState abilityDataState, IData attacker, IData hitAttack)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(this.abilityBlueprint[abilityDataState.AbilityKey].TimeFrequency));
            attacker.View.gameObject.SetActive(true);
            attacker.View.transform.localEulerAngles = new Vector3(0, 0, 0);
            var isComplete = false;
            attacker.View.transform.DOLocalRotate(new Vector3(0f, 360f, 0), 1f, RotateMode.FastBeyond360).OnComplete(() => isComplete = true);
            await UniTask.WaitUntil(() => isComplete);
            attacker.View.gameObject.SetActive(false);
            //Loop This splash attack
            await this.Execute(abilityDataState, attacker, hitAttack);
        }
    }
}