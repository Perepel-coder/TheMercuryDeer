namespace Assets.Scripts.Services.Enemies.Amor
{
    public class AmorViewService : BaseEnemyViewService
    {
        protected override void Start()
        {
            base.Start();
        }

        public void OnReactionToTakingHitEnter() => _ownerAI.ReactionToTakingHit.Weapon.TurnOnCollider(true);

        public void OnReactionToTakingHitExit() => _ownerAI.ReactionToTakingHit.Weapon.TurnOnCollider(false);
    }
}
