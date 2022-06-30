using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip[] _buttonSfx;

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Clique");
        _audio.PlayOneShot(_buttonSfx[1]);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Dessus");
        _audio.PlayOneShot(_buttonSfx[0]);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _audio.PlayOneShot(_buttonSfx[2]);
    }

}
