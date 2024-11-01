using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    [SerializeField] protected PowerUpScriptable powerUpScriptable;
    protected PowerUpsManager powerUpsManager;

    private SpriteRenderer sr;
    private BoxCollider2D box;
    private AudioSource powerUpPick;

    [SerializeField] private int id;

    public int Id { get => id; }


    private bool canMovePowerUp = true;


    void Start()
    {
        powerUpsManager = FindObjectOfType<PowerUpsManager>();

        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        powerUpPick = GetComponent<AudioSource>();

        GameManager.Instance.GameStatePlaying += UpdatePowerUp;
    }

    void UpdatePowerUp()
    {
        MovePowerUp();
        StopPowerUp();
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
        box.enabled = false;

        Destroy(gameObject, powerUpPick.clip.length);
    }


    protected abstract void ActivePowerUp(Collider2D collider);
}
