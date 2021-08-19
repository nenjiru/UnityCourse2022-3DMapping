using UnityEngine;

/// <summary>
/// </summary>
public class ViewPointGizmo : MonoBehaviour
{
    #region DEFINITION
    #endregion

    #region VARIABLE
    public Color drawColor = Color.yellow;

    Ray _ray;
    float _depth;
    #endregion

    #region UNITY_EVENT
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Color tmpColor = Gizmos.color;
        var color = drawColor;
        color.a = 0.5f;
        Gizmos.color = color;

        _ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(_ray, out RaycastHit hit, 1000f))
        {
            _depth = hit.distance;
        }
        Debug.DrawRay(_ray.origin, _ray.direction * _depth, drawColor);

        var pos = transform.position;
        pos.y *= 0.5f;
        Gizmos.DrawCube(pos, new Vector3(30f, transform.position.y, 20f));
        Gizmos.color = tmpColor;
    }
#endif
    #endregion

    #region PUBLIC_METHODS
    #endregion

    #region PRIVATE_METHODS
    #endregion
}