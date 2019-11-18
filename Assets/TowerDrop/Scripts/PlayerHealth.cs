using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float Health = 100.0f;

    public bool hit;

    public float playerHP
    {
        get
        {
            return Health;
        }
        set
        {
            Health = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && hit == false)
        {
            hit = true;
        }
    }

}
