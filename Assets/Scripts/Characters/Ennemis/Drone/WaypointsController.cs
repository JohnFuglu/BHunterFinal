using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : Controller
{

    //Waypoints
    [SerializeField] List<Transform> _waypoints;
    Transform _currentWaypoint;
    GameObject _player;
    int _waypointNumber;
    [SerializeField] float _speed;
    
    [Header("Direction to shoot at")]
    [SerializeField] GameObject frontToEnnemyA;  
    [SerializeField] GameObject frontToEnnemyB;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] LineRenderer _detectionRay;
    private Vector3[] lineRendVectors = new Vector3[2];
    private bool _drawnedRay=false;
    [SerializeField] GameObject _circle;
    [SerializeField] float _circleRadius;
    protected override void Start()
    {
        base.Start();
        _currentWaypoint = _waypoints[_waypointNumber];
        // player = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        // player = PlayerPersistentDataHandler.Instance.player.GetComponent<Transform>().position;
        _player = GameObject.FindWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        MoveAIWaypoints();
        if (_shoot.ActualAmmoInClip > 0) 
        {
       
            //player = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
            DetectionDrone(_shoot.canon.position, _player.GetComponent<Transform>().position-_shoot.canon.position);//player
        }
          
        if (_shoot.Ammo > 0 && _shoot.ActualAmmoInClip ==0)
        {
            _animator.SetTrigger("Reload");
        }
        
          
        WoundCheck(character);
        DeathCheck(character) ;
        character.GetDestroyed();
    }

    void MoveAIWaypoints()
    {
        if (!Physics2D.OverlapArea(frontToEnnemyA.transform.position, frontToEnnemyB.transform.position, _playerLayer))
        {
            transform.right = _waypoints[_waypointNumber].position - transform.position;

            // Quaternion rotation = Quaternion.LookRotation(_waypoints[_waypointNumber].position - transform.position, transform.TransformDirection(Vector3.forward));
            // transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            float dist = Vector3.Distance(_waypoints[_waypointNumber].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointNumber].position, Time.deltaTime * _speed);

            if (Vector3.Distance(transform.position, _waypoints[_waypointNumber].position) < 2.5)
            {
                if (_waypointNumber + 1 < _waypoints.Count)
                    _waypointNumber++;
                else
                    _waypointNumber = 0;
            }
        }
    }

    void DetectionDrone(Vector2 origin, Vector3 detection)
    {
        hit = Physics2D.Raycast(origin, detection, _shoot.DistanceDetection);
            DrawDetectionRay(frontToEnnemyA.transform.position, frontToEnnemyB.transform.position);
        if (hit.collider != null && hit.transform.CompareTag("Player"))
            transform.right = hit.transform.position - transform.position;

        detectSomething = hit;
        Debug.DrawRay(_shoot.canon.position, GameObject.FindWithTag("Player").transform.position - _shoot.canon.position, Color.yellow);
        if (detectSomething)
        {
            if (hit.collider != null && hit.transform.CompareTag("Player")) // et si le drone avance dans le même sens du rayon, il peut tirer
            {
                target = hit.collider.gameObject;
                //if (Physics2D.OverlapArea(frontToEnnemyA.transform.position, frontToEnnemyB.transform.position, _playerLayer))
                    if (Physics2D.OverlapCircle(_circle.transform.position, _circleRadius, _playerLayer))
                        _animator.SetTrigger("Shoot");
            }
        }
    }
 //private void OnDrawGizmosSelected()
 //       {
 //           if (frontToEnnemyA == null || frontToEnnemyB == null)
 //               return;
 //               Gizmos.DrawLine(frontToEnnemyA.transform.position, frontToEnnemyB.transform.position);
 //       }

    void DrawDetectionRay(Vector2 origin, Vector3 detection) 
    {
        Vector3 originToVector3 = new Vector3(origin.x, origin.y, 0);
        Vector3 detectionToVector3 = new Vector3(detection.x, detection.y, 0);
        lineRendVectors[0] = originToVector3;
        lineRendVectors[1] = detectionToVector3;
        _detectionRay.SetPositions(lineRendVectors);
    }

    private void OnDrawGizmosSelected()
    {
        if (_circle == null)
            return;
        Gizmos.DrawWireSphere(_circle.transform.position, _circleRadius);

    }
}
