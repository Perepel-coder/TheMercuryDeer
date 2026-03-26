using Assets.Scripts.Weapons.ReactionToTakingHit;

namespace Assets.Scripts.Enemies.Amor
{
    public class AmorView : BaseEnemyView
    {
        protected override void Start()
        {
            base.Start();
        }

        public void OnReactionToTakingHitEnter() => _ownerAI.ReactionToTakingHit.Weapon.TurnOnCollider(true);

        public void OnReactionToTakingHitExit() => _ownerAI.ReactionToTakingHit.Weapon.TurnOnCollider(false);
    }
}
