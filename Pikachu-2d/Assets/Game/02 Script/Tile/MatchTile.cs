using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MatchTile : MonoBehaviour
{
    [Header("------------REFERENCE-----------")]
    [SerializeField] GameObject _line;

    public bool isSpawnStars = true;

    [SerializeField] GameObject _starPref;
    [SerializeField] GameObject _dotPref;

    [SerializeField] UiGamePLayManager uiGamePlayManager;

    //  [SerializeField] float _durationFade = 0.25f;

    [Header("------------VALUE-----------")]

    public float _camVibrateDuration = 0.25f;
    public float _camVibrateLevel = 0.1f;

    private Coroutine _camCro;

    [SerializeField] Camera _cam;

    private Vector3 _camOriginalPos;

    private List<GameObject> _lineList = new List<GameObject>();

    private List<GameObject> _starList = new List<GameObject>();

    private List<GameObject> _dotList = new List<GameObject>();

    private int _matchStarCount = 0;

    private SpriteRenderer[] _lineHint;

    [SerializeField] Transform _starsTrans, _lineTrans, _dotTrans;

    private void Awake()
    {
        EventAction.OnMatchTile += OnMatchTile;
        EventAction.OnMatchTileFail += OnMatchTileFail;

        for (int i = 0; i < 15; i++)
        {
            SpawnStarPrefab();
            SpawnDot();
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
        item1.SetLayerWhite();
        item2.SetLayerWhite();
        if (_camCro != null)
        {
            StopCoroutine(_camCro);
            _camCro = null;
        }
        _camOriginalPos = _cam.transform.localPosition;
        _camCro = StartCoroutine(VibrateCam());
    }

    private Vector2Int Pos;

    public void SetAnim(bool isPlayAnim)
    {
        foreach (var item in GameManager.Instance.itemTileList)
        {
            if (Pos.x == item.index && Pos.y == item.value)
            {
                item.SetAnim(isPlayAnim);
            }
        }
    }

    public void UsingHint(MatchT matchTile)
    {
        Vector3[] pos = new Vector3[matchTile.posList.Count];

        for (int i = 0; i < pos.Length; i++)
        {
            Vector2Int localPos = matchTile.posList[i];
            pos[i] = GameManager.Instance.GetPosTile(localPos.x, localPos.y);
            Pos.x = localPos.x;
            Pos.y = localPos.y;
            SetAnim(true);
        }
        FindTileBooster.isUsing = true;
    }

    public SpriteRenderer[] CreateLine(MatchT matchTile, bool isFade = true, bool isHint = false)
    {
        Vector3[] pos = new Vector3[matchTile.posList.Count];
        SpriteRenderer[] sprite = new SpriteRenderer[matchTile.posList.Count - 1];

        for (int i = 0; i < pos.Length; i++)
        {
            Vector2Int localPos = matchTile.posList[i];
            pos[i] = GameManager.Instance.GetPosTile(localPos.x, localPos.y);
            Pos.x = localPos.x;
            Pos.y = localPos.y;
            //  SetAnim(true);
        }

        foreach (var item in GameManager.Instance.itemTileList)
        {
            item.SetAnim(false);
        }

        FindTileBooster.isUsing = false;

        for (int i = 1; i < pos.Length; i++)
        {
            Vector3 pos1 = pos[i - 1];
            Vector3 pos2 = pos[i];

            SpriteRenderer spriteLine = GetLine().GetComponent<SpriteRenderer>();
            spriteLine.color = Color.white;
            sprite[i - 1] = spriteLine;
            spriteLine.transform.localScale = Vector3.one;

            if (isFade)
                FunctionCommon.DelayTime(0.4f, () =>
                {
                    spriteLine.gameObject.SetActive(false);
                });

            if (Mathf.Abs(pos1.x - pos2.x) < 0.01f)
            {
                float lineLenght = Mathf.Abs(pos1.y - pos2.y);
                spriteLine.transform.localScale = new Vector3(0.18f * 2, ((lineLenght / spriteLine.bounds.size.y) + 0.18f * 2), 1f);
            }
            else if (Mathf.Abs(pos1.y - pos2.y) < 0.01f)
            {
                float length = Mathf.Abs(pos1.x - pos2.x);
                spriteLine.transform.localScale = new Vector3(((length / spriteLine.bounds.size.x) + 0.18f * 2), 0.18f * 2, 1f);
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
                foreach (var item in GameManager.Instance.itemTileList)
                {
                    item.SetAnim(false);
                    ClearDot();
                }
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

    #region Star
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

        PlayerData.Instance.TotalStar += _matchStarCount;
        //    uiGamePlayManager.StarCollected(_matchStarCount, _moveDuration + _moveDelay);
    }

    //set vi tri star
    private void CreatStarPos(int x, int y)
    {
        Vector3 pos = GameManager.Instance.GetPosTile(x, y);
        var starTrans = GetStars().transform;
        starTrans.localPosition = pos;
        _matchStarCount++;

        //starTrans.DOMove(uiGamePlayManager._starObj[0].transform.position, _moveDuration)/*.SetDelay(_moveDelay)*/.SetEase(Ease.Linear).
        //    OnComplete(() => starTrans.gameObject.SetActive(false));

        starTrans.DOKill();
        starTrans.DORotate(new Vector3(0, 0, 90), 0.4f).SetEase(Ease.Linear).SetDelay(0.1f).OnComplete(() =>
        {
            starTrans.gameObject.SetActive(false);
            starTrans.rotation = Quaternion.identity;
        });
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
    #endregion

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

    #region Dot
    public void CreatDot(MatchT matchTile)
    {
        for (int i = 1; i < matchTile.posList.Count; i++)
        {
            Vector2Int l1 = matchTile.posList[i - 1];
            Vector2Int l2 = matchTile.posList[i];

            if (l1.x == l2.x)
            {
                CreateDotPos(l1.x, l1.y);
            }
            else if (l1.y == l2.y)
            {
                CreateDotPos(l1.x, l1.y);
            }
        }

        Vector2Int finalLocalPos = matchTile.posList[matchTile.posList.Count - 1];
        CreateDotPos(finalLocalPos.x, finalLocalPos.y);
    }

    public void ClearDot()
    {
        for (int i = 0; i < _dotList.Count; i++)
        {
            _dotList[i].gameObject.SetActive(false);
        }
    }

    private void CreateDotPos(int x, int y)
    {
        Vector3 pos = GameManager.Instance.GetPosTile(x, y);
        var dotTrans = GetDot().transform;
        dotTrans.localPosition = pos;
    }

    private GameObject GetDot()
    {
        for (int i = 0; i < _dotList.Count; i++)
        {
            if (_dotList[i].activeSelf == false)
            {
                _dotList[i].SetActive(true);
                return _dotList[i];
            }
        }
        return SpawnDot();
    }

    private GameObject SpawnDot()//sinh ra cham
    {
        GameObject g = Instantiate(_dotPref, _dotTrans);
        g.SetActive(false);
        _dotList.Add(g);

        return g;
    }
    #endregion
}
