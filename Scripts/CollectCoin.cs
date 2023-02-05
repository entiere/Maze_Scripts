using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public static int TheCoin;
    public Text TextCoin;
    public KeyCode Money;

    // Start is called before the first frame update
    void Start()
    {
    //    TheCoin = 10;
        TextCoin = GetComponent<Text>();
    }

    // Update is farm coins
    void Update()
    {
        if (Input.GetKeyDown(Money))
        {
            TheCoin += 5;
        }
        TextCoin.text = " " + TheCoin;
    }

    // NewCoin is farm coins in map
    public void NewCoin()
    {
        TheCoin += 10;
    }
}
