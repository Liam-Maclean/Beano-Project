using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// Used for fixing known bugs in a temp gameObject
public class BugFixScript : MonoBehaviour
{
    private static bool m_created = false;

    private Canvas m_bugCanvas;

    void Awake()
    {
        if (!m_created)
        {
            DontDestroyOnLoad(this.gameObject);
            m_created = true;
        }

        m_bugCanvas = GetComponent<Canvas>();

        /// Use this to call bug fixer anywhere in game
        //GameObject bugFixer;
        //bugFixer = GameObject.FindGameObjectWithTag("BugFixer");
    }

    void Update()
    {
        if (!m_bugCanvas.enabled)
        {
            m_bugCanvas.enabled = true;
        }
    }

    public void SetWorldCanvas()
    {
        m_bugCanvas.renderMode = RenderMode.WorldSpace;
    }

    public void SetMenuCanvas()
    {
        m_bugCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
}
