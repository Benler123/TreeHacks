using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadImage : MonoBehaviour
{
    // Start is called before the first frame update
    public string imageUrl = "https://picsum.photos/200/300";
     

    void Start()
    {    
        StartCoroutine(LoadImagePeriodically());
    }

    IEnumerator GetTexture() {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log(www.error);
        } else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            GetComponent<Renderer>().material.mainTexture = texture;
            float aspectRatio = (float)texture.width / texture.height;
            transform.localScale = new Vector3(aspectRatio, 1, 1);
        }
    }

    IEnumerator LoadImagePeriodically()
    {
        while (true)
        {
            yield return StartCoroutine(GetTexture());
            yield return new WaitForSeconds(2);
        }
    }

    void Update()
    {
    }
}
