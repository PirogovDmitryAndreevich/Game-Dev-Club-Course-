using System;
using UnityEngine;

[RequireComponent (typeof(Fliper))]
public class EnemyDirectionOfView : MonoBehaviour
{
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private LayerMask _targetLayer;

    private Fliper _fliper;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookAreaOrigin(), _seeAreaSize);
    }

    public bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookAreaOrigin(), _seeAreaSize, 0f, _targetLayer);

        if (hit != null)
        {
            Debug.Log("Hit: " + hit.gameObject.name);
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize.x, ~(1 << gameObject.layer));

            if (hit2D.collider != null)
            {
                Debug.Log("Hit2D: " + hit2D.collider.gameObject.name);

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
        }

        return false;
    }

    private Vector2 GetLookAreaOrigin()
    {
        float halfCoefficient = 2.0f;
        float directionCoefficient = _fliper?.IsTernRight ?? true ? 1 : -1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoefficient * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }

    internal bool TrySeeTarget(out object target)
    {
        throw new NotImplementedException();
    }
}
