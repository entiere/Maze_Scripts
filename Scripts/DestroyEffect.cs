using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public GameObject CardsNew;
    public GameObject DeactivateObject;
    // Start is called before the first frame update
    public void StartDestroy()
    {
        CardsNew.SetActive(true);
        Destroy(gameObject, 1);
    }
    public void StartDeactivete()
    {
        DeactivateObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
