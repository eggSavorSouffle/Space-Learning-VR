using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;


public class fbDBHandler : MonoBehaviour
{
    public InputField inputData;
    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void writeData()
    {
        if (!string.IsNullOrEmpty(inputData.text))
        {
            //Document(fbHandler.user.UserId)
            //DocumentReference docRef = db.Collection("InputData").AddAsync();

            if (fbHandler.user != null)
            {
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "Input", inputData.text}
                };           

                db.Collection(fbHandler.user.UserId).AddAsync(data).ContinueWithOnMainThread(task => {
                    DocumentReference addedDocRef = task.Result;
                    Debug.Log(string.Format("Added document with ID: {0}.", addedDocRef.Id));
                });
            }
            else
            {
                Debug.LogWarning("Sign In");
            }

        }
    }

    public void getData()
    {
        Query allData = db.Collection(fbHandler.user.UserId);
        allData.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allDataQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allDataQuerySnapshot.Documents)
            {
                Debug.Log(string.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> data = documentSnapshot.ToDictionary();
           
                foreach (KeyValuePair<string, object> pair in data)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
        });
    }
}
