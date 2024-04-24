using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]private GameOverManager gameOverManager;
    private PlayerTakeDamage _playerTakeDamage;

    private IEnumerator OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerTakeDamage>())
            {
                
                _playerTakeDamage = other.gameObject.GetComponent<PlayerTakeDamage>();
                _playerTakeDamage.Die();
                yield return new WaitForSeconds(1);
                gameOverManager.ActiveGameOverPanel(true);
            }

        }
    }
}