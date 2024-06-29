using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CraneScript : MonoBehaviour
    {

        public Rigidbody2D _lifted;
        Transform _thisTransform;
        [SerializeField] float rotationSpeed, liftSpeed;
        [SerializeField] string _direction; //trash


        [SerializeField] Collider2D _grueAvant, _grueCabine, _maxUp, _maxDown, _maxGrue;
        [SerializeField] Collider2D[] _cordeColliders;
        string temp;
        Collider2D _player;
        private void Start()
        {
            _thisTransform = GetComponent<RectTransform>();
        }
        
       /*
        public void CraneCommand(string direction)
        {
            if (direction != null)
            {
                if (direction == "RotateRight")
                    _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * rotationSpeed);

                if (direction == "RotateLeft")
                    _thisTransform.rotation = Quaternion.Slerp(_thisTransform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);


                if (direction == "Up" && !_maxUp.IsTouching(_maxGrue))
                {
                    // _lifted.constraints = RigidbodyConstraints2D.None;
                    _lifted.transform.Translate(Vector2.Lerp(_lifted.transform.position, Vector2.up * liftSpeed, 5f) * Time.deltaTime);
                    StartCoroutine(Constraints());
                }

                if (direction == "Down" && !_maxDown.IsTouching(_maxGrue))
                {
                    _lifted.transform.Translate(Vector2.Lerp(_lifted.transform.position, Vector2.down * liftSpeed, 5f) * Time.deltaTime);
                    StartCoroutine(Constraints());
                }

                if (direction == "Left" && !CheckForCollision(_grueAvant))// _grueCorde.IsTouching(_grueAvant)
                {
                    _lifted.transform.Translate(Vector2.Lerp(_lifted.transform.position, Vector2.left * liftSpeed, 5f) * Time.deltaTime);
                    StartCoroutine(Constraints());
                }
                if (direction == "Right" && !CheckForCollision(_grueCabine))
                {
                    _lifted.transform.Translate(Vector2.Lerp(_lifted.transform.position, Vector2.right * liftSpeed, 5f) * Time.deltaTime);
                    StartCoroutine(Constraints());
                }
            }

        }

        bool CheckForCollision(Collider2D notToTouch)
        {
            foreach (Collider2D coll in _cordeColliders)
            {
                if (coll.IsTouching(notToTouch))
                    return true;
            }
            return false;

        }

        IEnumerator Constraints()
        {
            yield return new WaitForSeconds(1f);
            _lifted.constraints = RigidbodyConstraints2D.FreezePosition;
        }*/
    }
