using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public int ItemNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (ItemNum)
            {
                case 1:
                    GameManager.Instance.Items(1);
                    break;
                case 2:
                    GameManager.Instance.Items(2);
                    break;
                case 3:
                    GameManager.Instance.Items(3);
                    break;
                case 4:
                    GameManager.Instance.Items(4);
                    break;
                case 5:
                    GameManager.Instance.Items(5);
                    break;
                case 6:
                    GameManager.Instance.Items(6);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
