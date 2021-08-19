using UnityEngine;
using UnityEditor;

/// <summary>
/// </summary>
public class ProjectorGizmo : MonoBehaviour
{
    #region DEFINITION
    #endregion

    #region VARIABLE
    public Color drawColor = Color.cyan;
    public Vector3 size = new Vector3(30f, 10f, 24f);
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    Projector projector;
    #endregion

    #region UNITY_EVENT
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (projector == null)
        {
            projector = GetComponent<Projector>();
        }

        Color tmpColor = Gizmos.color;
        Gizmos.color = drawColor;

        var pos = transform.position;
        pos.z -= size.z / 2f;
        Gizmos.DrawCube(pos, size);
        Gizmos.color = tmpColor;

        pos = transform.position;
        Handles.Label(transform.position + offset, $"{gameObject.name}\nX:{pos.x}cm Y:{pos.y}cm z:{pos.z}cm\nFOV:{projector.fieldOfView}");
    }
#endif
    #endregion

    #region PUBLIC_METHODS
    #endregion

    #region PRIVATE_METHODS
    #endregion
}