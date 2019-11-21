using UnityEngine;

public class Tank : MonoBehaviour
{

    public GameObject tankShell;
    public GameObject target;

    public float fireRate;

    private bool isFired;
    private float timePassed;

    void Update()
    {
        if (target == null)
            return;

        if (tankShell == null)
            return;

        timePassed += Time.deltaTime;

        if (timePassed >= fireRate)
        {
            isFired = false;
        }


        if (!isFired)
        {
            Transform turrentPoint = transform.GetChild(1).transform;

            GameObject shell = Instantiate(tankShell, turrentPoint.position, turrentPoint.rotation);
            shell.GetComponent<Shell>().SetTarget(target.transform);
            shell.GetComponent<Shell>().SetShellSpeed(5.0f);


            isFired = true;
            timePassed = 0;
        }

    }
}
