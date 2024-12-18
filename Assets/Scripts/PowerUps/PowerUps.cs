using UnityEngine;
using UnityEngine.Events;

public abstract class PowerUps : MonoBehaviour
{
   
    [SerializeField] public class StringEvent : UnityEvent<string> { }

    [SerializeField] protected PowerUpScriptable powerUpScriptable;
    protected PowerUpsManager powerUpsManager;

    private SpriteRenderer sr;
    private CircleCollider2D circleCollider2D;
    private AudioSource powerUpPick;

    [SerializeField] private int id;

    public int Id { get => id; }

    public UnityEvent _powerUp;

    private bool canMovePowerUp = true;


    void Start()
    {
        powerUpsManager = FindObjectOfType<PowerUpsManager>();

        sr = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        powerUpPick = GetComponent<AudioSource>();

        GameManager.Instance.GameStatePlaying += UpdatePowerUp;
    }

    void UpdatePowerUp()
    {
        MovePowerUp();
        StopPowerUp();
        DestroyPowerUp(canMovePowerUp);
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStatePlaying -= UpdatePowerUp;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            powerUpPick.Play();
            ActivePowerUp(collider);
            DestroyPowerUp();
            
            
        }
    }


    private void MovePowerUp()
    {
        if (canMovePowerUp)
        {
            transform.position += new Vector3(0, powerUpScriptable.Speed) * Time.deltaTime;
        }
    }

    private void StopPowerUp()
    {
        if (transform.position.y <= powerUpScriptable.LimitDownPowerUp)
        {
            canMovePowerUp = false;
        }
    }

    private void DestroyPowerUp()
    {
        sr.enabled = false;
        circleCollider2D.enabled = false;

        Destroy(gameObject, powerUpPick.clip.length);
    }

    private void DestroyPowerUp(bool canMovePowerUp)
    {
        if (!canMovePowerUp)
        {
            Destroy(gameObject, powerUpScriptable.LifeTime);
        }
    }


    protected abstract void ActivePowerUp(Collider2D collider);
}
