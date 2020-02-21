using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ref : MonoBehaviour
{
   public static Ref Instance { get; private set; }

    //public Material[] materials;
    public TileInfo[] tileInfo;
    public Text popupText;

    private List<Popup> popupBuffer;

    public void showText(string message, int delay)
    {
        Popup buf = new Popup();
        buf.message = message;
        buf.delay = delay;
        popupBuffer.Add(buf);
        if (popupBuffer.Count == 1)
        {
            StartCoroutine(showPopup(message, delay));
        }
            
    }

    IEnumerator showPopup(string message, int delay)
    {
        Debug.Log(" Popup null " + popupText == null);
        popupText.enabled = true;
        popupText.text = message;
        yield return new WaitForSeconds(delay);
        popupText.enabled = false;
        popupBuffer.RemoveAt(0);

        if(popupBuffer.Count > 0)
        {
            StartCoroutine(showPopup(popupBuffer[0].message, popupBuffer[0].delay));
        }
    }

    class Popup
    {
        public string message;
        public int delay;
    }

    private void Awake()
    {
        Instance = this;
        popupBuffer = new List<Popup>();
    }
}
