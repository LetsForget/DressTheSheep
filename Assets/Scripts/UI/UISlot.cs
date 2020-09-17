using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISlot : MonoBehaviour
    {
        public void DisplayObjectName(string name)
        {
            _field.text = name;
        }

        [SerializeField] private Text _field;
    }
}