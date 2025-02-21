using UnityEngine;

public class CullObjects : MonoBehaviour
{
    private Transform player;
    public float despawnDistance = 30f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null && transform.position.z < player.position.z - despawnDistance)
        {
            gameObject.SetActive(false);
        }
    }
}
