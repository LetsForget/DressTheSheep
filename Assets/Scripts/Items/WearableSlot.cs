using Network;
using System;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class WearableSlot : MonoBehaviour
    {
        public UnityEvent SlotInteraction;

        [Obsolete]
        public bool TrySetInSlot(WearableContainer wearable)
        {
            if (wearable.Container.Type != _slotType)
            {
                return false;
            }

            MoveToSlot(wearable, .5f);
            
            wearable.ObjectGrabbed += OnObjeactDeattached;
            
            _wearable = wearable;

            (string, string)[] data = new (string, string)[4];

            data[0] = ("SlotType", _slotType.ToString());
            data[1] = ("Interaction", "Atached");
            data[2] = ("WearableName", _wearable.Container.Name);
            data[3] = ("SlotGuid", _wearable.Container.Guid.ToString());

            PostRequest.Post(data, gameObject);

            _uiSlot.DisplayObjectName(_wearable.Container.Name);
            SlotInteraction.Invoke();

            return true;
        }

        private void Start()
        {
            SlotInteraction = new UnityEvent();
        }

        [Obsolete]
        private void OnObjeactDeattached()
        {
            _wearable.ObjectGrabbed -= OnObjeactDeattached;
            _uiSlot.DisplayObjectName("Not defined");

            (string, string)[] data = new (string, string)[4];

            data[0] = ("SlotType", _slotType.ToString());
            data[1] = ("Interaction", "Detached");
            data[2] = ("WearableName", _wearable.Container.Name);
            data[3] = ("SlotGuid", _wearable.Container.Guid.ToString());

            PostRequest.Post(data, gameObject);

            _wearable = null;
            SlotInteraction.Invoke();
        }

        private void MoveToSlot(WearableContainer wearable, float time)
        {
            Vector3 startPos = wearable.transform.position;
            Quaternion startRot = wearable.transform.rotation;

            Action<float> posChange = t => 
            { 
                wearable.transform.position = Vector3.Lerp(startPos, transform.position, t);
                wearable.transform.rotation = Quaternion.Lerp(startRot, transform.rotation, t);
            };

            Action onPosChanged = () => 
            { 
                wearable.transform.position = transform.position;
                wearable.transform.rotation = transform.rotation;

                wearable.transform.SetParent(transform);
            };

            float timeStepQuan = 1 / moveStep;
            float waitTime = time / timeStepQuan;

            TransformChange(waitTime , 0, posChange, onPosChanged);
        }

        private void TransformChange(float waitTime, float t, Action<float> onProccess, Action onCompleted)
        {
            Observable.Timer(TimeSpan.FromSeconds(waitTime))
                .Subscribe(_ =>
                {
                    onProccess(t);
   
                    if (t > 1)
                    {
                        onCompleted();
                    }
                    else
                    {
                        TransformChange(waitTime, t + moveStep, onProccess, onCompleted);
                    }
                })
                .AddTo(this);
        }

        private const float moveStep = .01f;

        private WearableContainer _wearable;

        [SerializeField] private ItemType _slotType;
        [SerializeField] private UISlot _uiSlot;
    }
}