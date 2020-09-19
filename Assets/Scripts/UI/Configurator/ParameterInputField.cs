using System;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ParameterInputField : MonoBehaviour
    {
        public string ParameterName => _label.text;
        public bool GetValue(out object value) => _objectGet(out value);
        public void Set(string label, Type valueType)
        {
            _label.text = label;

            MethodInfo methodInfo = typeof(ParameterInputField).GetMethod($"Set_{valueType.Name}", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo?.Invoke(this, null);
        }

        private void Set_String()
        {
            _inputField.contentType = InputField.ContentType.Name;
            
            _objectGet = (out object value) =>
            {
                value = _inputField.text;
                return _inputField.text.Length != 0;
            };
        }

        private void Set_Int32()
        {
            _inputField.contentType = InputField.ContentType.IntegerNumber;
            _objectGet = (out object value) =>
            {
                bool result = int.TryParse(_inputField.text, out int intNum);
                value = intNum;
                return result;
            };
        }

        private void Set_Single()
        {
            _inputField.contentType = InputField.ContentType.DecimalNumber;
            _objectGet = (out object value) =>
            {
                try
                {
                    value = Convert.ToSingle(_inputField.text, NumberFormatInfo.InvariantInfo);
                }
                catch
                {
                    value = -1;
                    return false;
                }
                return true;
            };
        }

        private delegate bool TryObjectGet(out object value);
        private TryObjectGet _objectGet;

        [SerializeField] private Text _label;
        [SerializeField] private InputField _inputField;
    }
}