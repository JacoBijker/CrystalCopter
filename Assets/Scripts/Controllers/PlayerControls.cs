using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject burningAnimation;

    private float xSpeed = 1f;
    public float xStartingSpeed = 1f;

    const float maxVelocity = 4;
    const float maxSpeed = 4;
    const float velocitySpeed = 1f;

    const float maxRotation = 0.25f;
    const float rotationSpeed = 0.01f;
    // Use this for initialization
    private int dmg;

    private float damageGracePeriod;

    private bool Active = true;
    private GameObject burningObject;

    void Start()
    {
        Active = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            return;

        damageGracePeriod -= Time.deltaTime;
        Vector2 currentVelocity = GetComponent<Rigidbody2D>().velocity;
        Quaternion currentRotation = transform.rotation;
        if (Input.GetButton("Fire1"))
        {
            currentVelocity.y += velocitySpeed;
            currentRotation.z += rotationSpeed;
        }
        else
        {
            currentRotation.z -= rotationSpeed;
        }

        currentVelocity.x = xSpeed;
        currentRotation.z = Mathf.Clamp(currentRotation.z, 0, maxRotation);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -xSpeed, xSpeed);

        transform.rotation = currentRotation;
        GetComponent<Rigidbody2D>().velocity = currentVelocity;
    }

    public void Restart()
    {
        dmg = 1;
        xSpeed = xStartingSpeed;
        if (burningObject != null)
            DestroyImmediate(burningObject);

        this.transform.position = Vector3.zero;
        this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.transform.localRotation = Quaternion.identity;

        this.GetComponent<Rigidbody2D>().fixedAngle = true;
        Active = true;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Crystal")
        {
            GameManager.SendMessage("Score");
            
            Destroy(collisionInfo.gameObject);
            xSpeed = Mathf.Clamp(xSpeed + 0.1f, xStartingSpeed, maxSpeed);
        }
        else if (Active && damageGracePeriod <= 0)
        {
            dmg++;
            GameManager.SendMessage("HealthUpdate", dmg);
            //Handheld.Vibrate();
            if (dmg > 3)
            {
                this.GetComponent<Rigidbody2D>().fixedAngle = false;
                Active = false;

                // burningObject = Instantiate(burningAnimation, Vector3.zero, Quaternion.identity) as GameObject;
                // burningObject.transform.parent = this.transform;
                // burningObject.transform.localPosition = Vector3.zero;
                GameManager.SendMessage("Crashed");                
            }
            else
                damageGracePeriod = 1;
        }
        else if (burningObject != null)
        {
            burningObject.transform.rotation = Quaternion.identity;
        }
    }
}
