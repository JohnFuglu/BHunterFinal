using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class HintButton : MonoBehaviour
{
    public static Action OnClick;
   
    public Hint hint;
    public Sprite imageOfHint;
    public string descritpionTextHint;

     [SerializeField] private Image zoomPlace;
    [SerializeField] private TextMeshProUGUI textPlace;


    public void ShowZoomedHint() 
    {
        SelectAPlaceOfHunt.Instance.zoomedPanelTransform.gameObject.SetActive(true);
        zoomPlace = GameObject.Find("ZoommedImage").GetComponent<Image>();
        zoomPlace.sprite = hint.descriptionImage;
        textPlace = GameObject.Find("TextDescription").GetComponent<TextMeshProUGUI>();
        textPlace.text = hint.preciseDescription;
        StartCoroutine(HideZoomedHint());
    }

    IEnumerator HideZoomedHint() 
    {
        yield return new WaitForSeconds(3f);
        SelectAPlaceOfHunt.Instance.zoomedPanelTransform.gameObject.SetActive(false);
    }

   
}

