using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CrazyManManager : MonoBehaviour
{
    [Header("BoxCast")] 
    [SerializeField] private Vector3 castOffset;
    [SerializeField] private Vector3 castSize;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 distancePosition;

    [Space(35)] 
    [Header("CheckWall Cast")] 
    [SerializeField]
    private Vector3 wallCastOffset;
    [SerializeField] private Vector3 wallCastSize;
    
    [Space(35)] [SerializeField] private float runSpeed;
    private Animator _anim;
    private Rigidbody2D _rigidBody;
    [SerializeField] private GameObject suicideParticle;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        CheckPlayerOnRange();
        CheckWallOnRange();
    }

    void CheckPlayerOnRange()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + castOffset, castSize, 0, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit && hit.collider.CompareTag("Player"))
            {
                Attack(hit.collider.gameObject);
            }
        }
    }

    void CheckWallOnRange()
    {
        RaycastHit2D[] wallHits =
            Physics2D.BoxCastAll(transform.position + wallCastOffset, wallCastSize, 0, Vector2.zero);

        foreach (var hit in wallHits)
        {
            if (hit && hit.collider.CompareTag("Ground"))
            {
                Suicide(gameObject);
            }
        }
    }

    private void Attack(GameObject player)
    {
        _anim.Play("Walk");
        _rigidBody.velocity = new Vector2(runSpeed, _rigidBody.velocity.y);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position+distancePosition, distance, LayerMask.GetMask("Player"));
        foreach (var hit in hits)
        {
            if (hit && hit.gameObject.CompareTag("Player"))
            {
                Suicide(player);
            }
        }
    }

    public void Suicide(GameObject player)
    {
        Instantiate(suicideParticle,player.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        if (player.gameObject.GetComponent<PlayerTakeDamage>())
            player.gameObject.GetComponent<PlayerTakeDamage>().Die();
        Addressables.ReleaseInstance(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + castOffset, castSize);
        Gizmos.DrawWireCube(transform.position + wallCastOffset, wallCastSize);
        Gizmos.DrawWireSphere(transform.position+distancePosition, distance);
    }
}