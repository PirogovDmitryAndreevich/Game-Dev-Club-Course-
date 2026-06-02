using System;
using System.Linq;
using UnityEngine;

public class EnemyDirectionOfView : MonoBehaviour
{
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private float _ariaSizeRadius;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _ignoreLayers;

    Collider2D[] _hits = new Collider2D[1];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _ariaSizeRadius);
    }

    public bool TrySeeTarget(out Transform target)
    {
        target = null;

        Physics2D.OverlapCircleNonAlloc(transform.position, _ariaSizeRadius,_hits, _targetLayer);

        Collider2D hit = _hits.FirstOrDefault();

        if (hit == null)
            return false;

        Vector2 direction = (hit.transform.position - transform.position).normalized;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _ariaSizeRadius, ~_ignoreLayers);

        if (hit2D.collider != null)
        {
            if (hit2D.collider == hit)
            {
                Debug.DrawLine(transform.position, hit2D.point, Color.red);
                target = hit2D.transform;
                return true;
            }
            else
            {
                Debug.DrawLine(transform.position, hit2D.point, Color.white);
            }
        }

        return false;
    }
}
