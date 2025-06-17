using UnityEngine;

public class Collisions
{
    public static bool CollisionBetweenRects(Transform self, Transform target)
    {
        if (self.position.x + self.localScale.x / 2 >= target.position.x - target.localScale.x / 2 &&
            self.position.x - self.localScale.x / 2 <= target.position.x + target.localScale.x / 2 &&
            self.position.y + self.localScale.y / 2 >= target.position.y - target.localScale.y / 2 &&
            self.position.y - self.localScale.y / 2 <= target.position.y + target.localScale.y / 2)
        {
            return true;
        }

        return false;
    }

    public static bool CollisionBetweenCircles(Transform self, Transform target)
    {
        if (Vector2.Distance(self.position, target.position) <= (self.localScale.x / 2 + target.localScale.x / 2))
        {
            return true;
        }

        return false;
    }

    public static bool CollisionWithLeftEdge(Transform self, Transform target)
    {
        if (self.position.x - self.localScale.x / 2 <= target.position.x + target.localScale.x / 2)
        {
            return true;
        }

        return false;
    }

    public static bool CollisionWithDownEdge(Transform self, Transform target)
    {
        if (self.position.y - self.localScale.y / 2 <= target.position.y + target.localScale.y / 2)
        {
            return true;
        }

        return false;
    }

    public static bool CollisionWithRightEdge(Transform self, Transform target)
    {
        if (self.position.x + self.localScale.x / 2 >= target.position.x - target.localScale.x / 2)
        {
            return true;
        }

        return false;
    }

    public static void DrawRectOnGizmos(Transform self)
    {
        if (self == null) return;

        Gizmos.color = Color.green;

        Vector2 center = self.position;
        Vector2 halfSize = new Vector2(self.localScale.x / 2f, self.localScale.y / 2f);

        Vector3 topLeft = new Vector3(center.x - halfSize.x, center.y + halfSize.y, 0);
        Vector3 topRight = new Vector3(center.x + halfSize.x, center.y + halfSize.y, 0);
        Vector3 bottomRight = new Vector3(center.x + halfSize.x, center.y - halfSize.y, 0);
        Vector3 bottomLeft = new Vector3(center.x - halfSize.x, center.y - halfSize.y, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);        
    }

    public static void DrawCircleOnGizmos(Transform self, int segments = 32)
    {
        if (self == null) return;

        Gizmos.color = Color.green;

        Vector3 center = self.position;
        float radius = self.localScale.x / 2f;

        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(Mathf.Cos(0), Mathf.Sin(0), 0) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
