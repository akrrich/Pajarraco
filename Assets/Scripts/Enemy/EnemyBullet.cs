using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;
    private AudioSource audioShoot;

    private static int damage = 1;

    private static float speed = 7f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        audioShoot = GetComponent<AudioSource>();

        audioShoot.Play();
        rb.velocity = Vector2.down * speed;

        GameManager.Instance.GameStateLose += StopPhysics;
        GameManager.Instance.GameStateWin += StopPhysics;
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStateLose -= StopPhysics;
        GameManager.Instance.GameStateWin -= StopPhysics;

        speed = 7f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckAllCollision(collision);
    }


    public static void ApplyDamge(Player player)
    {
        player.Life -= damage;
    }

    public void IncreaseSpeed()
    {
        float enemyBulletSpeedEnhacer = 0.3f;
        speed += enemyBulletSpeedEnhacer;
    }


    private void StopPhysics()
    {
        Destroy(gameObject);
    }

    private void CheckAllCollision(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Destroy(gameObject);
            break;

            case "Floor":
                rb.velocity = Vector2.zero;
                StartCoroutine(FadeOutSprite());
            break;

            case "EnemyBullet":
                Destroy(gameObject);
                Destroy(collision.gameObject);
            break;
        }
    }

    private IEnumerator FadeOutSprite()
    {
        float fadeDuration = 2f; 
        float fadeStep = 0.075f;

        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += fadeStep;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return new WaitForSeconds(fadeStep);
        }

        Destroy(gameObject);
    }
}
