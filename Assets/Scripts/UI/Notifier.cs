using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Notifier : MonoBehaviour
    {
        public static void NotifyPlayer(string text, float time)
        {
            if (_instance._currentOperation != null)
            {
                _instance._currentOperation.Dispose();
            }

            _instance._notifyPanel.gameObject.SetActive(true);
            _instance._notifyText.text = text;

            _instance._currentOperation = Observable.Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ =>
                {
                    _instance._notifyPanel.gameObject.SetActive(false);
                    _instance._currentOperation = null;
                })
                .AddTo(_instance);
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("There more than one instances of notifier!");
                Destroy(this);
            }
            else
            {
                _instance = this;
                _notifyPanel.gameObject.SetActive(false);
            }
        }

        private IDisposable _currentOperation;

        private static Notifier _instance;
        [SerializeField] private Text _notifyText;
        [SerializeField] private GameObject _notifyPanel;
    }
}