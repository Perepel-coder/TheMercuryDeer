using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.Weapons.AmorSword;

namespace Assets.Scripts.Services.Enemies.StateHandler.AmorStateHandler
{
    public class AmorAttackingStateHandler : BaseAttackingStateHandler
    {
        private AmorSwordService _amorSword;

        public AmorAttackingStateHandler(AmorSwordService amorSword) : base() => _amorSword = amorSword;

        public override void Run(BaseEnemyAIService owner)
        {
            _amorSword.SetPositionStart(PlayerService.Instance.transform.position);
            _amorSword.Attack();
            base.Run(owner);
        }
    }
}
