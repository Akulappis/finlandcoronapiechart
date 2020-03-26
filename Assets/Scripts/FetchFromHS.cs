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

    public int[] infectionsPerRegion = new int[20];
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
            CalculateByArea(root.confirmed);
        }
    }
    public void CalculateByArea(ConfirmedCase[] confirmedCases)
    {
        foreach (var item in confirmedCases)
        {
            switch (item.healthCareDistrict)
            {
                case "Lappi":
                    infectionsPerRegion[0]++;
                    break;
                case "Länsi-Pohja":
                    infectionsPerRegion[1]++;
                    break;
                case "Pohjois-Pohjanmaa":
                    infectionsPerRegion[2]++;
                    break;
                case "Kainuu":
                    infectionsPerRegion[3]++;
                    break;
                case "Keski-Pohjanmaa":
                    infectionsPerRegion[4]++;
                    break;               
                case "Pohjois-Savo":
                    infectionsPerRegion[5]++;
                    break;
                case "Pohjois-Karjala":
                    infectionsPerRegion[6]++;          
                    break;
                case "Vaasa":
                    infectionsPerRegion[7]++;
                    break;
                case "Etelä-Pohjanmaa":
                    infectionsPerRegion[8]++;
                    break;
                case "Keski-Suomi":
                    infectionsPerRegion[9]++;
                    break;
                case "Etelä-Savo":
                    infectionsPerRegion[10]++;
                    break;
                case "Itä-Savo":
                    infectionsPerRegion[11]++;
                    break;
                case "Pirkanmaa":
                    infectionsPerRegion[12]++;
                    break;
                case "Satakunta":
                    infectionsPerRegion[13]++;
                    break;
                case "Päijät-Häme":
                    infectionsPerRegion[14]++;
                    break;
                case "Kymenlaakso":
                    infectionsPerRegion[15]++;
                    break;
                case "Kanta-Häme":
                    infectionsPerRegion[16]++;
                    break;
                case "Etelä-Karjala":
                    infectionsPerRegion[17]++;
                    break;
                case "HUS":
                    infectionsPerRegion[18]++;
                    break;
                case "Varsinais-Suomi":
                    infectionsPerRegion[19]++;
                    break;
            }
        }
                    fethced = true;

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



