using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenadeProjectile;

    public float coolDown = 1f;
    private float currentCoolDown;
    private bool onCoolDown = false;
    public List<GameObject> children;
    private Vector3 startingPos;
    private float viewBobTime = 0f;
    private bool viewBobDirection = true;
    public GameObject playerBody;
    // Start is called before the first frame update
    private void OnEnable()
    {
        onCoolDown = false;
    }
    void Start()
    {
        startingPos = transform.localPosition;
        currentCoolDown = coolDown;
    }
    IEnumerator CoolDownTimer()
    {
        onCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !onCoolDown)
        {
            Instantiate(grenadeProjectile,transform.position, transform.rotation);
            StartCoroutine(CoolDownTimer());
        }
        if (onCoolDown)
        {
            foreach (GameObject child in children)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            foreach (GameObject child in children)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }

        }
        //Viewbob
        if (playerBody.GetComponent<PlayerMovement>().isMoving == false)
        {
            transform.localPosition = startingPos;
        }
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && playerBody.GetComponent<PlayerMovement>().isMoving == true)
        {
            if (viewBobDirection == true)
            {
                transform.localPosition += Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime + Time.deltaTime * 5, 0, 1);
            }
            if (viewBobDirection == false)
            {
                transform.localPosition -= Vector3.up * Time.deltaTime * 0.1f;
                viewBobTime = Mathf.Clamp(viewBobTime - Time.deltaTime * 5, 0, 1);
            }
            if (viewBobTime >= 1)
            {
                viewBobDirection = false;
            }
            if (viewBobTime <= 0)
            {
                viewBobDirection = true;
            }
            Debug.Log(viewBobTime);

        }
        else
        {
            viewBobDirection = true;
            viewBobTime = 0;
        }
    }
}
