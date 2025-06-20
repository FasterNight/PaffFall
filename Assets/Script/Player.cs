using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;         // Vitesse de suivi
    public float clampX = 5f;
    public float clampZ = 5f;
    public float fixedY = 0f;             // Hauteur constante du joueur

    private Camera mainCamera;
    private Rigidbody rb;

    void Start()
    {
        mainCamera = Camera.main;

        // Ajoute automatiquement un Rigidbody s’il n’existe pas
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        FollowMousePosition();
        LockHeight();
    }

    void FollowMousePosition()
    {
        Vector3 mouseWorldPos = GetMouseWorldPos();

        // Clamp la position cible
        mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, -clampX, clampX);
        mouseWorldPos.z = Mathf.Clamp(mouseWorldPos.z, -clampZ, clampZ);
        mouseWorldPos.y = fixedY; // Toujours au même Y

        // Mouvement lissé
        Vector3 targetPosition = Vector3.Lerp(transform.position, mouseWorldPos, moveSpeed * Time.deltaTime);
        rb.MovePosition(new Vector3(targetPosition.x, fixedY, targetPosition.z));
    }

    void LockHeight()
    {
        // Assure que Y reste constant en cas d’impact ou glitch
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, fixedY, pos.z);
    }

    Vector3 GetMouseWorldPos()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, fixedY, 0f));
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }
}
