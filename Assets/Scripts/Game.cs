using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Rocket;

    public Rocket GetRocket()
    {
        //GameObject gameObject = Instantiate
        return FindFirstObjectByType(typeof(Rocket)) as Rocket;
    }
}
