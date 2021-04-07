using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CollectibleCube : MonoBehaviour
{

    public UnityAction OnCubeCollected;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlaceholderCube")
        {
            PlaceholderCube placeholderCube = other.gameObject.GetComponent<PlaceholderCube>();
            GetComponent<Renderer>().material.color = placeholderCube.color;
            transform.position = placeholderCube.transform.position;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            placeholderCube.gameObject.SetActive(false);
            OnCubeCollected?.Invoke();
            
        }
    }
}
