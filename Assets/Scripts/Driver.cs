using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{

    bool hasPackage;
    SpriteRenderer spriteRenderer;

    [SerializeField] float slowSpeed;
    [SerializeField] float boostSpeed;
    [SerializeField] Color32 withPackageColor = new Color32();
    [SerializeField] Color32 withoutPackageColor = new Color32();
    [SerializeField] float destroyDelay;
    [SerializeField] float moveSpeed;
    [SerializeField] float steerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed;

        transform.Rotate(0, 0, -steerAmount * Time.deltaTime);
        transform.Translate(0, moveAmount * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COllided");
        moveSpeed = slowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Passenger" && hasPackage)
        {
            Debug.Log("Package has been delivered");
            hasPackage = false;
            spriteRenderer.color = withoutPackageColor;
        }

        if (collision.tag == "Package" && !hasPackage)
        {
            Debug.Log("Package has been picked up");
            hasPackage = true;
            spriteRenderer.color = withPackageColor;
            Destroy(collision.gameObject,destroyDelay);

        }

        if (collision.tag == "Booster")
        {
            moveSpeed = boostSpeed;
        }
    }
}
