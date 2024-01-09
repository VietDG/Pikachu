using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchEffect : MonoBehaviour
{
    [Header("Succeed")]
    public GameObject matchLineSegmentPrefab;

    public bool spawnStar = true;

    public GameObject starPrefab;

    public TopPanelView topPanelView;

    public float fadeTime = 0.25f;

    [Header("Fail")]
    public AnimationCurve shakeCurve = AnimationCurve.Linear(0f, 1f, 0f, 1f);

    public float cameraShakeDuration = 0.25f;

    public float cameraShakeMagnitude = 0.1f;

    private Coroutine cameraShakeCoroutine;

    private Camera _cam;

    private Vector3 cameraOriginPosition;

    private List<GameObject> lineMatchObjectPool = new List<GameObject>();

    // private Vector3 _starCollectPos;

    private List<GameObject> _starPoolObj = new List<GameObject>();

    private int _matchStarCreatedCount = 0;

    private float _starMoveDuration = 0.8f;

    private float _starMoveDelay = 0.25f;

    private SpriteRenderer[] hintMatchLines;

    [SerializeField] Transform _starsTrans, _lineTrans;

    private void Awake()
    {
        TileMatchManager.TileMatchSucceededEvent += OnTileMatchSucceeded;//tìm kiếm thành công 2 ô cùng id
        TileMatchManager.TileMatchFailedEvent += OnTileMatchFailed;// tìm kiếm không thành công 2 ô không cùng id

        _cam = Camera.main;

        for (int i = 0; i < 15; i++)
        {
            SpawnStarObject();// spamw sao 15 
        }

        for (int i = 0; i < 6; i++)
        {
            SpawnLineMatchObject();//spamw 6 đường thẳng
        }
    }

    private void OnDestroy()
    {
        TileMatchManager.TileMatchSucceededEvent -= OnTileMatchSucceeded;
        TileMatchManager.TileMatchFailedEvent -= OnTileMatchFailed;
    }

    private void OnTileMatchSucceeded(Match match)
    {
        CreateMatchLine(match);

        if (spawnStar)
            CreateStarts(match);
    }

    private void OnTileMatchFailed(ItemTile tile1, ItemTile tile2)
    {
        Debug.LogError("khong an duoc");
        if (cameraShakeCoroutine != null)
        {
            StopCoroutine(cameraShakeCoroutine);
            cameraShakeCoroutine = null;
        }

        cameraOriginPosition = _cam.transform.localPosition;
        cameraShakeCoroutine = StartCoroutine(PlayCameraShake());// camera rung
    }

    public SpriteRenderer[] CreateMatchLine(Match match, bool autoFadeOut = true)
    {
        Vector3[] positions = new Vector3[match.locations.Count];//
        SpriteRenderer[] spriteRenderers = new SpriteRenderer[match.locations.Count - 1];

        for (int i = 0; i < positions.Length; i++)
        {
            Vector2Int location = match.locations[i];
            positions[i] = GameManager.Instance.GetPosition(location.x, location.y);
        }

        for (int i = 1; i < positions.Length; i++)
        {
            Vector3 pos1 = positions[i - 1];
            Vector3 pos2 = positions[i];

            SpriteRenderer spriteRenderer = GetLineMatchObject().GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            spriteRenderers[i - 1] = spriteRenderer;
            spriteRenderer.transform.localScale = Vector3.one;

            if (autoFadeOut)
                spriteRenderer.DOFade(0f, fadeTime).OnComplete(() => spriteRenderer.gameObject.SetActive(false)).SetDelay(0.2f);

            if (Mathf.Abs(pos1.x - pos2.x) < 0.01f)
            {
                float length = Mathf.Abs(pos1.y - pos2.y);
                spriteRenderer.transform.localScale = new Vector3(0.18f, length / spriteRenderer.bounds.size.y, 1f);
            }
            else if (Mathf.Abs(pos1.y - pos2.y) < 0.01f)
            {
                float length = Mathf.Abs(pos1.x - pos2.x);
                spriteRenderer.transform.localScale = new Vector3(length / spriteRenderer.bounds.size.x, 0.18f, 1f);
            }

            spriteRenderer.transform.localPosition = (pos1 + pos2) * 0.5f;
        }

        return spriteRenderers;
    }// vẽ hình ảnh từ vị trí i đến vị trí thứ i-1;

    public void CreateHintMatchLine(Match match, bool autoFadeOut)// gợi ý 
    {
        hintMatchLines = CreateMatchLine(match, autoFadeOut);

        StopAllCoroutines();
        StartCoroutine(WaitToClearHintMatchLine());
    }

    private IEnumerator WaitToClearHintMatchLine()
    {
        yield return null;

        while (true)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                if (hintMatchLines != null && hintMatchLines.Length > 0)
                {
                    for (int i = 0; i < hintMatchLines.Length; i++)
                    {
                        hintMatchLines[i].gameObject.SetActive(false);
                    }

                    hintMatchLines = null;
                }

                break;
            }

            yield return null;
        }
    }

    private IEnumerator PlayCameraShake()
    {
        float time = cameraShakeDuration;
        while (time > 0f)
        {
            Vector3 change = Random.insideUnitCircle * cameraShakeMagnitude;
            _cam.transform.localPosition = cameraOriginPosition + change;
            time -= Time.deltaTime;

            yield return null;
        }

        time = 0f;
        _cam.transform.localPosition = cameraOriginPosition;
        cameraShakeCoroutine = null;
    }

    private void CreateStarts(Match match)
    {
        _matchStarCreatedCount = 0;
        //_starCollectPos = topPanelView.GetCollectStarPosition();

        for (int i = 1; i < match.locations.Count; i++)
        {
            Vector2Int locationA = match.locations[i - 1];
            Vector2Int locationB = match.locations[i];

            if (locationA.x == locationB.x)
            {
                int step = (locationA.y < locationB.y) ? 1 : -1;

                for (int y = locationA.y; y != locationB.y; y += step)
                {
                    CreateStarAtLocation(locationA.x, y);
                }
            }
            else if (locationA.y == locationB.y)
            {
                int step = (locationA.x < locationB.x) ? 1 : -1;

                for (int x = locationA.x; x != locationB.x; x += step)
                {
                    CreateStarAtLocation(x, locationA.y);
                }
            }
        }

        Vector2Int lastLocation = match.locations[match.locations.Count - 1];
        CreateStarAtLocation(lastLocation.x, lastLocation.y);

        UserData.current.userStatus.starCount += _matchStarCreatedCount;
        topPanelView.OnStarsCollected(_matchStarCreatedCount, _starMoveDuration + _starMoveDelay);
    }

    private void CreateStarAtLocation(int x, int y)// tạo vị trí ngôi sao
    {
        Vector3 position = GameManager.Instance.GetPosition(x, y);
        var starTransform = GetStarObject().transform;
        starTransform.localPosition = position;

        _matchStarCreatedCount++;

        starTransform.DOKill();
        starTransform.DOMove(topPanelView.starObjects[0].transform.position, _starMoveDuration).
            SetDelay(_starMoveDelay).
            SetEase(Ease.InOutQuad).
            OnComplete(() => starTransform.gameObject.SetActive(false));
    }

    private GameObject GetStarObject()
    {
        for (int i = 0; i < _starPoolObj.Count; i++)
        {
            if (_starPoolObj[i].activeSelf == false)
            {
                _starPoolObj[i].SetActive(true);
                return _starPoolObj[i];
            }
        }

        return SpawnStarObject();
        // active star;
    }

    private GameObject SpawnStarObject()
    {
        GameObject go = Instantiate(starPrefab, _starsTrans);
        go.SetActive(false);
        _starPoolObj.Add(go);

        return go;
        // spam sao 
    }

    private GameObject GetLineMatchObject()
    {

        for (int i = 0; i < lineMatchObjectPool.Count; i++)
        {
            if (lineMatchObjectPool[i].activeSelf == false)
            {
                lineMatchObjectPool[i].SetActive(true);
                return lineMatchObjectPool[i];
            }
        }

        return SpawnLineMatchObject();
    }

    private GameObject SpawnLineMatchObject()
    {
        GameObject go = Instantiate(matchLineSegmentPrefab, _lineTrans);
        go.SetActive(false);
        lineMatchObjectPool.Add(go);

        return go;
    }
}
