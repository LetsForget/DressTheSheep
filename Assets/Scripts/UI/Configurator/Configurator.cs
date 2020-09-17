using Items;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Configurator : MonoBehaviour
    {
        public void SetDisplaying(GameObject wearableModel, Type wearableType)
        {
            int childCount = _parametersHolder.transform.childCount;

            for (int i = childCount -1; i >=0; i--)
            {
                Destroy(_parametersHolder.transform.GetChild(i).gameObject);
            }

            if (!wearableType.IsSubclassOf(typeof(AbstractWearable)))
            {
                throw new CantBeConfiguratedException();
            }

            ConstructorInfo[] constructorInfos = wearableType.GetConstructors();

            if (constructorInfos.Length == 0)
            {
                throw new CantBeConfiguratedException();
            }

            ParameterInfo[] parameters = constructorInfos[0].GetParameters();

            int paramQuan = parameters.Length;
            ParameterInputField[] _parameterInputFields = new ParameterInputField[paramQuan];
            for (int i = 0; i < paramQuan; i++)
            {
                _parameterInputFields[i] = Instantiate(_parameterInputField, _parametersHolder.transform);
                _parameterInputFields[i].Set(parameters[i].Name, parameters[i].ParameterType);
            }

            _generateButton.onClick.RemoveAllListeners();
            _generateButton.onClick.AddListener(() => Generate(wearableModel, constructorInfos[0], paramQuan, _parameterInputFields));
        }

        private void Generate(GameObject model, ConstructorInfo constructor, int paramsQuan, ParameterInputField[] parameters)
        {
            GameObject modelSpawned = Instantiate(model, _spawnPlace.position, _spawnPlace.rotation);

            object[] objParams = new object[paramsQuan];
            for (int i = 0; i < paramsQuan; i++)
            {
                objParams[i] = parameters[i].GetValue();
            }

            AbstractWearable wearable = constructor.Invoke(objParams) as AbstractWearable;

            WearableContainer container = modelSpawned.AddComponent<WearableContainer>();
            container.Initialize(wearable);
        }

        [SerializeField] private Button _generateButton;
        [SerializeField] private Transform _spawnPlace;
        [SerializeField] private VerticalLayoutGroup _parametersHolder;
        [SerializeField] private ParameterInputField _parameterInputField;
    }

    public class CantBeConfiguratedException : Exception
    {
        public override string Message => "Object can't be configurated!";
    }
}