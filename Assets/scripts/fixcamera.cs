using UnityEngine;

public class DisableCinemachineCam : MonoBehaviour
{
    public GameObject cineCamObj;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cineCamObj.SetActive(false);
            
            
        }
    }

}
