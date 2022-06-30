using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HintButton : MonoBehaviour
{
    public static Action OnClick;
   
    //public int buttonNumber;

    public Sprite imageOfHint;
    public string descritpionTextHint;
    [SerializeField] private Image _zoomedImage;
    [SerializeField] private Text _zoomedText;


    public void ShowZoomedHint() 
    {
        SelectAPlaceOfHunt.Instance.zoomedPanelTransform.gameObject.SetActive(true);
        _zoomedImage = GameObject.Find("ZoomedImage").GetComponent<Image>();
        _zoomedImage.sprite = imageOfHint;
        _zoomedText = GameObject.Find("ZoomedDescription").GetComponent<Text>();
        _zoomedText.text = descritpionTextHint;
        StartCoroutine(HideZoomedHint());
    }

    IEnumerator HideZoomedHint() 
    {
        yield return new WaitForSeconds(3f);
        SelectAPlaceOfHunt.Instance.zoomedPanelTransform.gameObject.SetActive(false);
    }

   
}

