using UnityEngine;

public class backgroundcontroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float parallex;
    public GameObject cam;
    private float startpos;
    void Start()
    {
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * parallex;

        transform.position =new Vector3 (startpos + distance,transform.position.y,transform.position.z);
    }
}
