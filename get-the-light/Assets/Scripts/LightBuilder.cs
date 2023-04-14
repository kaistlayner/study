using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBuilder : MonoBehaviour
{
    [SerializeField]
    private LightSpawner lightSpawner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit raycastHit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
            {
                if (raycastHit.transform.CompareTag("Tile"))
                {
                    lightSpawner.SpawnLight(raycastHit.transform);
                }
            }
        }
    }

}
