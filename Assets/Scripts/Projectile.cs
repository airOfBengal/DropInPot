using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public GameObject lineVisualGO;
    public int lineSegment = 10;
    public float flightTime = 1f;

    private Camera cam;

    private Touch touch;
    private Vector3 startTouchPos;
    private Vector3 movedTouchPos;
    private float touchChangedZ;
    private bool touchEnded = false;

    public float zDelta = 10;
    public float xDelta = 5;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineVisual.positionCount = lineSegment + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, GameManager.instance.ball.transform.position.z + 1);
                    lineVisualGO.SetActive(true);
                    startTouchPos = cam.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Moved:
                    movedTouchPos = cam.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    lineVisualGO.SetActive(false);
                    touchEnded = true;
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }

            touchChangedZ = Mathf.Abs(movedTouchPos.z - startTouchPos.z);
            float moveCursorX = startTouchPos.x - movedTouchPos.x;
            cursor.transform.position = new Vector3(moveCursorX, cursor.transform.position.y, (GameManager.instance.ball.transform.position.z + 1) + touchChangedZ * zDelta);
            LaunchProjectile();
        } 
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(cam.WorldToScreenPoint(cursor.transform.position));
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit))
        {
            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);

            if (hit.collider.CompareTag("Tile"))
            {
                cursor.transform.position = new Vector3(cursor.transform.position.x, hit.point.y, cursor.transform.position.z);
            }

            Visualize(vo, cursor.transform.position); //we include the cursor position as the final nodes for the line visual position

            transform.rotation = Quaternion.LookRotation(vo);

            if (touchEnded)
            {
                Rigidbody obj = GameManager.instance.ball.GetComponent<Rigidbody>(); //Instantiate(projectile, shootPoint.position, Quaternion.identity);
                obj.useGravity = true;
                obj.velocity = vo;
            }
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    //added final position argument to draw the last line node to the actual target
    void Visualize(Vector3 vo, Vector3 finalPos)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, (i / (float)lineSegment) * flightTime);
            lineVisual.SetPosition(i, pos);
        }

        lineVisual.SetPosition(lineSegment, finalPos);
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
}