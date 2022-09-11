using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float moveSpeed = 200f;

    private Rigidbody rb;

    private Vector3 randomRotation;

    private float removePositionZ;

    public Material targetMaterial;

    private Material baseMaterial;

    public Renderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        randomRotation = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        removePositionZ = Camera.main.transform.position.z;

        renderers = GetComponentsInChildren<Renderer>();
        baseMaterial = renderers[0].material;

    }

    public void ResetMaterial()
    {
        if(renderers == null)
        {
            return;
        }

        foreach(Renderer rend in renderers)
        {
            rend.material = baseMaterial;
        }

    }

    public void SetTargetMaterial()
    {
        if (renderers == null)
        {
            return;
        }

        foreach (Renderer rend in renderers)
        {
            rend.material = targetMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < removePositionZ)
        {
            Destroy(gameObject);
        }

        Vector3 movementVector = new Vector3(0f, 0f, -moveSpeed * Time.deltaTime);
        rb.velocity = movementVector;

        transform.Rotate(randomRotation * Time.deltaTime);
    }

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().OnAsteroidImpact();
            DestroyAsteroid();
        }
    }
}
