using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardActivate : MonoBehaviour
{
    // Start is called before the first frame update
    public int PriceCard;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CollectCoin.TheCoin - PriceCard < 0){
        GetComponent<Button>().interactable = false;
        }
        else{
        GetComponent<Button>().interactable = true;
        }
        
    }
}
