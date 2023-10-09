namespace Gameplay.Controller
{
    using System;
    using System.Collections.Generic;
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using FSG.MeshAnimator.Snapshot;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using GameFoundation.Scripts.Utilities.ObjectPool;
    using Gameplay.Ability;
    using Gameplay.Model;
    using Gameplay.Signal;
    using UnityEngine;
    using Zenject;

    public abstract class BaseUnitView : MonoBehaviour
    {
        public  SnapshotMeshAnimator Animator;
        public  Action<Collider>     TriggerEnterAction;
        public  IData                Data;
        public  int                  Health                         => this.Data.Health;
        private void                 OnTriggerEnter(Collider other) { this.TriggerEnterAction?.Invoke(other); }
    }

    public abstract class BaseUnitController<TData, TView> : IController<TData> where TData : IData where TView : BaseUnitView
    {
        private readonly IGameAssets gameAsset;
        protected        TData       Data;
        protected        TView       view;
        public           bool        IsFree { get; set; }

        private            float                turnSmoothVelocity;
        [Inject] private   MiscParamBlueprint   miscParamBlueprint;
        [Inject] private   DiContainer          diContainer;
        [Inject] protected ProcessAbilitySystem ProcessAbilitySystem;
        [Inject] protected ILogService          Logger;
        [Inject] protected SignalBus            SignalBus;
        protected BaseUnitController(IGameAssets gameAsset) { this.gameAsset = gameAsset; }

        public virtual async UniTask Create(TData data)
        {
            this.Data = data;
            var prefab = await this.gameAsset.LoadAssetAsync<GameObject>(data.PrefabKey);
            this.view                    = prefab.Spawn().GetComponent<TView>();
            data.View                    = this.view.transform;
            this.IsFree                  = false;
            this.view.TriggerEnterAction = this.OnTriggerEnter;
            this.view.Data               = data;

            this.SignalBus.Subscribe<UpdateUnitDataSignal>(this.OnUnitDataChange);
        }

        private void OnUnitDataChange(UpdateUnitDataSignal obj)
        {
            if (Equals(this.Data, obj.Data))
            {
                this.OnUpdateData();
            }
        }

        protected virtual void OnUpdateData() { this.DetectDead(); }

        protected virtual void OnTriggerEnter(Collider obj) { }

        public virtual void Dispose()
        {
            if (this.view != null)
            {
                this.view.Recycle();
                this.view   = null;
                this.IsFree = true;
                this.SignalBus.Unsubscribe<UpdateUnitDataSignal>(this.OnUnitDataChange);
            }
        }

        public virtual void OnMove(Vector3 direction)
        {
            this.Data.CurrentPosition    += direction;
            this.view.transform.position += direction;
        }

        protected void LookAt(Vector3 direction)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle       = Mathf.SmoothDampAngle(this.view.transform.eulerAngles.y, targetAngle, ref this.turnSmoothVelocity, this.miscParamBlueprint.TurnSmoothTime);
            this.view.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        protected void PlayAnimation(string animName)
        {
            if (this.view.Animator != null)
            {
                this.view.Animator.Play(animName);
                this.view.Animator.OnAnimationFinished = this.OnAnimationFinished;
            }
        }

        protected virtual void OnAnimationFinished(string obj) { }

        protected AbilityDataState CreateAbilityDataState(string abilityId)
        {
            var data = this.diContainer.Instantiate<AbilityDataState>();
            data.AbilityKey = abilityId;
            data.State      = AbilityState.Free;

            return data;
        }

        protected async UniTask ProcessAbility(bool activeAbility, List<AbilityDataState> abilityDataStates, IData source, IData target)
        {
            if (!activeAbility) return;
            var listTask = new List<UniTask>();

            foreach (var abilityDataState in abilityDataStates)
            {
                listTask.Add(this.ProcessAbilitySystem.ProcessAbility(abilityDataState, source, target));
            }

            await UniTask.WhenAll(listTask);
        }

        protected virtual async void DetectDead()
        {
            if (this.Data.Health <= 0)
            {
                this.PlayAnimation("Die");
                await UniTask.Delay(1000);
                this.Dispose();
            }
        }
    }
}