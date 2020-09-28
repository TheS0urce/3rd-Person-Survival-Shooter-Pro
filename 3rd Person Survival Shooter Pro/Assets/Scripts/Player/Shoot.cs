using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodSpatterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Firing();
    }

    void Firing()
    {
        //left click fire
        //cast ray from centre of screen
        //debug the name of the object hit
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 centreViewPort = new Vector3(.5f, .5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(centreViewPort);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 0 | 1 << 7))
            {
                if (hitInfo.collider != null)
                {
                    Debug.Log("Object hit: " + hitInfo.collider.name);
                    Health health = hitInfo.collider.GetComponent<Health>();

                    if(health != null)
                    {
                        //instantiate blood effect
                        //position the raycast hit
                        //rotate towards hitnormal position (surface normal)
                        Instantiate(_bloodSpatterPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        health.Damage(25);
                    }
                }
            }
        }
    }
}
