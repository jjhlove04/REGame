using UnityEngine;

public class GearScript : MonoBehaviour
{
    public GameObject endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(endPos.transform.position.x + 35f, endPos.transform.position.y - 70f, endPos.transform.position.z + 45f);
    }
}
