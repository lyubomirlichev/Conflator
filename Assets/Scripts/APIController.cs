using System;
using System.Collections;
using FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

public class APIController
{
    public class KanyeResponse
    {
        public string quote;
    }

    private MonoBehaviour host;
    int retryCount = 3;

    public void Init(MonoBehaviour mono)
    {
        host = mono;
    }

    public void GetKanyeResponse(Action<KanyeResponse> success)
    {
        WaitForWebRequest<KanyeResponse>("https://api.kanye.rest/", success, (error) => { Debug.LogError("Failed to get API response."); });
    }

    private void WaitForWebRequest<T>(string path, Action<T> success, Action<string> error) where T : class
    {
        host.StartCoroutine(SendRequest(path, success, error));
    }

    private IEnumerator SendRequest<T>(string path, Action<T> success, Action<string> error) where T : class
    {
        float timeout = 0;
        retryCount = 3;

        using var unityWebRequest = UnityWebRequest.Get(path);
        unityWebRequest.SendWebRequest();

        while (!unityWebRequest.isDone && timeout < 20)
        {
            yield return null;
            timeout += Time.deltaTime;
        }

        var text = unityWebRequest.downloadHandler.text;
        var errorText = unityWebRequest.error;
        var responseCode = unityWebRequest.responseCode;
        var result = unityWebRequest.result;

        if (result != UnityWebRequest.Result.Success)
        {
            retryCount--;

            if (retryCount > 0)
            {
                host.StartCoroutine(SendRequest(path, success, error));
            }
            else
            {
                Debug.LogError($"WebRequest {path}. Got error {errorText} Code {responseCode} text {text}");
                error?.Invoke(text);
                unityWebRequest.Dispose();
            }
        }
        else
        {
            ParseResult(unityWebRequest.downloadHandler.text, success, error);
        }
    }

    private void ParseResult<T>(string text, Action<T> successCallback, Action<string> errorCallback)
    {
        var parse = ReadFromText<T>(text);
        if (parse != null)
        {
            successCallback?.Invoke(parse);
        }
        else
        {
            errorCallback?.Invoke("Error parsing data.");
        }
    }

    private T ReadFromText<T>(string jsonText)
    {
        if (false == string.IsNullOrEmpty(jsonText))
        {
            return (T)Deserialise(jsonText, default(T), typeof(T));
        }

        return default;
    }

    private object Deserialise(string jsonText, object obj, Type type)
    {
        var serialiser = new fsSerializer();
        var jsonData = fsJsonParser.Parse(jsonText);
        serialiser.TryDeserialize(jsonData, type, ref obj);
        return obj;
    }
}