using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColaInteraction : MonoBehaviour
{
    [Tooltip("Trage aici în Inspector obiectul care are CocaColaSwitcher")]
    public CocaColaSwitcher switcher;

    private bool _canInteract = false;

    private IEnumerator Start()
    {
        // aşteaptă un pic până trece OVRHand prin scenă
        yield return new WaitForSeconds(4f);
        _canInteract = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canInteract) return;

        if (other.CompareTag("Hand"))
        {
            switcher.ActivateNext(gameObject);
        }
    }

    public void HandleHoverEnter(HoverEnterEventArgs args)
    {
        switcher.ActivateNext(gameObject);
    }

    

    //public void TriggerManually()
    //{
    //    Debug.Log("Hovered! Activăm următorul");
    //    switcher.ActivateNext(gameObject);
    //}
}
