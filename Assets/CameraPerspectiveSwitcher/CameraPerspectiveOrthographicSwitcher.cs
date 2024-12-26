//https://forum.unity.com/threads/smooth-transition-between-perspective-and-orthographic-modes.32765/

using UnityEngine;

public class CameraPerspectiveOrthographicSwitcher : MonoBehaviour
{
    private Matrix4x4 ortho,perspective;
    public float fov = 60f,
                near = .3f,
                far = 1000f,
                orthographicSize = 50f;
    private float aspect;
    private bool orthoOn;

    public MatrixBlender blender;
    public Camera m_camera;

    void Start()
    {
        aspect = (float)Screen.width / (float)Screen.height;
        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        m_camera.projectionMatrix = ortho;
        orthoOn = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orthoOn = !orthoOn;
            if (orthoOn)
                blender.BlendToMatrix(ortho, 1f, 8, true);
            else
                blender.BlendToMatrix(perspective, 1f, 8, false);
        }
    }
}