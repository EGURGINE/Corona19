using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update

    void Start()
    {
            //Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Plane GroupPlan = new Plane(Vector3.up, Vector3.zero);
            //float rayLengh;
            //if (GroupPlan.Raycast(cameraRay, out rayLengh)) { }
            //{
            //    Vector3 pointTolook = cameraRay.GetPoint(rayLengh);
            //    Debug.Log("X =" + pointTolook.x + "Z =" + pointTolook.z);
            //    transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            //}

    }

    // Update is called once per frame
    void Update()
    {   
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        BulletDie();
    }
        float cnt;
    void BulletDie() 
    {
        cnt += Time.deltaTime;
        if (cnt>3)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                //GameManager.Instance.IsEmmoIdx--;
                Destroy(gameObject);
                break;
            case "Wall":
                //GameManager.Instance.IsEmmoIdx--;
                Destroy(gameObject);
                break;
        }
    }
}
