using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    Vector2 position;
    bool contact = false;

    Color baseColor;
    private void Start()
    {
        baseColor= GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        contact = true;
        Color contactC =  new Color();
        contactC = Color.blue;
        GetComponent<SpriteRenderer>().color = contactC;
        StartCoroutine(ContactCD());
    }

    private void OnMouseDrag()
    {
        if (!contact)
        {
            Plane dragPlane = new Plane(Camera.main.transform.forward, transform.position);
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;
            if (dragPlane.Raycast(camRay, out enter))
            {
                Vector3 fingerPosition = camRay.GetPoint(enter);
                transform.position = fingerPosition;
            }
        }
    }

    IEnumerator ContactCD() {
        yield return new WaitForSeconds(1);
        contact = false;
        GetComponent<SpriteRenderer>().color = baseColor;
    }
}
