using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UController : MonoBehaviour
{
    public UStory currentStory;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStory(UStory newStory)
    {
        currentStory = newStory;
    }
}
