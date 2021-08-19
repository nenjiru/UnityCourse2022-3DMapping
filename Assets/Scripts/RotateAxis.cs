using UnityEngine;

public class RotateAxis : MonoBehaviour
{
    [SerializeField]
    Vector3 _axis = new Vector3(0f, 0f, 0f);

    void Update()
    {
        transform.Rotate(_axis);
    }
}
