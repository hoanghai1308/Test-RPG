namespace Gameplay.Model
{
    using System;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public interface IController : IDisposable
    {
        void OnMove(Vector3 direction);
    }

    public interface IController<in TData> : IController where TData : IData
    {
        UniTask Create(TData data);
        bool    IsFree { get; set; }
    }

    public interface IEnemyController
    {
        void OnAttack();
    }
}