//using MixedReality.Toolkit.UX;
//using UnityEngine;

//public class SliderModifier : MonoBehaviour
//{
//    public Slider slider;
//    public ToggleCollection multiplierToggles;
//    public float baseValue = 1f;

//    private float[] multipliers = new float[] { 0.01f, 0.1f, 1f, 10f, 25f };
//    private float currentMultiplier = 1f;
//    public enum ModificationDirection
//    {
//        Increase,
//        Decrease
//    }

//    public ModificationDirection modificationDirection;

//    // Start is called before the first frame update
//    void Start()
//    {
//        if (slider == null)
//        {
//            Debug.LogError("Slider is not assigned.");
//            return;
//        }

//        if (multiplierToggles == null)
//        {
//            Debug.LogError("MultiplierToggles is not assigned.");
//            return;
//        }

//        // Subscribe to the ToggleCollection selection change event
//        multiplierToggles.OnToggleSelected.AddListener(OnMultiplierChanged);
//    }
//    void OnMultiplierChanged(int index)
//    {
//        // Update the current multiplier based on the selected toggle index
//        if (index >= 0 && index < multipliers.Length)
//        {
//            currentMultiplier = multipliers[index];
//        }
//    }

//    public void ModifySlider()
//    {
//        if (slider == null)
//        {
//            Debug.LogError("Slider is not assigned.");
//            return;
//        }
//        // Determine the modification direction
//        bool increase = modificationDirection == ModificationDirection.Increase;

//        // Calculate the new value for the slider
//        float modificationValue = baseValue * currentMultiplier * (increase ? 1 : -1);
//        float newValue = Mathf.Clamp(slider.Value + modificationValue, slider.MinValue, slider.MaxValue);

//        // Assign the new value to the slider
//        slider.Value = newValue;

//    }
//    public void ApplyModification()
//    {
//        ModifySlider();
//    }
//}
