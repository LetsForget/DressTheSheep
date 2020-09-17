using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ParameterInputField : MonoBehaviour
    {
        public object GetValue() => _objectGet();
        public void Set(string label, Type valueType)
        {
            _label.text = label;

            MethodInfo methodInfo = typeof(ParameterInputField).GetMethod($"Set_{valueType.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo?.Invoke(this, null);
        }

        private void Set_String()
        {
            _inputField.contentType = InputField.ContentType.Name;
            _objectGet = () => _inputField.text;
        }

        private void Set_Int32()
        {
            _inputField.contentType = InputField.ContentType.IntegerNumber;
            _objectGet = () => int.Parse(_inputField.text);
        }

        private void Set_Single()
        {
            _inputField.contentType = InputField.ContentType.DecimalNumber;
            _objectGet = () => float.Parse(_inputField.text);
        }

        private Func<object> _objectGet;

        [SerializeField] private Text _label;
        [SerializeField] private InputField _inputField;
    }
}