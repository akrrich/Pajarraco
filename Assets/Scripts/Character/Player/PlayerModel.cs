using UnityEngine;

public class PlayerModel
{
    private ObjectPooler bulletPlayerPool;

    private Transform rightColumn;
    private Transform leftColumn;
    private Transform floor;

    private int life = 3;
    private int minLife = 1;

    private float jumpForce = 8f;

    public Transform RightColumn { get => rightColumn; }
    public Transform LeftColumn { get => leftColumn; }
    public Transform Floor { get => floor; }


    public PlayerModel()
    {
        FindObjects();
    }


    private void FindObjects()
    {
        rightColumn = GameObject.Find("RightWall").transform;
        leftColumn = GameObject.Find("LeftWall").transform;
        floor = GameObject.Find("Floor").transform;

       // bulletPlayerPool = GameObject.Find("BulletPlayerPool").GetComponent<ObjectPooler>();
    }

    private void Death()
    {
        Debug.Log("Murio");
    }


    public void Attack(Transform firePosition, Vector2 dir)
    {
        GameManager.Instance.AudioManager.PlaySFX("PlayerShoot");
        GameManager.Instance.FireBullet(firePosition.position,dir,false, 20f);
    }

    public void Jump(CustomRigidBody customRB)
    {
        GameManager.Instance.AudioManager.PlaySFX("Jump");

        customRB.Velocity = new Vector2(customRB.Velocity.x, jumpForce);
    }

    public void GetDamage(int damage)
    {
        GameManager.Instance.AudioManager.PlaySFX("PlayerDamage");

        life -= damage;

        if (life < minLife)
        {
            Death();
        }
    }
}
