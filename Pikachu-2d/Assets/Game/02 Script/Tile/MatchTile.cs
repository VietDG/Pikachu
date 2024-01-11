using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTile : MonoBehaviour
{
    [Header("------------REFERENCE-----------")]
    [SerializeField] GameObject _line;

    public bool isSpawnStars = true;

    [SerializeField] GameObject _starPref;

    [SerializeField] UiGamePLayManager uiGamePlayManager;

    [SerializeField] float _durationFade = 0.25f;

    [Header("------------VALUE-----------")]

    public float _camVibrateDuration = 0.25f;
    public float _camVibrateLevel = 0.1f;

    private Coroutine _camCro;

    private Camera _cam;

    private Vector3 _camOriginalPos;

    private List<GameObject> _lineList = new List<GameObject>();

    private List<GameObject> _starList = new List<GameObject>();

    private int _matchStarCount = 0;

    private float _moveDuration = 0.8f;

    private float _moveDelay = 0.25f;

    private SpriteRenderer[] _lineHint;

    [SerializeField] Transform _starsTrans, _lineTrans;

    private void Awake()
    {
        EventAction.OnMatchTile += OnMatchTile;
        EventAction.OnMatchTileFail += OnMatchTileFail;

        _cam = Camera.main;

        for (int i = 0; i < 15; i++)
        {
            SpawnStarPrefab();
        }

        for (int i = 0; i < 6; i++)
        {
            SpawnLine();
        }
    }

    private void OnDestroy()
    {
        EventAction.OnMatchTile -= OnMatchTile;
        EventAction.OnMatchTileFail -= OnMatchTileFail;
    }

    private void OnMatchTile(MatchT matchTile)
    {
        CreateLine(matchTile);

        if (isSpawnStars)
            CreateStarts(matchTile);
    }

    private void OnMatchTileFail(ItemTile item1, ItemTile item2)
    {
        Debug.LogError("khong an duoc");
        if (_camCro != null)
        {
            StopCoroutine(_camCro);
            _camCro = null;
        }
        _camOriginalPos = _cam.transform.localPosition;
        _camCro = StartCoroutine(VibrateCam());
    }

    public SpriteRenderer[] CreateLine(MatchT matchTile, bool isFade = true)
    {
        Vector3[] pos = new Vector3[matchTile.posList.Count];
        SpriteRenderer[] sprite = new SpriteRenderer[matchTile.posList.Count - 1];

        for (int i = 0; i < pos.Length; i++)
        {
            Vector2Int localPos = matchTile.posList[i];
            pos[i] = GameManager.Instance.GetPosTile(localPos.x, localPos.y);
        }

        for (int i = 1; i < pos.Length; i++)
        {
            Vector3 pos1 = pos[i - 1];
            Vector3 pos2 = pos[i];

            SpriteRenderer spriteLine = GetLine().GetComponent<SpriteRenderer>();
            spriteLine.color = Color.white;
            sprite[i - 1] = spriteLine;
            spriteLine.transform.localScale = Vector3.one;

            if (isFade)
                spriteLine.DOFade(0f, _durationFade).OnComplete(() => spriteLine.gameObject.SetActive(false)).SetDelay(0.2f);

            if (Mathf.Abs(pos1.x - pos2.x) < 0.01f)
            {
                float lineLenght = Mathf.Abs(pos1.y - pos2.y);
                spriteLine.transform.localScale = new Vector3(0.18f, lineLenght / spriteLine.bounds.size.y, 1f);
            }
            else if (Mathf.Abs(pos1.y - pos2.y) < 0.01f)
            {
                float length = Mathf.Abs(pos1.x - pos2.x);
                spriteLine.transform.localScale = new Vector3(length / spriteLine.bounds.size.x, 0.18f, 1f);
            }

            spriteLine.transform.localPosition = (pos1 + pos2) * 0.5f;
        }

        return sprite;
    }

    public void CreateMatchLine(MatchT match, bool isFade)
    {
        _lineHint = CreateLine(match, isFade);

        StopAllCoroutines();
        StartCoroutine(WaitClearLine());
    }

    private IEnumerator WaitClearLine()
    {
        yield return null;

        while (true)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                if (_lineHint != null && _lineHint.Length > 0)
                {
                    for (int i = 0; i < _lineHint.Length; i++)
                    {
                        _lineHint[i].gameObject.SetActive(false);
                    }

                    _lineHint = null;
                }
                break;
            }

            yield return null;
        }
    }

    private IEnumerator VibrateCam()
    {
        float timer = _camVibrateDuration;
        while (timer > 0f)
        {
            Vector3 changePos = Random.insideUnitCircle * _camVibrateLevel;
            _cam.transform.localPosition = _camOriginalPos + changePos;
            timer -= Time.deltaTime;

            yield return null;
        }

        timer = 0f;
        _cam.transform.localPosition = _camOriginalPos;
        _camCro = null;
    }

    private void CreateStarts(MatchT matchTile)
    {
        _matchStarCount = 0;

        for (int i = 1; i < matchTile.posList.Count; i++)
        {
            Vector2Int l1 = matchTile.posList[i - 1];
            Vector2Int l2 = matchTile.posList[i];

            if (l1.x == l2.x)
            {
                int pos = (l1.y < l2.y) ? 1 : -1;

                for (int y = l1.y; y != l2.y; y += pos)
                {
                    CreatStarPos(l1.x, y);
                }
            }
            else if (l1.y == l2.y)
            {
                int pos = (l1.x < l2.x) ? 1 : -1;

                for (int x = l1.x; x != l2.x; x += pos)
                {
                    CreatStarPos(x, l1.y);
                }
            }
        }

        Vector2Int finalLocalPos = matchTile.posList[matchTile.posList.Count - 1];
        CreatStarPos(finalLocalPos.x, finalLocalPos.y);

        PlayerData.playerData.userProfile.totalStar += _matchStarCount;
        uiGamePlayManager.StarCollected(_matchStarCount, _moveDuration + _moveDelay);
    }

    //set vi tri star
    private void CreatStarPos(int x, int y)
    {
        Vector3 pos = GameManager.Instance.GetPosTile(x, y);
        var starTrans = GetStars().transform;
        starTrans.localPosition = pos;
        _matchStarCount++;

        starTrans.DOKill();
        starTrans.DOMove(uiGamePlayManager._starObj[0].transform.position, _moveDuration).SetDelay(_moveDelay).SetEase(Ease.InOutQuad).
            OnComplete(() => starTrans.gameObject.SetActive(false));
    }

    private GameObject GetStars()
    {
        for (int i = 0; i < _starList.Count; i++)
        {
            if (_starList[i].activeSelf == false)
            {
                _starList[i].SetActive(true);
                return _starList[i];
            }
        }
        return SpawnStarPrefab();
    }

    private GameObject SpawnStarPrefab()//sinh ra sao
    {
        GameObject g = Instantiate(_starPref, _starsTrans);
        g.SetActive(false);
        _starList.Add(g);

        return g;
    }

    private GameObject GetLine()
    {
        for (int i = 0; i < _lineList.Count; i++)
        {
            if (_lineList[i].activeSelf == false)
            {
                _lineList[i].SetActive(true);
                return _lineList[i];
            }
        }

        return SpawnLine();
    }

    private GameObject SpawnLine()
    {
        GameObject g = Instantiate(_line, _lineTrans);
        g.SetActive(false);
        _lineList.Add(g);

        return g;
    }
}
