using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private float messageTime;
    public static MessageDisplay instance;
    private static TMPro.TMP_Text messageText;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        messageText = transform.GetComponent<TMPro.TMP_Text>();    
    }
    public static void DisplayMessage(string text){
        if(text != ""){
        messageText.text = text;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.MessageFade());
        }
    }
    private IEnumerator MessageFade(){
        yield return new WaitForSeconds(messageTime);
        messageText.text = "";
    }
}
