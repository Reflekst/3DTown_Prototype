using UnityEngine;

public class MoveScan : MonoBehaviour
{
    [SerializeField] private float rayRange;
    public static bool isWay, isLanding;
    public GameObject Forward, Backward, Player;

    void Update()
    {
        ScanTerrain();
        WaitForGround();
    }
    private void ScanTerrain()
    {
        RaycastHit hitFace;
        RaycastHit hitBack;

        //Forward and Backward
        if (Physics.Raycast(Forward.transform.position, -Forward.transform.up, out hitFace, rayRange) && Physics.Raycast(Backward.transform.position, -Backward.transform.up, out hitBack, rayRange))
            isWay = true;
        else
            isWay = false;

    }
    private void WaitForGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.transform.position, -Player.transform.up, out hit, rayRange))
            isLanding = true;
        else
            isLanding = false;
    }
}
