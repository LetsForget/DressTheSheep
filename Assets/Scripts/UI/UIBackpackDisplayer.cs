using UnityEngine;

namespace UI
{
    public class UIBackpackDisplayer : MonoBehaviour
    {
        private void Start()
        {
            _uiToShow.gameObject.SetActive(false);
        }

        private void OnMouseOver()
        {
            _uiToShow.gameObject.SetActive(Input.GetKey(KeyCode.Mouse0));
        }

        private void OnMouseExit()
        {
            _uiToShow.gameObject.SetActive(false);
        }

        [SerializeField] private Canvas _uiToShow;
    }
}