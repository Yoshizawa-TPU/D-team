using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool toStage = false;
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] float displayImageDroup = 1f;
    [SerializeField] CanvasGroup canvasGroup;

    float m_Timer;

    [SerializeField] Canvas canvas;

    public sunnyGrow sunnyGrow;
    [SerializeField] rainGrow rainGrow;
    [SerializeField] thunderGrow thunderGrow;
    [SerializeField] FieldPlacer fieldplacer;
    [SerializeField] Score Score;
    [SerializeField] Monster Monster;
    [SerializeField] ScoreManager SCMG;

    public GameObject[] seedBody;
    public GameObject[] sunSeeds;
    public GameObject[] rainSeeds;
    public GameObject[] thunderSeeds;
    int[] count;
    public float Position = 0;
    bool setPosition;

    private bool pause;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pausePanel;
    private int scoreCount = 0;
    private bool codeCheck = true;
    static public bool PAUSE = false;
    Vector3[] spawnPosition = new Vector3[5];

    public float maxScore;
    public bool controlButton;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        setPosition = true;
        count = new int[seedBody.Length];
        //seedBody = new GameObject[seedBody.Length];
        pause = false;
        controlButton = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P))
        {
            EndButton();
        }
        if(Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("メニュー");
        }

        if(setPosition)
        {
            setSeedPosition();
        }

        //収穫(成長)のボタン操作
        if ((controlButton && Input.GetKeyUp(KeyCode.L))||(controlButton && Input.GetButtonDown("Decision")))
        {
            controlButton = false;
            StartCoroutine(handleHervest());
        }
        

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(pause == false)
            {
                pauseGame();
            }
            else
            {
                resumeGame();
            }
        }

        if (toStage)
        {
            FadeOut();
        }
        
        if (codeCheck)
        {
            inScore(scoreCount);
        }
    }

    private IEnumerator handleHervest()
    {
        for (int i = 0; i < seedBody.Length; i++)
        {
            if (seedBody[i] != null)
            { 
                Sunny(i);
                Rain(i);
                Thunder(i);
            }

        }
        Monster.pushL = true;
        yield return new WaitForSeconds(0.45f);//次の開店までの待ち時間
        controlButton = true;
    }
    public void Sunny(int i)
    {
        sunnyGrow script = seedBody[i].GetComponent<sunnyGrow>();
        if (script != null)
        {
            if (script.S_Ready == true)
            {
                if (seedBody[i].tag == "Sunny")
                {
                    Destroy(seedBody[i]);
                    count[i]++;
                    if (count[i] >= 2)
                    {
                        count[i] = 2;
                        script.S_Harvest = true;
                    }
                    else
                    {
                        script.S_Harvest = false;
                    }

                    spawnPosition[i].y = Position;
                    seedBody[i] = Instantiate(sunSeeds[count[i]], spawnPosition[i], Quaternion.identity);

                    if(script.S_Harvest == true)
                    {
                        StartCoroutine(Harvest(i));
                    }
                }
            }
            else if(script.S_Return == true)
            {
                Destroy(seedBody[i]);
                count[i]--;
                if (count[i] <= 0)
                {
                    count[i] = 0;
                }
                spawnPosition[i].y = Position;
                seedBody[i] = Instantiate(sunSeeds[count[i]], spawnPosition[i], Quaternion.identity);
            }
        }
    }

    public void Rain(int i)
    {
        rainGrow script = seedBody[i].GetComponent<rainGrow>();
        if (script != null)
        {
            if (script.R_Ready == true)
            {
                if (seedBody[i].tag == "Rainy")
                {
                    Destroy(seedBody[i]);
                    count[i]++;

                    if (count[i] >= 2)
                    {
                        count[i] = 2;
                        script.R_Harvest = true;
                    }
                    else
                    {
                        script.R_Harvest = false;
                    }

                    spawnPosition[i].y = Position;
                    seedBody[i] = Instantiate(rainSeeds[count[i]], spawnPosition[i], Quaternion.identity);

                    if (script.R_Harvest == true)
                    {
                        StartCoroutine(Harvest(i));
                    }
                }
            }
            else if(script.R_Return == true)
            {
                Destroy(seedBody[i]);
                count[i]--;
                if (count[i] <= 0)
                {
                    count[i] = 0;
                }
                spawnPosition[i].y = Position;
                seedBody[i] = Instantiate(rainSeeds[count[i]], spawnPosition[i], Quaternion.identity);
            }
        }
    }

    public void Thunder(int i)
    {
        thunderGrow script = seedBody[i].GetComponent<thunderGrow>();
        if (script != null)
        {
            if (script.T_Ready == true)
            {
                if (seedBody[i].tag == "Thunder")
                {
                    Destroy(seedBody[i]);
                    count[i] += 2; ;

                    if (count[i] >= 2)
                    {
                        count[i] = 2;
                        script.T_Harvest = true;
                    }
                    else
                    {
                        script.T_Harvest = false;
                    }

                    spawnPosition[i].y = Position;
                    seedBody[i] = Instantiate(thunderSeeds[count[i]], spawnPosition[i], Quaternion.identity);

                    if (script.T_Harvest == true)
                    {
                        StartCoroutine(Harvest(i));
                    }
                }
            }
        }
    }


    private IEnumerator Harvest(int i)
    {
        var animator = GetComponent<Animator>();
        yield return new WaitForSeconds(1.0f);
        fieldplacer.SeedsJudge(i);
        setPosition = true;
        scoreCount++;
    }

    public void setSeedPosition()
    {
        for (int i = 0; i < seedBody.Length; i++)
        {
            //Debug.Log("入れる前" + spawnPosition[i]);
            count[i] = 0;
            if (seedBody[i] != null)//スタート時点ではseedがnull
            {
                spawnPosition[i] = seedBody[i].transform.position;
                //Debug.Log("入れたあと" + spawnPosition[i]);
                setPosition = false;
            }
        }
    }

    public void inScore(int i)
    {
        codeCheck = false;
        scoreCount = 0;
        switch (i)
        {
            case 0:
                Score.score += SCMG.zeroPickPoint;
                break;
            case 1:
                Score.score += SCMG.onePickPoint;
                break;
            case 2:
                Score.score += SCMG.twoPickPoint;
                break;
            case 3:
                Score.score += SCMG.threePickPoint;
                break;
            case 4:
                Score.score += SCMG.fourPickPoint;
                break;
            case 5:
                Score.score += SCMG.fivePickPoint;
                break;
        }
        codeCheck = true;
        //Debug.Log("最終スコアは" + Score.score);
        PlayerPrefs.SetFloat("resultScore", Score.score);
    }

    public void HuntingScore()
    {
        Debug.Log("害獣を倒した。");
        Score.score += SCMG.HuntingPoint;
    }

    public void StartButton()
    {
        toStage = true;
    }

    public void TitlleButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("titlle");//移動先のシーン名を入力
    }

    public void EndButton()
    {
        SceneManager.LoadScene("end");//移動先のシーン名を入力
    }

    public void ExitButton()//ゲーム自体の終了
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // エディタの再生モードを停止
#else
            Application.Quit();  // ビルド環境でアプリケーションを終了
#endif
    }

    public void pauseGame()
    {
        Debug.Log("ポーズ");
        PAUSE = true;
        pauseUI.SetActive(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        pause = true;
    }

    public void resumeGame()
    {
        Debug.Log("ポーズ解除");
        PAUSE = false;
        pauseUI.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
        pause = false;
    }

    public void FadeOut()
    {
        Debug.Log("作動");
        m_Timer += Time.deltaTime;

        canvasGroup.alpha = m_Timer / fadeDuration;

        //Debug.Log(m_Timer);
        //Debug.Log(fadeDuration + displayImageDroup);

        if(m_Timer > fadeDuration+displayImageDroup)
        {
            Debug.Log("完了");
            SceneManager.LoadScene("mainGame");
        }
    }
}
