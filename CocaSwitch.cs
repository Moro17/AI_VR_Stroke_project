using UnityEngine;

public class CocaColaSwitcher : MonoBehaviour
{
    public GameObject coca1;
    public GameObject coca2;
    public GameObject coca3;
    public GameObject coca4;

    void Start()
    {
        // Pornim doar cu prima activă
        coca1.SetActive(true);
        coca2.SetActive(false);
        coca3.SetActive(false);
        coca4.SetActive(false);
    }

    public void ActivateNext(GameObject current)
    {
        Debug.Log("Activare: " + current.name);

        if (current == coca1)
        {
            coca1.SetActive(false);
            coca2.SetActive(true);
        }
        else if (current == coca2)
        {
            coca2.SetActive(false);
            coca3.SetActive(true);
        }
        else if (current == coca3)
        {
            coca3.SetActive(false);
            coca4.SetActive(true);
        }
        else if (current == coca4)
        {
            coca4.SetActive(false);
        }
    }
}
