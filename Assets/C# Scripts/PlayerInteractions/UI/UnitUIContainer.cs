using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUIContainer : MonoBehaviour
{
    private ushort tally;
    [SerializeField] private Sprite[] unitImage;

    public List<UnitIdentifyer> unitList;

    public UnitVariant variant;

    [Header("Component Values")]
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private TMPro.TextMeshProUGUI text;

    public void SetUpContainer(UnitVariant variant_){
        this.variant = variant_;
        //Debug.Log((int)variant);
        this.image.sprite = unitImage[(int)variant_];
        this.gameObject.SetActive(true);
    }

    public void ClearContainer(){
        this.gameObject.SetActive(false);
        text.text = "";
        image.sprite = null;
        variant = UnitVariant.None;
        unitList.Clear();
    }

    void Update(){
        if(unitList.Count > 0){
            tally = (ushort)unitList.Count;
            text.text = tally.ToString();
        }
        else{
            this.gameObject.SetActive(false);
            text.text = "";
            image.sprite = null;
            variant = UnitVariant.None;
        }
    }
}
