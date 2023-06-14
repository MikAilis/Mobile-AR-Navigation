using System.Collections;
using DefaultNamespace;
using LitJson;
using NetworkTools;
using UnityEngine;

public class Net : MonoBehaviour
{
    [SerializeField] private string _serverIp = "127.0.0.1";
    [SerializeField] private string _serverPort = "8080";
    [SerializeField] private string[] _subCatalogue = null;

    private HttpManager _httpManager;
    private GameObject m_gameObject;


    private void Start()
    {
        SendGetRequest();
        SendSaveRequest();
    }

    public IEnumerator SendGetRequest()
    {
        SMessage smsg = new SMessage("GET_OBJECT",GetComponent<Position>().cube.name,GetComponent<Position>()._currentPosition,
            Quaternion.Euler(GetComponent<Position>().cube.transform.rotation.eulerAngles) * Vector3.forward,
            GetComponent<Position>().cube.transform.localScale);
        RMessage rmsg = new RMessage();
        _httpManager = new HttpManager(rmsg, smsg);
        // Get the URI for the GET request
        string uri = _httpManager.GetParser(_serverIp, _serverPort, _subCatalogue);
        
        StartCoroutine(_httpManager.Get(uri));
        while (_httpManager.Result == NetworkResult.Waiting)
        {
            yield return null;
        }
        
        if (_httpManager.Result == NetworkResult.Success)
        {
            Debug.Log("update success!");
            //todo: create a new object
            GameObject cube = GameObject.Find("MovableObject(Clone)");
            if (cube==null)
            {
                cube = Instantiate(m_gameObject, rmsg.prefabs[0].position, rmsg.prefabs[0].rotation);
            }
            else
            {
                if (rmsg.prefabs.Count>0)
                {
                    cube.transform.position = rmsg.prefabs[0].position;
                }
                else
                {
                    Destroy(cube);
                    cube = null;
                }
            }
        }
    }
    public IEnumerator SendSaveRequest()
    {
        SMessage smsg = new SMessage("SAVE_OBJECT",GetComponent<Position>().cube.name,GetComponent<Position>()._currentPosition,
            Quaternion.Euler(GetComponent<Position>().cube.transform.rotation.eulerAngles) * Vector3.forward,
            GetComponent<Position>().cube.transform.localScale);
        
        RMessage rmsg = new RMessage();
        _httpManager = new HttpManager(rmsg, smsg);
        // Get the URI for the GET request
        string uri = _httpManager.GetParser(_serverIp, _serverPort, _subCatalogue);
        
        StartCoroutine(_httpManager.Get(uri));
        while (_httpManager.Result == NetworkResult.Waiting)
        {
            yield return null;
        }
        
        if (_httpManager.Result == NetworkResult.Success)
        {
            Debug.Log("Save success!");
        }
    }

}
