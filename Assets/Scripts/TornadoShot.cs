using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoShot : MonoBehaviour
{
    public float cnt;
    public float a;
    public GameObject Emmo;
    public Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tornadoShot());
    }
    IEnumerator tornadoShot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(Emmo);
            bullet.transform.position = pos.position;
            bullet.transform.rotation = pos.rotation;
            pos.Rotate(Vector3.up * a);
            yield return new WaitForSeconds(cnt);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
