using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class SingularityCore : MonoBehaviour
{
    void Awake(){
        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.velocity = Vector3.zero;
    }
}
