using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject placedPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager raycaster;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycaster = GetComponent<ARRaycastManager>();
    }

    void OnPlaceObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }
}
