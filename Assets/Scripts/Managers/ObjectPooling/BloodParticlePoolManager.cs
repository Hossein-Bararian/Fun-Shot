using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodParticlePoolManager : MonoBehaviour
{
   
    public static BloodParticlePoolManager Instance;
    private ObjectPool<GameObject> _particlePool;
    [SerializeField] private GameObject bloodParticlePrefabs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _particlePool = new ObjectPool<GameObject>(OnCreateParticle, OnGetParticle, OnReleaseParticle,
            OnDestroyParticle, true, 20, 40);
    }

    private void OnDestroyParticle(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnReleaseParticle(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnGetParticle(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private GameObject OnCreateParticle()
    {
        return Instantiate(bloodParticlePrefabs);  
    }
   
    public GameObject GetParticle()
    {
        return _particlePool.Get();
    }

    public void ReleaseParticle(GameObject obj)
    { 
        if (obj.activeSelf)
        {
            _particlePool.Release(obj);
        }
    }
}
