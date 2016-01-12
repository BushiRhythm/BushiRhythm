using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour {

    public float m_updateInterval = 1.0f;  // 更新される頻度
    float m_accumulated   = 0.0f;
    float m_timeUntilNextInterval; //  次の更新までの残り時間
    int m_numFrames = 0;

    Text _text;


    Text text
    {
        get
        {
            if (!_text)
                _text = GetComponent<Text>();
            return _text;
        }

    }


    void Start()
    {
    m_timeUntilNextInterval = m_updateInterval;
    }

    void Update()
    {
        m_timeUntilNextInterval -= Time.deltaTime;
        m_accumulated += Time.timeScale / Time.deltaTime;
        ++m_numFrames;

            if( m_timeUntilNextInterval <= 0.0 )
            {
            // FPSの計算と表示
            float fps = m_accumulated / m_numFrames;
            string format = System.String.Format( "FPS: {0:F2}", fps );
            text.text = format;
 
            m_timeUntilNextInterval = m_updateInterval;
            m_accumulated = 0.0F;
            m_numFrames = 0;
            }
    }

}
