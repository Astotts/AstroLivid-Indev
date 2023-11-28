using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI incomeTxt;
    [SerializeField] private TMPro.TextMeshProUGUI creditsTxt;
    private static float creditTimer = 10f;

    //------------------------------------------------
    private int income = 100;
    private float displayedCredits = 0;
    private float credits = 1000;
    private float time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        incomeTxt.text = "";
        creditsTxt.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        time -= 1f * Time.deltaTime;
        if(time <= 0){
            credits += income;
            time = 5f;
        }

        if(displayedCredits != credits){
            //Debug.Log(displayedCredits + " " + credits + " " + creditTimer);
            displayedCredits = Mathf.Lerp(displayedCredits, credits, creditTimer * Time.deltaTime);
        }
    }

        void FixedUpdate(){
        if(credits > 999999f){
            creditsTxt.text = Mathf.RoundToInt(displayedCredits/1000000) + "M";
        }
        else if(credits > 9999f){
            creditsTxt.text = Mathf.RoundToInt(displayedCredits/1000) + "K";
        }
        else{
            creditsTxt.text = Mathf.RoundToInt(displayedCredits) + "";
        }

        incomeTxt.text = income.ToString();
    }

    public bool Purchase(int cost, int income_){
        if(credits >= cost){
            credits -= cost;
            income += income_;
            return true;
        }
        return false;
    } 
}
