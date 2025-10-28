using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AutoAxisFromAnchorRotation : MonoBehaviour
{
    [System.Serializable]
    public class JointAxisData
    {
        public ArticulationBody joint;
        public Vector3 computedAxis;
    }

    public List<JointAxisData> joints = new List<JointAxisData>();

    [ContextMenu("Detect Axes From Anchor Rotation")]
    public void DetectAxes()
    {
        foreach (var data in joints)
        {
            if (data.joint == null) continue;

            // Axa default folosită de X Drive
            Vector3 localX = Vector3.right;

            // Calculăm axa reală de rotație în spațiul global
            Vector3 worldAxis = data.joint.transform.TransformDirection(localX);

            // Proiectăm în spațiu local
            data.computedAxis = data.joint.transform.InverseTransformDirection(worldAxis).normalized;

            Debug.Log($"{data.joint.name} → Axa locală de rotație detectată: {data.computedAxis}");
        }
    }

    [ContextMenu("Copie în Clipboard ca listă de Vector3")]
    public void CopyAsVector3List()
    {
        string output = "[\n";
        foreach (var d in joints)
        {
            Vector3 a = d.computedAxis;
            output += $"    new Vector3({a.x:F2}f, {a.y:F2}f, {a.z:F2}f),\n";
        }
        output += "]";
        GUIUtility.systemCopyBuffer = output;
        Debug.Log("Lista a fost copiată în clipboard ✂️");
    }
}
