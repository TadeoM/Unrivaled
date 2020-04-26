using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridEditor : MonoBehaviour {

    private int counter = 0;
    public GameObject gridThatStoresTheItems;
    public GameObject scrollView;
    public Text itemPrefab;

	// Use this for initialization
	void Start () {
        scrollView.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public Text AddItemToGrid()
    {
        counter++;
        var newText = Instantiate(itemPrefab) as Text;
        newText.text = string.Format("Item {0}", counter.ToString());
        newText.transform.parent = gridThatStoresTheItems.transform;
        return newText;
    }
}
