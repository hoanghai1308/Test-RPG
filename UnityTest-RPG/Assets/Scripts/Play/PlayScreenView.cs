namespace Play
{
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.Presenter;
    using GameFoundation.Scripts.UIModule.ScreenFlow.BaseScreen.View;
    using Gameplay.Manager;
    using TMPro;
    using Zenject;
    using UniRx;

    public class PlayScreenView : BaseView
    {
        public TextMeshProUGUI txtTotalEnemy;
    }

    [ScreenInfo(nameof(PlayScreenView))]
    public class PlayScreenPresenter : BaseScreenPresenter<PlayScreenView>
    {
        private readonly GameDataManager gameDataManager;
        public PlayScreenPresenter(SignalBus signalBus, GameDataManager gameDataManager) : base(signalBus) { this.gameDataManager = gameDataManager; }

        protected override async void OnViewReady()
        {
            base.OnViewReady();
            await this.OpenViewAsync();
            this.gameDataManager.TotalEnemyCount.Subscribe(this.OnEnemyCountChange);
        }

        private void OnEnemyCountChange(int obj) { this.View.txtTotalEnemy.text = $"Total Enemy: {obj}"; }

        public async override UniTask BindData() { }
    }
}