using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    
    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 1f;
    public float maxZoom = 5f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Count == 0) { return; }
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = new Vector3(centerPoint.x + offset.x, centerPoint.y + offset.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 10f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform targ in targets)
        {
            bounds.Encapsulate(targ.position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform targ in targets)
        {
            bounds.Encapsulate(targ.position);
        }

        return bounds.size.y > bounds.size.x ? bounds.size.y : bounds.size.x;
    }
}
