using Cinemachine;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField]private GameOverManager gameOverManager;
    private PlayerTakeDamage _playerTakeDamage;


    private void Awake()
    {
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        gameOverManager = FindObjectOfType<GameOverManager>(true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerTakeDamage>())
            {
                _playerTakeDamage = other.GetComponent<PlayerTakeDamage>();
                cinemachine.Follow = null;
                _playerTakeDamage.Die();
            }

        }
    }
}
