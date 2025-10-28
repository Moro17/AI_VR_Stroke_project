using UnityEngine;

public class ColaSequenceManager : MonoBehaviour
{
    public GameObject[] colaObjects;
    public Transform rightHand;
    private int currentIndex = 0;
    public float touchDistance = 0.2f;

    void Start()
    {
        for (int i = 0; i < colaObjects.Length; i++)
            colaObjects[i].SetActive(i == 0); // doar primul activ
    }

    void Update()
    {
        if (currentIndex >= colaObjects.Length) return;

        GameObject currentCola = colaObjects[currentIndex];
        float distance = Vector3.Distance(rightHand.position, currentCola.transform.position);

        if (distance < touchDistance)
        {
            currentCola.SetActive(false);
            currentIndex++;
            if (currentIndex < colaObjects.Length)
                colaObjects[currentIndex].SetActive(true);
        }
    }
}
