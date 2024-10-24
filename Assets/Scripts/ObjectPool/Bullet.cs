using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletPool bulletPool;

    private Rigidbody2D rb;
    private Animator shoot;
    private SpriteRenderer sr;
    private CapsuleCollider2D capsule;
    private AudioSource audioShoot;

    private float speed = 5f;
    private float lifeTime = 3f;

    private Vector2 offsetBulletPosition = new Vector2(0, 0.8f);


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
            sr.enabled = false;
            capsule.enabled = false;

            StartCoroutine(ReturnToPoolAfterAudio());
        }
    }

    void Update()
    {
        //PauseManager.PauseAndUnPauseSounds(audioShoot);
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
        sr = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();
        audioShoot = GetComponent<AudioSource>();

        rb.isKinematic = false;
        sr.enabled = true;
        capsule.enabled = true;

        rb.velocity = Vector2.up * speed;

        audioShoot.Play();
        shoot.SetTrigger("Shoot");

        Invoke("ReturnToPool", lifeTime);
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
