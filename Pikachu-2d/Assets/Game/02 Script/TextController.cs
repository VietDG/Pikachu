using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    [SerializeField] TMP_Text _newText;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TMP_Text>().text = _newText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
