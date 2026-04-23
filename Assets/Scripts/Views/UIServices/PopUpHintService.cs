using TMPro;
using UnityEngine;

namespace Assets.Scripts.Views.UIServices
{
    public class PopUpHintService : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }

        public void Draw<T>(T text)
        {
            IsEmpty = false;
            GetComponentInChildren<TMP_Text>().text = text.ToString();
        }

        public void EraseText()
        {
            IsEmpty = true;
            GetComponentInChildren<TMP_Text>().text = string.Empty;
        }
    }
}
