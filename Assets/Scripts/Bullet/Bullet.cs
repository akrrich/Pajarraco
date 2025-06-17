using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private ObjectPooler bulletPool;

    // Revisar esto si hay que cambiarlo de lugar
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] private string poolNameInHieararchy;

    protected Vector2 direction;


    protected virtual void Awake()
    {
        SuscribeToUpdateManagerEvent();
        GetComponents();
    }

    //Simulacion Update
    protected virtual void UpdateBullet()
    {
        Movement();
        CheckCollisions();
    }

    // Simulacion de Gizmos
    protected virtual void OnDrawGizmosBullet()
    {
        Collisions.DrawRectOnGizmos(transform);
    }

    void OnDestroy()
    {
        UnsuscribeToUpdateManagerEvent();
    }


    private void SuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate += UpdateBullet;
        GameManager.Instance.UpdateManager.OnDrawGizmos += OnDrawGizmosBullet;
    }

    private void UnsuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateBullet;
        GameManager.Instance.UpdateManager.OnDrawGizmos -= OnDrawGizmosBullet;
    }

    protected virtual void GetComponents()
    {
        bulletPool = GameObject.Find(poolNameInHieararchy).GetComponent<ObjectPooler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Movement()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    protected abstract void CheckCollisions();

    protected void OnReturnBulletToPool()
    {
        transform.position = bulletPool.transform.position;
        bulletPool.ReturnObjectToPool(this);
    }


    public void OnActiveBullet(Transform startPosition, Vector2 dir)
    {
        transform.position = startPosition.position;
        direction = dir.normalized;
    }
}
