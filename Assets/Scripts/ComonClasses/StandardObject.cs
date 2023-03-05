using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StandardObject : MonoBehaviour, IDamageable, IDestructible
{
    [SerializeField] float _healthAssign;
    [SerializeField] int _scoreAssign;
    [SerializeField] bool _destroyedChecker;


    public float Health { get { return _healthAssign; } set { _healthAssign = value; } }
    public int Score { get { return _scoreAssign; } set { _scoreAssign = value; } }
    public float TempHp { get; set; }
    public GameObject destroyedObjectPrefab;

    public bool Destroyed { get { return _destroyedChecker; } set { _destroyedChecker = value; } }

    protected SpawnHintOnDestroy SpawnHintOnDestroy;

    [Header("DestructionAnimation")]

    [SerializeField] protected GameObject onDestructionFxPrefab;

    AnimationInactiveSelf fxAnimationScript;
    Transform whereToSpawn;

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
    }
    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
    }


    public void GiveHint(GameObject thisgObject) 
    {
        if (!gameObject.CompareTag("Player") && !gameObject.CompareTag("Ennemis"))
        {
            SpawnHintOnDestroy = new SpawnHintOnDestroy(gameObject);
        }

    }
    #region("Destruction + DestructionFX")


    public void GetDestroyed()
    {
        if (Health <= 0)
        {
            if (gameObject.GetComponent<HintAllocator>())
            {
                GiveHint(gameObject);
                ExploEnd(onDestructionFxPrefab);
                Destroy(gameObject);
            }
            else
            { 
                ExploEnd(onDestructionFxPrefab);
                PlayerPersistentDataHandler.Instance.PlayerScore += Score;
            }

        }
    }
    public void ExploEnd(GameObject ExplosionFxPrefab)
    {
        GameObject go = Instantiate(ExplosionFxPrefab);
        go.transform.position = transform.position;
        fxAnimationScript = go.GetComponent<AnimationInactiveSelf>();
        InstantiateDestroyedAndDestroy(destroyedObjectPrefab, go.transform);
    }
    public void InstantiateDestroyedAndDestroy(GameObject prefab, Transform intactObjPosition)
    {   
        GameObject destroyedObject = Instantiate(destroyedObjectPrefab);
        destroyedObject.transform.parent = null;
        destroyedObject.transform.position = intactObjPosition.position;//transform.position
        Destroy(gameObject);
    }
    #endregion
}
