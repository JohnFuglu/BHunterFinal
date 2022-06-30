using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LaserVertical : MonoBehaviour
{
    [SerializeField] float _laserSpeed;
    SpriteRenderer _visuLaser;
    Light2D _light;
    Rigidbody2D _rb;
    Vector3 startPos;
    void Start()
    {
        startPos=this.transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _light = GetComponentInChildren<Light2D>();
        _visuLaser = GetComponent<SpriteRenderer>();
        _rb.AddForce(Vector2.down * RandomiseSpeed(_laserSpeed));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hero>()== null) {
            transform.position = startPos;
        }
        if (collision.gameObject.GetComponent<Hero>() != null)
        {
            collision.gameObject.GetComponent<Hero>().Health=0;
        }
    }

    float RandomiseSpeed(float originalSpeed) 
    {
        float speed = Random.Range(1, originalSpeed);
        return speed;
    }


}
