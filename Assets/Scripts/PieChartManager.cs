using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class PieChartManager : MonoBehaviour
{
    public TextMeshProUGUI[] textVersions;
    public float[] values;
    public Image circlePrefab;
    public Color[] circleColors;
    public FetchFromHS fetchFromHS;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForLoading());
    }
    public IEnumerator WaitForLoading()
    {
        while (!fetchFromHS.fethced)
        {
            yield return null;
        }
        MakeGraph();
    }
    public void MakeGraph()
    {
        values[0] = fetchFromHS.confirmedCases;
        values[1] = fetchFromHS.deathCases;
        values[2] = fetchFromHS.recoveries;
        for (int i = 0; i < 3; i++)
        {
            textVersions[i].text = values[i].ToString();
        }


        float total = 0f;
        float zRotation = 0f;
        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }
        for (int i = 0; i < values.Length; i++)
        {
            Image newCircle = Instantiate(circlePrefab) as Image;
            newCircle.transform.SetParent(transform, false);
            newCircle.color = circleColors[i];
            newCircle.fillAmount = values[i] / total;
            newCircle.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= newCircle.fillAmount * 360f;
        }
    }


}
