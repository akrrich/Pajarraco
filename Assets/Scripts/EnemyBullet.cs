using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D capsule;
    private AudioSource audioShoot;

    private static int damage = 1;

    private float speed = 5f;
    private float lifeTime = 3f;

    private Vector2 offsetBulletPosition;

    public Vector2 OffsetBulletPosition {  get => offsetBulletPosition; }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CircleCollider2D>();
        audioShoot = GetComponent<AudioSource>();

        audioShoot.Play();
        rb.velocity = Vector2.down * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            rb.isKinematic = true;
            sr.enabled = false;
            capsule.enabled = false;

            Destroy(gameObject);
        }
    }

    public static void ApplyDamge(Player player)
    {
        player.Life -= damage;
    }
}
