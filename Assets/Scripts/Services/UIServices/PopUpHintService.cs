using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services.UIServices
{
    public class PopUpHintService : MonoBehaviour
    {
        public void Draw<T>(T text)
        {
            GetComponentInChildren<TMP_Text>().text = text.ToString();
        }

        public void EraseText()
        {
            GetComponentInChildren<TMP_Text>().text = string.Empty;
        }
    }
}
