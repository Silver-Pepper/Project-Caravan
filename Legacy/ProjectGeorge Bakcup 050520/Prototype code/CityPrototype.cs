using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGeorge.Controllers;
using ProjectGeorge.Entities;

public class CityPrototype : MonoBehaviour
{
    public float money;
    public int slaves;
    public string flavourName;
    public string flavourText;
    public Merchant merchantC;
    public GameObject merchant;

    // Start is called before the first frame update
    void Start()
    {
        merchantC = merchant.GetComponent<Merchant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
