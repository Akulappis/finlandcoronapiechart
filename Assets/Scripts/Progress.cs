using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Progress : MonoBehaviour
{
    public FetchFromHS dataScript;
    public Slider slider;
    public Gradient gradient;
    public Image image;
    public float fillValue = 0;
    public float currentPercentage = 0;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(ChangeColor());
    }
    public IEnumerator ChangeColor()
    {
        while(!dataScript.fethced){
            yield return null;
        }
        
        fillValue = dataScript.infectionsPerRegion[id];
        currentPercentage = (fillValue) / 100;
        image.color = gradient.Evaluate(currentPercentage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
