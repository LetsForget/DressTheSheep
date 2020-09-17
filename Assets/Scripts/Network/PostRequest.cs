using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Network
{
    public static class PostRequest 
    {
        [Obsolete]
        public static void Post((string, string)[] data, GameObject sender)
        {
            try
            {
                WWWForm request = new WWWForm();
                
                foreach ((string, string) field in data)
                {
                    request.AddField(field.Item1, field.Item2);
                }

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("auth", _authKey);

                ObservableWWW.PostWWW(_address, request.data, headers)
                    .Subscribe(_ => Debug.Log("Post sent"))
                    .AddTo(sender);
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }

        }

		private const string _address = "https://dev3r02.elysium.today/inventory/status";
		private const string _authKey = "BMeHG5xqJeB4qCjpuJCTQLsqNGaqkfB6";
	}
}