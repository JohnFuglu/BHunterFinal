using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockerMachine : MonoBehaviour
{
   
    [SerializeField] bool _canMove = true;
    [SerializeField] float _maxDistance, _dropChance;
    [SerializeField] Collider2D _thisCollider;
    [SerializeField] Vector2 _direction;
    [SerializeField] BoxCollider2D _trigger;
    GameObject _startPoint;
    
    
    
    ColliderDistance2D _distance;
    float _speed;
    Rigidbody2D _rb;
    BoxCollider2D _boxColl;
    SpringJoint2D[] _springs;
    bool _droppedLoad = false;
    Collider2D _player;

    void Start()
    {
        _speed = _direction.x;
        CreateReferencePoint();
        _springs = GetComponentsInChildren<SpringJoint2D>();
        _player = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    }

    void Update()
    {
        if(!_droppedLoad && _canMove)
            Move(TurnBack());
        
        if (!_droppedLoad && _trigger.IsTouching(_player))
        {
            DropLoad();
        }
       
    }

    void DropLoad() 
    {
            RandomChance(_dropChance);
    }

    void RandomChance(float floatForPercent) 
    {
       if(Random.value <= floatForPercent) 
       {
            foreach(SpringJoint2D spring in _springs) 
            {
                Destroy(spring);
                _droppedLoad = true;
            }
       }
    }

    Vector2 TurnBack() 
    {
        ColliderDistance2D dist = Physics2D.Distance(_boxColl, _thisCollider);
        if (dist.distance > _maxDistance)
        {
            _direction = -_direction;
            return _direction;
        }
        if(_thisCollider.IsTouching(_boxColl))
        {
            _direction = -_direction;
            return _direction;
        }
        else return _direction;
    }

    private void Move(Vector2 direction)
    {
        transform.Translate(direction*_speed*Time.deltaTime);
    }
    void CreateReferencePoint() 
    {
        Move(_direction);
      
        _startPoint = new GameObject("DockerStartingPoint", typeof(Rigidbody2D), typeof(BoxCollider2D));
        //Instantiate(_startPoint);

        Vector3 v = new Vector3(this.transform.position.x - ((GetComponent<SpriteRenderer>().bounds.size.x / 2)+4), this.transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2), 0);
        _startPoint.transform.position =v;
        _rb = _startPoint.GetComponent<Rigidbody2D>();
        _boxColl = _startPoint.GetComponent<BoxCollider2D>();
        _rb.isKinematic = true;
        _boxColl.isTrigger = true;
    }
}
