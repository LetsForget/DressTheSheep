using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ConfiguratorSwapButton : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            _configurator.SetDisplaying(_model, _type);
        }

        private void Awake()
        {
            _type = Type.GetType(_typeName);

            if (_type == null)
            {
                Debug.LogError("Type name incorrect!");
            }
        }

        private Type _type;

        [SerializeField] private Configurator _configurator;
        [SerializeField] private GameObject _model;
        [SerializeField] private string _typeName;
    }
}