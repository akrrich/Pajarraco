using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private BulletPool bulletPool;

    private Rigidbody2D rb;
    private Animator shoot;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsule;
    private AudioSource audioShoot;

    private static int damage = 1;

    private static float speed = 6f;

    private Vector2 offsetBulletPosition = new Vector2(0, 0.8f);

    public static float Speed { get => speed; set => speed = value; }


    void Start()
    {
        GameManager.Instance.GameStateDefeated += StopPhysics;
        GameManager.Instance.GameStateWin += StopPhysics;
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStateDefeated -= StopPhysics;
        GameManager.Instance.GameStateWin -= StopPhysics;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.isKinematic = true;
        spriteRenderer.enabled = false;
        capsule.enabled = false;

        StartCoroutine(ReturnToPoolAfterAudio());
    }


    public static void ApplyDamge(Enemy enemy)
    {
        enemy.Life -= damage;
    }

    public void InstantiateBullet(Transform playerPosition, BulletPool pool)
    {
        bulletPool = pool;

        transform.position = playerPosition.position + (Vector3)offsetBulletPosition;
        Initialize();
    }


    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
        audioShoot = GetComponent<AudioSource>();

        rb.isKinematic = false;
        spriteRenderer.enabled = true;
        capsule.enabled = true;

        rb.velocity = Vector2.up * speed;

        audioShoot.Play();
    }

    private void StopPhysics()
    {
        ReturnToPool();
    }

    private IEnumerator ReturnToPoolAfterAudio()
    {
        yield return new WaitWhile(() => audioShoot.isPlaying);

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        bulletPool.ReturnBulletToPool(this);
    }
}
