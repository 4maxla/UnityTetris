using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{

    // reference laukuma
    Board m_gameBoard;

    // reference spawneram
    Spawner m_spawner;

    // pašreiz aktīvais objekts
    Shape m_activeShape;

    ScoreManager m_scoreManager;

    public float m_dropInterval = 0.25f;

    float m_dropIntervalModded;

    float m_timeToDrop;

    float m_timeToNextKeyLeftRight;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRateLeftRight = 0.25f;

    float m_timeToNextKeyDown;

    [Range(0.01f, 0.5f)]
    public float m_keyRepeatRateDown = 0.01f;

    float m_timeToNextKeyRotate;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRateRotate = 0.25f;

    public GameObject m_gameOverPanel;

    bool m_gameOver = false;



    // Use this for initialization
    void Start()
    {

        // Atrod Spawneri un Board objektus pēc tā taga, un referencē tos.
        m_gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        m_spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        m_scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();

        m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;
        m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
        m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

        if (!m_gameBoard)
        {
            Debug.LogWarning("WARNING!  There is no game board defined!");
        }

        if (!m_spawner)
        {
            Debug.LogWarning("WARNING!  There is no spawner defined!");
        }

        if (!m_scoreManager)
        {
            Debug.LogWarning("WARNING!  There is no ScoreManager!");
        }
        else
        {
            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position); // Noapaļo pozicījas kordinātes uz veseliem skaitļiem.

            if (!m_activeShape) // Ja nav aktīvā figūra, tad izsauksies SpawnShape metode un uzspawnos jaunu figūru.
            {
                m_activeShape = m_spawner.SpawnShape();
            }
        }

        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_spawner || !m_gameBoard || !m_activeShape || m_gameOver || !m_scoreManager) // Ja netiek atrasts Spawner, gameBoard, vai m_activeShape, tad neko neuzsākt.
        {
            return;
        }
        PlayerInput();
    }

    void PlayerInput()  // Kontrolieris
    {

        if (Input.GetButton("MoveRight") && (Time.time > m_timeToNextKeyLeftRight) || Input.GetButtonDown("MoveRight"))
        {
            m_activeShape.MoveRight();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
            }

        }
        else if (Input.GetButton("MoveLeft") && (Time.time > m_timeToNextKeyLeftRight) || Input.GetButtonDown("MoveLeft"))
        {
            m_activeShape.MoveLeft();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
            }

        }
        else if (Input.GetButtonDown("Rotate") && (Time.time > m_timeToNextKeyRotate))
        {
            m_activeShape.RotateRight();
            m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.RotateLeft();
            }

        }
        else if (Input.GetButton("MoveDown") && (Time.time > m_timeToNextKeyDown) || (Time.time > m_timeToDrop)) // Ja laiks būs lelāks par norādīto vērtību, tad...
        {
            m_timeToDrop = Time.time + m_dropInterval;
            m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown; // Šis ļaus figūrai krist ik pēc noteikta laika

            m_activeShape.MoveDown();

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                if (m_gameBoard.IsOverLimit(m_activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();
                }
            }

        }
    }

    // Figūras piezemēšanās
    void LandShape()
    {
        // virza figūru augšup un novirza to laukuma masīvā
        m_activeShape.MoveUp();
        m_gameBoard.StoreShapeInGrid(m_activeShape);

        // Spawnot nākamo figūru
        m_activeShape = m_spawner.SpawnShape();

        // Pievienots visiem timeToNextKey mainīgajiem pašreizējā laika vērtības, lai nebūtu aizkaves pie nākamās figūras, kas spawnotos
        m_timeToNextKeyLeftRight = Time.time;
        m_timeToNextKeyDown = Time.time;
        m_timeToNextKeyRotate = Time.time;

        // Nodzēst pabeigtās rindas, no laukuma, ja viņas tur ir.
        m_gameBoard.ClearAllRows();

        if (m_gameBoard.m_completedRows > 0)
        {
            m_scoreManager.ScoreLines(m_gameBoard.m_completedRows);

            if (m_scoreManager.didLevelUp)
            {
                m_dropIntervalModded = Mathf.Clamp(m_dropInterval - ((float)m_scoreManager.m_level * 0.05f), 0.05f, 1f);
            }
        }
    }

    // aktivizējas, kad figūras pārsniedz vertikālo limitu laukumam.
    void GameOver()
    {
        // virzīt figūru vienu lauku augšup
        m_activeShape.MoveUp();

        // Ieslēgt GameOver paneli
        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(true);
        }

        // pārslēdz Game Over stāvokli uz True
        m_gameOver = true;
    }

    // restartē līmeni
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
