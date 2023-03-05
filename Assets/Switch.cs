using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public  delegate void OnAction();
    public static OnAction switched;

    [SerializeField] Sprite on;
    Sprite off;
    SpriteRenderer rd;
    private void Start()
    {
       rd= GetComponent<SpriteRenderer>();
        off = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (Input.GetKeyDown(KeyCode.E))
                Activate();

    }
    void Activate() {
        rd.sprite = on;
        if (switched != null)
            switched();
    }
}
