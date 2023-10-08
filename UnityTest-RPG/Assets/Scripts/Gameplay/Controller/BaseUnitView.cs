namespace Gameplay.Controller
{
    using System;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.ObjectPool;
    using Gameplay.Model;
    using UnityEngine;

    public abstract class BaseUnitView : MonoBehaviour
    {
        public  Animator         Animator;
        public  Action<Collider> TriggerEnterAction;
        private void             OnTriggerEnter(Collider other) { this.TriggerEnterAction?.Invoke(other); }
    }

    public abstract class BaseUnitController<TData, TView> : IController<TData> where TData : IData where TView : BaseUnitView
    {
        private readonly IGameAssets gameAsset;
        protected        TData       Data;
        protected        TView       view;
        public           bool        IsFree { get; set; }

        private float turnSmoothVelocity;
        private float turnSmoothTime = 0.15f;
        protected BaseUnitController(IGameAssets gameAsset) { this.gameAsset = gameAsset; }

        public virtual async UniTask Create(TData data)
        {
            this.Data = data;
            var prefab = await this.gameAsset.LoadAssetAsync<GameObject>(data.PrefabKey);
            this.view                    = prefab.Spawn().GetComponent<TView>();
            data.View                    = this.view.transform;
            this.IsFree                  = false;
            this.view.TriggerEnterAction = this.OnTriggerEnter;
        }

        protected virtual void OnTriggerEnter(Collider obj) { }

        public virtual void Dispose()
        {
            if (this.view != null)
            {
                this.view.Recycle();
                this.view   = null;
                this.IsFree = true;
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
            var angle       = Mathf.SmoothDampAngle(this.view.transform.eulerAngles.y, targetAngle, ref this.turnSmoothVelocity, this.turnSmoothTime);
            this.view.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        protected void PlayAnimation(string trigger)
        {
            if (this.view.Animator != null)
            {
                this.view.Animator.SetTrigger(trigger);
            }
        }
    }
}