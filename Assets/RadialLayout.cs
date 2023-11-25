using UnityEngine;

public class RadialLayout : MonoBehaviour
{
    public float radius;

    void Start()
    {
        //transform.position = new Vector3(-6.5f, 0.5f, 0f);
        ArrangeObjectsRadially();
    }

    void ArrangeObjectsRadially()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            float angle = i * (360f / childCount);
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float z = Mathf.Sin(radians) * radius;

            Vector3 newPosition = new Vector3(x, 0.1f, z);

            Transform child = transform.GetChild(i);
            child.position = newPosition;

            // Optionally, you can also rotate the objects to face the center
            //child.LookAt(transform.position);
        }
        
    }
}
