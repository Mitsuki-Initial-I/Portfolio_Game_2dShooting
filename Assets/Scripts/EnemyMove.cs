using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    public enum MoveType
    {
        Straight,
        Zigzag,
        Homing,
        Circle,
        Wave
    }

    [Header("移動タイプ設定")]
    public MoveType moveType = MoveType.Straight;

    [Header("共通設定")]
    public float speed = 2f;

    [Header("Zigzag / Wave 設定")]
    public float amplitude = 1f;   // 揺れ幅
    public float frequency = 3f;   // 揺れの速さ

    [Header("Homing 設定")]
    public string playerTag = "Player";  // プレイヤー識別用
    private Transform player;

    [Header("Circle 設定")]
    public Transform center;       // 回転中心
    public float radius = 2f;
    public float rotateSpeed = 2f;
    private float angle = 0f;

    // 内部管理用
    private Vector3 startPos;
    private float startY;

    void Start()
    {
        startPos = transform.position;
        startY = transform.position.y;

        if (moveType == MoveType.Homing)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        switch (moveType)
        {
            case MoveType.Straight:
                MoveStraight();
                break;
            case MoveType.Zigzag:
                MoveZigzag();
                break;
            case MoveType.Homing:
                MoveHoming();
                break;
            case MoveType.Circle:
                MoveCircle();
                break;
            case MoveType.Wave:
                MoveWave();
                break;
        }
    }

    // ------------------- 各移動処理 -------------------

    void MoveStraight()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
    }

    void MoveZigzag()
    {
        float y = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y, 0);
    }

    void MoveHoming()
    {
        if (player == null) return;
        Vector2 dir = (player.position - transform.position).normalized;
        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    void MoveCircle()
    {
        if (center == null) return;
        angle += rotateSpeed * Time.deltaTime;
        float x = center.position.x + Mathf.Cos(angle) * radius;
        float y = center.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, 0);
    }

    void MoveWave()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + Vector3.left * speed * Time.time + Vector3.up * yOffset;
    }
}
