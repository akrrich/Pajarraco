using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected float speed;


    protected virtual void Awake()
    {
        SuscribeToUpdateManagerEvent();
        GetComponents();
    }

    // Simulacion de Update
    protected virtual void UpdateCharacterController()
    {

    }

    // Simulacion de Gizmos
    protected virtual void OnDrawGizmosCharacterController()
    {
        Collisions.DrawRectOnGizmos(transform);
    }

    protected virtual void OnDestroy()
    {
        UnsuscribeToUpdateManagerEvent();
    }


    protected virtual void GetComponents()
    {
    }


    private void SuscribeToUpdateManagerEvent()
    { 
        GameManager.Instance.UpdateManager.OnUpdate += UpdateCharacterController;
        GameManager.Instance.UpdateManager.OnDrawGizmos += OnDrawGizmosCharacterController;
    }

    private void UnsuscribeToUpdateManagerEvent()
    {
        GameManager.Instance.UpdateManager.OnUpdate -= UpdateCharacterController;
        GameManager.Instance.UpdateManager.OnDrawGizmos -= OnDrawGizmosCharacterController;
    }
}
