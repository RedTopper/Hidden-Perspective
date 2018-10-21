using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Body configuration
    public float offset = 0.5f;
    public float dist = -0.1f;

    //Player configuration
    public float speed = 0.2f;
    public float sensitivityMouse = 10f;
    public float sensitivityJoy = 10f;
    public float maxYAngle = 80f;

    //Enemy config
    public Enemy enemyPrefab;

    private GameObject obs;
    private GameObject body;
    private GameObject cam;
    private GameObject handl;
    private GameObject handr;
    private Vector2 currentRotation;
    private bool VREnabled = false;
    private bool collecting = false;
    private bool dying = false;
    private int collected = 0;
    private int health = 2;


    void Start ()
    {
        body = GameObject.Find("Player/Body");
        cam = GameObject.Find("Player/Cam");
        obs = GameObject.Find("Observer/Cam");
        handl = GameObject.Find("Player/HandLeft");
        handr = GameObject.Find("Player/HandRight");

        if (XRSettings.enabled && XRDevice.isPresent)
        {
            VREnabled = true;
        }
        else
        {
            XRSettings.enabled = false;
            VREnabled = false;
            obs.GetComponent<Camera>().rect = new Rect(0.78f, 0f, 0.22f, 1.0f);
        }
    }

    void Update()
    {
        Vector3 dir = new Vector3();
        if (VREnabled)
        {
            //movement
            Vector2 left = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            dir = new Vector3(left.x, 0.0f, left.y).normalized * speed;

            //look
            gameObject.transform.RotateAround(cam.transform.position, Vector3.up, OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x * sensitivityJoy);
        }
        else
        {
            //movement
            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * speed;

            //look
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivityMouse;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivityMouse;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            cam.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //Find yaw
        Vector3 pos = cam.transform.position;
        Vector3 angle = cam.transform.rotation.eulerAngles;
        angle.x = 0;
        angle.z = 0;
        Quaternion yaw = Quaternion.Euler(angle);
        
        //Move the body
        body.transform.position = pos + (yaw * Vector3.forward) * dist - new Vector3(0, offset, 0);

        //Move the player
        gameObject.GetComponent<Rigidbody>().velocity = yaw * dir;

        //Move the hands
        if (VREnabled)
        {
            handl.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            handl.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            handr.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            handr.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        }
        else
        {
            handl.transform.RotateAround(cam.transform.position, Vector3.up, angle.y - handl.transform.rotation.eulerAngles.y);
            handr.transform.RotateAround(cam.transform.position, Vector3.up, angle.y - handr.transform.rotation.eulerAngles.y);
        }

        //Check if the player has won
        Maze maze = GameObject.Find("Maze Runner").GetComponent<Maze>();
        if (collected > 0 && maze.GetObjectiveCount() == collected)
        {
            SceneManager.LoadScene("Win");
        }

        //reset collecting
        collecting = false;
        dying = false;
    }

    public bool IsVREnabled()
    {
        return VREnabled;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!collecting && col.gameObject.name == "Objective")
        {
            collecting = true;
            Destroy(col.gameObject);
            collected++;

            //spawn enemy
            Enemy enemy = Instantiate(enemyPrefab) as Enemy;
            enemy.name = "Enemy";
            enemy.transform.position = new Vector3(1, 0.21f, 1);
        }

        if (!dying && col.gameObject.name == "Enemy")
        {
            dying = true;

            health--;

            Debug.Log(health);

            if (health <= 0)
            {
                SceneManager.LoadScene("Lose");
            }

            //add bloody screen

            Destroy(col.gameObject);
        }
    }
}
