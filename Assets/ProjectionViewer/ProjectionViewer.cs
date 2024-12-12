//Source : https://gist.github.com/runevision/07e846fed321d4785d10332a733d0844

using UnityEngine;

// Visual effect/illusion where a rotating model alternates between
// seeming to rotate one way around and the other.
// Unlike many similar effects, the eyes are nudged/forced to see it
// in alternating ways by oscillating between regular perspective and
// inverse perspective, changing which parts of the model appear
// bigger or smaller on the screen.

// Attach this script on the GameObject with the Camera component.
// The camera should be one unit away from the model.
// E.g. if model is at the origin, the camera should be at 0, 0, -1.
// Adjust the scale of the model to fit within this view.
// The camera should use perspective projection.
// You can use whatever field of view you want, but note that the
// Effect Strength parameter can also be used to tweak the amount
// of perspective.
// The model should use a shader without backface culling.
// Unlit recommended too.
public class ProjectionViewer : MonoBehaviour
{
    public Transform model;
    public float effectStrength = 1f;
    public float cycleDuration = 5f;
    public float revolutions = 1f;
    public float rotationOffset = 0f;

    Camera cam;
    Matrix4x4 proj;

    void Start()
    {
        cam = GetComponent<Camera>();
        proj = cam.projectionMatrix;
    }

    void Update()
    {
        // Manipulate camera projection matrix.
        Matrix4x4 curProj = proj;
        float cos = Mathf.Cos(Time.time * Mathf.PI * 2f / cycleDuration) * effectStrength;
        curProj.m23 = Mathf.LerpUnclamped(-1f, -0.6f, cos);
        curProj.m32 = Mathf.LerpUnclamped(0f, -1f, cos);
        curProj.m33 = Mathf.LerpUnclamped(1f, 0f, cos);
        cam.projectionMatrix = curProj;

        // Rotate model.
        float angle = rotationOffset + Time.time * 360 * revolutions / cycleDuration;
        model.eulerAngles = new Vector3(0, angle, 0f);
    }

    void OnPreRender()
    {
        GL.wireframe = true;
    }

    void OnPostRender()
    {
        GL.wireframe = false;
    }
}