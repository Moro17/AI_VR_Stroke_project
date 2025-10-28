using UnityEngine;

public class WorkspaceTestMover : MonoBehaviour
{
    public Transform workspaceRoot;

    [ContextMenu("Move Workspace Test")]
    public void MoveTest()
    {
        if (workspaceRoot == null)
        {
            Debug.LogError("WorkspaceRoot is NULL");
            return;
        }

        workspaceRoot.position += new Vector3(2f, 0f, 0f); // Mută-l 2m pe X
        Debug.Log("Workspace moved 2m on X");
    }
}
