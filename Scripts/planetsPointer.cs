
using UnityEngine;
using UnityEngine.EventSystems;

public class planetsPointer : MonoBehaviour
{
    public GameObject placeToDrop_GO;
    //public GameObject blackedout;

    private bool isMoving;
    private bool isCorrectPlace = false;
    private bool inPlace = false;


    private Vector3 screenPoint;
    private Vector3 startPos;
    
    private Vector3 dropPlacePos;




    private void Start()
    {
        dropPlacePos = placeToDrop_GO.GetComponent<Transform>().position;
    }
    
    private void Update()
    {
        if(isMoving == true && inPlace == false)
        {
            Debug.Log("Dragging");
            GetComponent<Renderer>().material.color = Color.blue;


            Vector3 curScreenPointer = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPointer);
            this.transform.position = curPosition;

        }
       

    }

    public void OnPointerDown()
    {
        Debug.Log("PointerIsDown");

        //GetComponent<Renderer>().material.color = Color.green;
        startPos = this.gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        isMoving = true;

    }

    public void OnPointerUp()
    {
        //GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("PointerIsUp");

        isMoving = false;

        if(isCorrectPlace == true)
        {
           this.gameObject.transform.position = dropPlacePos;
            inPlace = true;
        }
        else
        {
            this.gameObject.transform.position = startPos;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Black")
            isCorrectPlace = true;
    }
}
