using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool moving;
    float baseSpeed;
    Vector3 startPos;
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] AudioClip sound;

    [SerializeField] protected enum trapType {SawTrap, SpikesTrap }
    [SerializeField] protected trapType traps;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (moving && traps==trapType.SawTrap) {
           baseSpeed+=speed;
           transform.Rotate(new Vector3(0,0,speed));
        }
        if (moving && traps == trapType.SpikesTrap)
        {
            float y = Mathf.PingPong(Time.time * speed, 1);
           transform.position = new Vector3(startPos.x, startPos.y+y);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            hero.TakeDamage(damage);
        }
    }
}
