using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    float moveSpeed = 5;
    float movepos_x = 0;
    float movepos_y = 0;
    float time_Count = 0f;
    float bulletSpan = 5;

    int mode_x = 0;
    int mode_y = 0;

    Camera cam;
    Vector3 worldTopRigh;
    Vector3 worldBottomLeft;

    void GetCamraScale()
    {
        float depth = cam.transform.position.z * -1;
        Vector3 bottomLeft = new Vector3(0, 0, depth);
        worldBottomLeft = cam.ScreenToWorldPoint(bottomLeft);

        Vector3 topRight = new Vector3(Screen.width, Screen.height, depth);
        worldTopRigh = cam.ScreenToWorldPoint(topRight);
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        GetCamraScale();
        time_Count = bulletSpan;
    }
    void Update()
    {
        GetCamraScale();
        time_Count += Time.deltaTime;
        time_Count = Mathf.Clamp(time_Count, 0, bulletSpan);
        
        if (Input.GetKey(KeyCode.D)) { mode_x = 1; }
        else if (Input.GetKey(KeyCode.A)) { mode_x = -1; }
        else { mode_x = 0; }
        if (Input.GetKey(KeyCode.W)) { mode_y = 1; }
        else if (Input.GetKey(KeyCode.S)) { mode_y = -1; }
        else { mode_y = 0; }

        movepos_x = transform.position.x + moveSpeed * mode_x * Time.deltaTime;
        movepos_y = transform.position.y + moveSpeed * mode_y * Time.deltaTime;
        movepos_x = Mathf.Clamp(movepos_x, worldBottomLeft.x, worldTopRigh.x);
        movepos_y = Mathf.Clamp(movepos_y, worldBottomLeft.y, worldTopRigh.y);
        transform.position = new Vector3(movepos_x, movepos_y, 0);

        if(Input.GetKey(KeyCode.Space))
        {
            if (time_Count >= bulletSpan)
            {
                time_Count = 0;
                Instantiate(bullet,transform.position+new Vector3(0,-1.5f,0),Quaternion.identity);
            }
        }
    }
}