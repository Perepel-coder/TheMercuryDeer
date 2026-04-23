using Assets.Scripts.Constants.Paths;
using Assets.Scripts.Services.InventorySystemServices;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Views.UIServices;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class ItemServiceView : MonoBehaviour
    {
        private ItemService _owner;
        private float _radiusInteractionZone = 1f;
        private PopUpHintService _popUpHintService;

        protected virtual void Start()
        {
            _owner = GetComponent<ItemService>();

            _popUpHintService = Instantiate(
                Resources.Load<PopUpHintService>(ResourcePaths.UI.HINT_POP_UP),
                transform.position + new Vector3(0, _radiusInteractionZone, 0),
                Quaternion.identity);

            _popUpHintService.EraseText();
        }

        protected virtual void OnDestroy()
        {
            if (_popUpHintService != null)
                Destroy(_popUpHintService.gameObject);
        }

        protected virtual void Update()
        {
            CheckHintPopUp();
        }

        private void CheckHintPopUp()
        {
            if (_owner.IsPlayerInRange && _popUpHintService.IsEmpty) _popUpHintService.Draw("е");
            else if (!_owner.IsPlayerInRange && !_popUpHintService.IsEmpty) _popUpHintService.EraseText();
        }
    }
}