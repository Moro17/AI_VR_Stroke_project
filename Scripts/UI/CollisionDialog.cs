////using MixedReality.Toolkit.UX;
//using UnityEngine;

//public class CollisionDialog : MonoBehaviour
//{
//    public DialogPool dialogPool;

//    public CollisionManager collisionManager;

//    /// <summary>
//    /// A Unity event function that is called when an enabled script instance is being loaded.
//    /// </summary>
//    protected virtual void Awake()
//    {
//        if (dialogPool == null)
//        {
//            dialogPool = GetComponent<DialogPool>();
//        }

//        if (collisionManager == null)
//        {
//            collisionManager = GetComponent<CollisionManager>();
//        }
//    }

//    public void SpawnDialogFromCode()
//    {
//        IDialog dialog = dialogPool.Get()
//            .SetHeader("Collision detect!")
//            .SetBody("All of the dialog's properties can be set from code, using a friendly API.")
//            .SetPositive("Yes, please!", (args) => Debug.Log("Code-driven dialog says " + args.ButtonType))
//            .SetNegative("No, thanks.", (args) => Debug.Log("Code-driven dialog says " + args.ButtonType));

//        dialog.Show();
//    }

//    public void SpawnNeutralDialogFromCode()
//    {
//        IDialog dialog = dialogPool.Get()
//            .SetHeader("Collision detected!")
//            .SetBody("Press \"OK!\" after the robot has reached a collision-free pose!")
//            .SetNeutral("OK!", (args) => collisionManager.isCollisionEnabled = true);

//        dialog.Show();
//    }

//    public void SpawnAllThreeDialogFromCode()
//    {
//        IDialog dialog = dialogPool.Get()
//            .SetHeader("You can even have three!")
//            .SetBody("Yes, in fact, you can request all three option types and they'll still be laid out correctly.")
//            .SetPositive("Yes, please!", (args) => Debug.Log("Code-driven dialog says " + args.ButtonType))
//            .SetNegative("No, thanks.", (args) => Debug.Log("Code-driven dialog says " + args.ButtonType))
//            .SetNeutral("Neutral option!", (args) => Debug.Log("Code-driven dialog says " + args.ButtonType));

//        dialog.Show();
//    }

//    public void SpawnDialogWithAsync()
//    {
//        ShowAsyncDialog();
//    }

//    private async void ShowAsyncDialog()
//    {
//        DialogDismissedEventArgs result = await dialogPool.Get()
//            .SetHeader("This dialog is spawned from an async method.")
//            .SetBody("The async method that spawned this dialog will await for the dialog's result.")
//            .SetPositive("Yes, please!")
//            .SetNegative("No, thanks.")
//            .ShowAsync();

//        Debug.Log("Async dialog says " + result.Choice?.ButtonText);
//    }
//}
