using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling {
    public class PooledObject : MonoBehaviour, IPoollable
    {
        [SerializeField] string _nameInPool;
        public string NameInPool { get { return _nameInPool; } set { _nameInPool = value; } }

        protected virtual void OnEnable()
        {
            ActionOnEnable();
        }



        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        protected virtual void ActionOnEnable()
        {
            Invoke("Hide", 5f);
        }
    }
}
