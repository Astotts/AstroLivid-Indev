using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceManager : MonoBehaviour
{
    public float credits;
    private float displayedCredits;
    private float creditTimer = 10f;
    public float power;
    public float powerUsed;
    public float population; 


    [SerializeField] private GameObject creditCount;
    private Text creditDisplay;
    [SerializeField] private GameObject powerCount;
    private Text powerDisplay;
    [SerializeField] private GameObject populationCount;
    private Text populationDisplay;

    // Start is called before the first frame update
    void Start()
    {
        creditDisplay = this.creditCount.GetComponent<Text>();
        powerDisplay = powerCount.GetComponent<Text>();
        populationDisplay = populationCount.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(displayedCredits != credits){
            //Debug.Log(displayedCredits + " " + credits + " " + creditTimer);
            displayedCredits = Mathf.Lerp(displayedCredits, credits, creditTimer * Time.deltaTime);
        }
    }

    void FixedUpdate(){
        if(credits > 999999f){
            creditDisplay.text = "Credits " + Mathf.RoundToInt(displayedCredits/1000000) + "M";
        }
        else if(credits > 9999f){
            creditDisplay.text = "Credits " + Mathf.RoundToInt(displayedCredits/1000) + "K";
        }
        else{
            creditDisplay.text = "Credits " + Mathf.RoundToInt(displayedCredits);
        }
        
        powerDisplay.text = "Power " + powerUsed + "/" + power;
        populationDisplay.text = "Population " + population;
    }
}
