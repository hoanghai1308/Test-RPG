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
    }

    public abstract class BaseUnitController<TData, TView> : IDisposable where TData : IData where TView : BaseUnitView
    {
        private readonly IGameAssets gameAsset;
        protected        TData       Data;
        protected        TView       view;

        protected BaseUnitController(IGameAssets gameAsset) { this.gameAsset = gameAsset; }

        public virtual async UniTask Create(TData data)
        {
            this.Data = data;
            var prefab = await this.gameAsset.LoadAssetAsync<GameObject>(data.PrefabKey);
            this.view = prefab.Spawn().GetComponent<TView>();
        }

        public virtual void Dispose()
        {
            if (this.view != null)
            {
                this.view.Recycle();
                this.view = null;
            }
        }

        public virtual void OnMove(Vector3 position)
        {
            this.Data.CurrentPosition    += position;
            this.view.transform.position += position;
            
        }
    }
}