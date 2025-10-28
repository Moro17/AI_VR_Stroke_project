//using MixedReality.Toolkit.UX;
//using System.Collections;
//using UnityEngine;

//public class TestCom : MonoBehaviour
//{
//    public Slider slider;
//    public DigitalTwinControl control;

//    // Start is called before the first frame update
//    void Start()
//    {
//        StartCoroutine(CallRepeatedly());
//    }

//    IEnumerator CallRepeatedly()
//    {
//        yield return new WaitForSeconds(10f);
//        while (true)
//        {
//            RepeatedFunction();
//            yield return new WaitForSeconds(2.5f);
//        }
//    }

//    void RepeatedFunction()
//    {
//        slider.Value += 5f;
//        control.PublishCurrentPosition();
//    }
//}
