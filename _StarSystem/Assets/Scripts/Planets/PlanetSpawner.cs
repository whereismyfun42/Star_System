using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public static PlanetSpawner instance;

    public int numberOfPlanets = 500;
    public int maxRadius = 200;
    public GameObject[] Planets;
    public Material[] mats;
    public Material trail;

    void Awake()
    {
        Planets = new GameObject[numberOfPlanets];
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Planets = CreateSpheres(numberOfPlanets, maxRadius); 
    }

    public GameObject[] CreateSpheres(int count, int radius)
    {
        var sphr = new GameObject[count];
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rb = sphereToCopy.AddComponent<Rigidbody>();
        rb.useGravity = false;
        sphereToCopy.AddComponent<Rotate>();
        sphereToCopy.GetComponent<Rotate>().rotateSpeed = 150f;
        sphereToCopy.tag = "Planet";

        for (int i = 0; i<count; i++)
        {
            var sp = GameObject.Instantiate(sphereToCopy);
            sp.transform.position = this.transform.position +
                new Vector3(Random.Range(-maxRadius, maxRadius),
                            Random.Range(-10, 10),
                            Random.Range(-maxRadius, maxRadius));
            sp.transform.localScale *= Random.Range(1f, 2f);
            
            sp.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
            TrailRenderer tr = sp.AddComponent<TrailRenderer>();
            tr.time = 1.0f;
            tr.startWidth = 0.1f;
            tr.endWidth = 0;
            tr.material = trail;
            tr.startColor = new Color(1, 1, 0, 0.1f);
            tr.endColor = new Color(0, 0, 0, 0);
            Planets[i] = sp;

        }
        GameObject.Destroy(sphereToCopy);

        return Planets;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject s in Planets)
        {
            Vector3 difference = this.transform.position - s.transform.position;

            float dist = difference.magnitude;
            Vector3 gravityDirection = difference.normalized;
            float gravity = 6.7f * (this.transform.localScale.x * s.transform.localScale.x * 1) / (dist * dist);
            Vector3 gravityVector = (gravityDirection * gravity);
            s.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
        }
    }
}
