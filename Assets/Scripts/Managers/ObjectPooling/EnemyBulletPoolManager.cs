using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPoolManager : MonoBehaviour
{
 
    public static EnemyBulletPoolManager Instance;
    [SerializeField] private GameObject enemyBulletPrefab;
    private ObjectPool<GameObject> _bulletPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bulletPool =
            new ObjectPool<GameObject>(OnCreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true, 10, 20);
    }

    private void OnDestroyBullet(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnReleaseBullet(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnGetBullet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private GameObject OnCreateBullet()
    {
        return Instantiate(enemyBulletPrefab);
    }

    public GameObject GetBullet()
    {
        return _bulletPool.Get();
    }

    public void ReleaseBullet(GameObject bullet)
    {
        _bulletPool.Release(bullet);
    }
}
