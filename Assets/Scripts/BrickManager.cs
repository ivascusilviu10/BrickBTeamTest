using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [Header("Pattern Settings")]
    [SerializeField, Range(0f, 1f)] private float skipChance = 0.2f; // 0.2 = 20% chance to skip brick

    [Header("Brick Settings")]
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 10;
    [SerializeField] private float xSpacing = 1.1f;
    [SerializeField] private float ySpacing = 0.6f;
    [SerializeField] private Vector3 startPosition = new Vector3(-5f, 4f, 0f);

    [Header("Custom Pattern Settings")]
    [SerializeField] private bool useCustomPattern = false;
    [SerializeField] private bool[,] brickPattern;

    private void Start()
    {
        if (useCustomPattern && brickPattern != null && brickPattern.Length > 0)
        {
            SpawnCustomPattern();
        }
        else
        {
            SpawnFullGrid();
        }
    }

    private void SpawnFullGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (Random.value < skipChance)
                {
                    continue; // skip this brick
                }

                Vector3 position = startPosition + new Vector3(col * xSpacing, -row * ySpacing, 0f);
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.transform.parent = this.transform;

                Brick brickScript = brick.GetComponent<Brick>();
                if (brickScript != null)
                {
                    brickScript.SetRowIndex(row);
                }
            }
        }
    }

    private void SpawnCustomPattern()
    {
        int patternRows = brickPattern.GetLength(0);
        int patternCols = brickPattern.GetLength(1);

        for (int row = 0; row < patternRows; row++)
        {
            for (int col = 0; col < patternCols; col++)
            {
                if (brickPattern[row, col])
                {
                    SpawnBrickAt(row, col);
                }
            }
        }
    }

    private void SpawnBrickAt(int row, int col)
    {
        Vector3 position = startPosition + new Vector3(col * xSpacing, -row * ySpacing, 0f);
        GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
        brick.transform.parent = this.transform;

        Brick brickScript = brick.GetComponent<Brick>();
        if (brickScript != null)
        {
            brickScript.SetRowIndex(row);
        }
    }
}