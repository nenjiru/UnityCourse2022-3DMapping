using UnityEngine;
using UnityEditor;

/// <summary>
/// </summary>
public class PositionGizmo : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 0f, 0f);

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        var pos = transform.position;
        Handles.Label(transform.position + offset, $"{gameObject.name}\nX:{pos.x}cm Y:{pos.y}cm z:{pos.z}cm");
    }
#endif
}