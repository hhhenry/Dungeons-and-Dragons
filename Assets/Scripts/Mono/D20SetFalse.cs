using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20SetFalse : MonoBehaviour
{
    public GameObject d20;
    public void D20False() {
        d20.SetActive(false);
    }
}
