using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public GameObject target;

    void LateUpdate()
    {
        transform.LookAt(target.transform);
    }

}
