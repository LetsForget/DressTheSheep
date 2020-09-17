using System;
using UnityEngine;

namespace Items
{
    public class WearableContainer : MonoBehaviour
    {
        public event Action ObjectGrabbed;

        public AbstractWearable Container { get; private set; }

        private void Awake()
        {
            _rbody = gameObject.GetComponent<Rigidbody>();
            _layerMask = LayerMask.GetMask("Slots");
        }

        public void Initialize(AbstractWearable wearable)
        {
            if (Container != null)
            {
                Debug.LogError("Already initialized!");
                return;
            }

            Container = wearable;
        }

        private void OnMouseDown()
        {
            ObjectGrabbed?.Invoke();

            transform.SetParent(null);
            _rbody.isKinematic = false;

            _rbody.velocity = Vector3.zero;
            _rbody.angularVelocity = Vector3.zero;
        //    _rbody.isKinematic = true;
            
            Vector3 camPos = Camera.main.transform.position;
            Vector3 objPos = transform.position;

            Vector3 dir = camPos - objPos;
            _moveArea = new Plane(dir.normalized, objPos);
        }

        private void OnMouseDrag()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_moveArea.Raycast(ray, out float enter))
            {
                _rbody.velocity = (ray.GetPoint(enter) - transform.position) * 5;

               // _rbody.velocity = Vector3.zero;
                _rbody.angularVelocity = Vector3.zero;
            }
        }

        private void OnMouseUp()
        {
            _rbody.isKinematic = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit info,100, _layerMask))
            {
                if (info.transform.TryGetComponent(out WearableSlot slot))
                {
                    _rbody.isKinematic = true;
                    slot.TrySetInSlot(this);
                }
            }
        }

        private Plane _moveArea;
        private Rigidbody _rbody;
        private LayerMask _layerMask;
    }
}