using UnityEngine;
using System.Collections;

/*
 * 非選択時にもカメラのビュー範囲を表示する
 * https://gist.githubusercontent.com/sharkattack51/5451394/raw/0f9aa0229bf78488e07d47b93a5b418a8fb118fa/CameraGizmoDrawer.cs
 */
[ExecuteInEditMode]
public class CameraGizmo : MonoBehaviour
{
    [SerializeField]
    Color _color = Color.gray;

    [SerializeField]
    bool _isProjector = false;

    Camera _camera;
    Projector _projector;
    float _fov = 0f;
    float _size = 0f;
    float _max = 0f;
    float _min = 0f;
    float _aspect = 0f;

    void OnDrawGizmos()
    {
        if (_camera == null)
        {
            if (_isProjector == false)
            {
                _camera = GetComponent<Camera>();
            }
            else
            {
                _projector = GetComponent<Projector>();
            }
        }

        if (_isProjector == false)
        {
            _fov = _camera.fieldOfView;
            _size = _camera.orthographicSize;
            _max = _camera.farClipPlane;
            _min = _camera.nearClipPlane;
            _aspect = _camera.aspect;
        }
        else
        {
            _fov = _projector.fieldOfView;
            _size = _projector.orthographicSize;
            _max = _projector.farClipPlane;
            _min = _projector.nearClipPlane;
            _aspect = _projector.aspectRatio;
        }

        Color tempColor = Gizmos.color;
        Gizmos.color = _color;
        Matrix4x4 tempMat = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, this.transform.rotation, new Vector3(_aspect, 1.0f, 1.0f));
        Gizmos.DrawFrustum(Vector3.zero, _fov, _max, _min, 1.0f);

        Gizmos.color = tempColor;
        Gizmos.matrix = tempMat;
    }
}