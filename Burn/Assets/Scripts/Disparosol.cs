using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparosol : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto base de disparo (en el centro del Sol)
    public float warningTime = 1.5f; // Tiempo total antes de disparar
    public float projectileSpeed = 10f;
    public float detectionRange = 5f; // Rango de detección del jugador
    public float fireRadius = 1.5f; // Distancia desde el centro del Sol para disparar
    public LineRenderer lineRenderer;
    public LayerMask playerLayer;

    private Transform targetPlayer;

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (player != null && targetPlayer == null)
        {
            targetPlayer = player.transform;
            StartCoroutine(ShootSequence());
        }
    }

    private IEnumerator ShootSequence()
    {
        if (targetPlayer == null) yield break;

        Vector2 direction;

        // Esperar (warningTime - 1) segundos antes de activar la línea
        yield return new WaitForSeconds(warningTime - 1f);

        if (targetPlayer == null) yield break; // Si el jugador ya no está, cancelar el disparo

        // Obtener la posición actual del jugador para la advertencia
        Vector2 targetPosition = targetPlayer.position;
        direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 newFirePoint = (Vector2)transform.position + direction * fireRadius;

        // Activar la línea de advertencia en el último segundo
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, newFirePoint);
        lineRenderer.SetPosition(1, targetPosition);

        // Esperar 1 segundo con la línea encendida
        yield return new WaitForSeconds(1f);

        if (targetPlayer == null) yield break; // Si el jugador se fue, cancelar el disparo

        // Obtener la posición del jugador justo antes de disparar (para evitar disparar a una posición vieja)
        
        direction = (targetPosition - (Vector2)transform.position).normalized;
        newFirePoint = (Vector2)transform.position + direction * fireRadius;

        // Justo antes de disparar, ocultar la línea
        lineRenderer.enabled = false;

        // Instanciar proyectil y lanzarlo hacia la posición más reciente
        GameObject projectile = Instantiate(projectilePrefab, newFirePoint, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        targetPlayer = null; // Reiniciar objetivo
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Dibujar el radio de disparo alrededor del Sol
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
}
