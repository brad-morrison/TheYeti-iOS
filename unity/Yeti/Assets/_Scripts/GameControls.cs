using UnityEngine;

public class GameControls : TheYeti
{
    private void Update()
    {
#if UNITY_EDITOR
        HandleDebugKeyboardInput();
#endif
        HandlePointerInput();
    }

#if UNITY_EDITOR
    private void HandleDebugKeyboardInput()
    {
        if (!GM.gameManager.allowInput)
            return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.gameManager.HandleInput(PunchSide.Left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.gameManager.HandleInput(PunchSide.Right);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GM.gameManager.FallingBones(true);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            GM.gameManager.FallingBones(false);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!GM.gameManager.goldMode.goldMode)
            {
                GM.gameManager.ActivateGoldMode();
            }
            else
            {
                GM.gameManager.DeactivateGoldMode();
            }
        }

        // flush high score
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("reset scores");
            GM.playerData.SetHighScore(0);
            GM.playerData.SetKills(0);
            GM.gameManager.highScore = 0;
            GM.gameManager.totalKills_counter = 0;
        }

        // for debug - add 100 to high score
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("added 100 to high score | now - " + GM.gameManager.highScore);
            int data = GM.playerData.GetHighScore();
            GM.gameManager.highScore = GM.gameManager.highScore + 100;
            GM.playerData.SetHighScore(data + 100);
        }

        // for debug - add 100 to kills
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("added 100 to kills | now - " + GM.gameManager.totalKills_counter);
            int data = GM.playerData.GetKills();
            GM.gameManager.totalKills = GM.gameManager.totalKills_counter + 100;
            GM.playerData.SetKills(data + 100);
        }

        // for debug - start frenzy mode
        if (Input.GetKeyDown(KeyCode.F))
        {
            GM.gameManager.frenzyMode.StartFrenzyTransition();
        }
    }
#endif

    private void HandlePointerInput()
    {
        if (!Input.GetMouseButtonUp(0) || !GM.gameManager.allowInput)
            return;

        Vector3 touch = Input.mousePosition;

        if (touch.y >= GM.gameManager.deviceScreenHeight / 2)
            return;

        PunchSide side = touch.x < GM.gameManager.deviceScreenWidth / 2
            ? PunchSide.Left
            : PunchSide.Right;

        GM.gameManager.HandleInput(side);
    }
}
