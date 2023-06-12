using System.Collections;
using LitJson;
using NetworkTools;
using UnityEngine;

public class Net : MonoBehaviour
{
    [SerializeField] private string _serverIp = "127.0.0.1";
    [SerializeField] private string _serverPort = "8080";
    [SerializeField] private string[] _subCatalogue = null;

    private HttpManager _httpManager;

    private void Start()
    {

        RecvMessage recvMessage = _httpManager.Data;

        SendMessage sendMessage = new SMessage();

        _httpManager = new HttpManager(recvMessage);

        SendGetRequest();
    }

    public void SendGetRequest()
    {
        // Get the URI for the GET request
        string uri = _httpManager.GetParser(_serverIp, _serverPort, _subCatalogue);
        
        StartCoroutine(SendGetRequestCoroutine(uri));
    }

    private IEnumerator SendGetRequestCoroutine(string uri)
    {
        yield return _httpManager.Get(uri);

        // Check the result of the GET request
        switch (_httpManager.Result)
        {
            case NetworkResult.Success:
                Debug.Log("GET request succeeded!");
              
                RecvMessage recvMessage = _httpManager.Data;
                JsonData jsonData = _httpManager.JsonData;
                byte[] rawData = _httpManager.RawData;
                break;
            default:
                Debug.Log($"GET request failed with error: {_httpManager.Result.ToString()}");
                break;
        }
        
        _httpManager.Clear();
    }
    
}
