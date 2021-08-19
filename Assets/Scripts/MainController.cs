using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MainController : MonoBehaviour
{
    public enum Mode
    {
        Calibration,
        Test,
        Projection
    }

    [Header("プロジェクターの解像度")] public Vector2 resolution = new Vector2(1920, 1080);
    [Header("投影面の大きさと距離（cm）")] public Vector3 projection = new Vector3(192f, 108f, 100f);
    [HideInInspector] public float fov = 30f;
    [Header("モード")] public Mode mode = Mode.Calibration;
    [Space]
    public Projector projector;
    public Projector viewPointProjector;
    public Camera renderCamera;
    public Camera virtualCamera;
    public Transform projectionSize;
    public Transform cubeModel;
    public Transform physicalCube;
    public Transform virtualCube;
    Mode _mode;

    void OnValidate()
    {
        Calc();
    }

    void Calc()
    {
        if (mode == Mode.Calibration)
        {
            fov = 2.0f * Mathf.Atan(projection.y * 0.5f / Mathf.Abs(projection.z)) * Mathf.Rad2Deg;
            projectionSize.localScale = new Vector2(projection.x, projection.y);
            var pos = projector.transform.position;
            pos.z = -projection.z;
            projector.transform.position = pos;
        }

        var aspect = resolution.x / resolution.y;
        projector.aspectRatio = aspect;
        projector.fieldOfView = fov;

        viewPointProjector.fieldOfView = fov;
        viewPointProjector.aspectRatio = aspect;

        renderCamera.fieldOfView = fov;
        virtualCamera.fieldOfView = fov;
    }

    void Update()
    {
        viewPointProjector.transform.LookAt(cubeModel);
        virtualCamera.transform.localPosition = viewPointProjector.transform.position;
        virtualCamera.transform.localRotation = viewPointProjector.transform.rotation;
        virtualCube.localPosition = physicalCube.position;
        virtualCube.localRotation = physicalCube.rotation;

        if (mode != _mode)
        {
            switch (mode)
            {
                case Mode.Calibration: calibrationMode(); break;
                case Mode.Test: testMode(); break;
                case Mode.Projection: projectionMode(); break;
            }
            _mode = mode;
        }
    }

    /*
    #if UNITY_EDITOR
        [CustomEditor(typeof(MainController))]
        [CanEditMultipleObjects]
        public class MainControllerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                MainController main = target as MainController;
                GUILayout.Space(10);
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Calc")) main.Calc();
                    GUILayout.Label("FOV " + main.fov);
                }
            }
        }
    #endif
    */

    void calibrationMode()
    {
        projector.enabled = true;
        projectionSize.gameObject.SetActive(true);
        cubeModel.gameObject.SetActive(true);
        physicalCube.gameObject.SetActive(false);
        virtualCube.parent.gameObject.SetActive(false);
        viewPointProjector.transform.parent.gameObject.SetActive(false);
    }

    void testMode()
    {
        projector.enabled = false;
        projectionSize.gameObject.SetActive(false);
        cubeModel.gameObject.SetActive(true);
        physicalCube.gameObject.SetActive(true);
        virtualCube.parent.gameObject.SetActive(false);
        viewPointProjector.transform.parent.gameObject.SetActive(false);
    }

    void projectionMode()
    {
        projector.enabled = false;
        projectionSize.gameObject.SetActive(false);
        cubeModel.gameObject.SetActive(true);
        physicalCube.gameObject.SetActive(true);
        virtualCube.parent.gameObject.SetActive(true);
        viewPointProjector.transform.parent.gameObject.SetActive(true);
    }
}
