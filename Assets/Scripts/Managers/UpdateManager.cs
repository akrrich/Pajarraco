using System;

public class UpdateManager
{
    private event Action onUpdate;
    private event Action onDrawGizmos;

    public Action OnUpdate { get => onUpdate; set => onUpdate = value; }
    public Action OnDrawGizmos { get => onDrawGizmos; set => onDrawGizmos = value; }


    public void Update()
    {
        onUpdate?.Invoke();
    }

    public void DrawGizmos()
    {   
        onDrawGizmos?.Invoke();  
    }
}
