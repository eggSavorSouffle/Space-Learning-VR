using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class fbHandler : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public Text statusText;
    private static Firebase.Auth.FirebaseAuth auth;
    public static Firebase.Auth.FirebaseUser user;

    protected string email = "";
    protected string password = "";
    protected string displayName = "";

    void Start()
    {
        InitializeFirebase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignIn()
    {
        logout();
        getInputs();
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            user = task.Result;           
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.Email);
        });
    }

    public void CreateUserWithEmail()
    {
        getInputs();
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });
    }

    void getInputs()
    {
        email = emailField.text.ToString();
        password = passwordField.text.ToString();
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {           
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                statusText.text = "Current User: ";
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                email = user.Email ?? "";
                Debug.Log(user.Email);
                statusText.text = "Current User: " + user.Email;
            }
        }
    }

    public void logout()
    {
        auth.SignOut();
    }


    //Needed for android devices
    void CheckGooglePlayServices()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }


}
