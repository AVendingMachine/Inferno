using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LayerMask groundMask;
    public bool grappling = false;
    public Transform player;
    public Transform hook;
    public Transform sphook;
    private float grappleTime = 0;
    public float unhookDistance = 2;
    public LineRenderer lineRenderer;
    public Transform cam;
    private void OnEnable()
    {
        Application.onBeforeRender += UpdateLinePosition;
    }
    private void OnDisable()
    {
        Application.onBeforeRender -= UpdateLinePosition;
    }
    private void UpdateLinePosition()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, sphook.position);
    }
    private void LateUpdate()
    {
        UpdateLinePosition();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.position, transform.forward, out hit, Mathf.Infinity, groundMask))
            {
                grappling = true;
                Debug.Log("grappling");
                hook.position = hit.point;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            grappling = false;
        }
        if (Vector3.Distance(player.position,hook.position) < unhookDistance)
        {
            grappling = false;
        }

        if (grappling)
        {
            sphook.position = Vector3.Lerp(transform.position, hook.position, 200*grappleTime);
            player.position = Vector3.Lerp(player.position, hook.position, grappleTime);
            grappleTime = Mathf.Clamp(grappleTime + 0.5f * (Time.deltaTime / Vector3.Distance(player.position, hook.position)), 0, 1);
            player.GetComponent<PlayerMovement>().velocity.y = 0;
            player.GetComponent<PlayerMovement>().enabled = false;
            lineRenderer.enabled = true;
        }
        if (!grappling)
        {
           // sphook.position = transform.position;
            grappleTime = 0;
            player.GetComponent<PlayerMovement>().enabled = true;
            lineRenderer.enabled = false;
        }

    }
}
