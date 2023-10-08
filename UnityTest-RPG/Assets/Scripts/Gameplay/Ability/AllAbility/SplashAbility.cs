namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Model;
    using UnityEngine;

    public class SplashAbility : BaseAbility
    {
        private readonly ILogService logger;
        public override  string      AbilityName => "SplashAttack";

        public SplashAbility(ILogService logger) { this.logger = logger; }

        //Todo separate it if have multiple splash attack
        public override async UniTask Execute(IData attacker, IData hitAttack)
        {
            attacker.View.gameObject.SetActive(true);
            attacker.View.transform.localEulerAngles = new Vector3(0, 0, 0);
            var isComplate = false;
            attacker.View.transform.DOLocalRotate(new Vector3(0f, 360f, 0), 1f, RotateMode.FastBeyond360).OnComplete(() => isComplate = true);
            await UniTask.WaitUntil(() => isComplate);
            attacker.View.gameObject.SetActive(false);
        }
    }
}