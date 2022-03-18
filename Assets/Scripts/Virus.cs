using UnityEngine;

public class Virus : Enemy
{
    protected override void Attack()
    {
        float bulletCnt = 10;

        firePos.LookAt(GameObject.Find("Player").transform.position);

        Quaternion firstRotation = firePos.rotation;

        for (int i = 0; i <= bulletCnt; i++)
        {
            Bullet bullet = Instantiate(bulletObj);
            bullet.transform.position = firePos.position;
            bullet.transform.rotation = firePos.rotation;
            firePos.Rotate(Vector3.up * 5);
        }

        firePos.rotation = firstRotation;
    }
}
