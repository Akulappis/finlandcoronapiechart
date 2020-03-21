using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class FetchFromHS : MonoBehaviour
{
    public bool fethced = false;
    public int confirmedCases = 0;
    public int deathCases = 0;
    public int recoveries = 0;

    public string jsonToUse;
    void Start()
    {
        StartCoroutine(GetTexture());
    }
    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://w3qa5ydb4l.execute-api.eu-west-1.amazonaws.com/prod/finnishCoronaData");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            jsonToUse = www.downloadHandler.text;
            RootObject root = JsonUtility.FromJson<RootObject>(jsonToUse);
            Debug.Log(root);
            confirmedCases = root.confirmed.Length;
            deathCases = root.deaths.Length;
            recoveries = root.recovered.Length;
            fethced = true;
        }
    }
    [System.Serializable]
    public class ConfirmedCase
    {
        public int id;
        public string date = null;
        public string healthCareDistrict = null;
        public string infectionSourceCountry = null;
        public string infectionSource = null;
    }
    [System.Serializable]
    public class RecoveredCase
    {
        public int id;
        public string date = null;
        public string healthCareDistrict = null;
    }
    [System.Serializable]
    public class DeathCase
    {
        public int id;
        public string date = null;
        public string healthCareDistrict = null;
    }
    [System.Serializable]
    public class RootObject
    {
        public ConfirmedCase[] confirmed;
        public DeathCase[] deaths;
        public RecoveredCase[] recovered;
    }
}



