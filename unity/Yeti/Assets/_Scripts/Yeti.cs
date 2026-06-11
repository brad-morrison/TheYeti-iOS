using UnityEngine;

public enum YetiPose
{
    Both,
    Dead,
    Idle1,
    Idle2
}

public class Yeti : TheYeti {
    
    public float yetiPunchInterval; // 0.1
    public GameObject yeti, yeti_goldOutline;
    public Sprite yetiGold_left, yetiGold_right, yetiGold_bothUp;
    public Costume currentCostume;
    private SpriteRenderer yetiRenderer;
    private SpriteRenderer goldOutlineRenderer;

    private void Awake()
    {
        yetiRenderer = yeti.GetComponent<SpriteRenderer>();
        goldOutlineRenderer = yeti_goldOutline.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        currentCostume = GM.gameManager.costumesList[GM.playerData.GetCostume()];
        yetiRenderer.sprite = currentCostume.both;
        GM.gameManager.yetiCharacter_gameOver.GetComponent<SpriteRenderer>().sprite = currentCostume.both;
    }

    public void SetSprite(PunchSide side)
    {
        switch (side)
        {
            case PunchSide.Left:
                yetiRenderer.sprite = currentCostume.left;
                goldOutlineRenderer.sprite = yetiGold_right;
                break;

            case PunchSide.Right:
                yetiRenderer.sprite = currentCostume.right;
                goldOutlineRenderer.sprite = yetiGold_left;
                break;
        }

        Invoke(nameof(ResetSprite), yetiPunchInterval);
    }

    public void SetSprite(YetiPose pose)
    {
        switch (pose)
        {
            case YetiPose.Both:
                yetiRenderer.sprite = currentCostume.both;
                goldOutlineRenderer.sprite = yetiGold_bothUp;
                break;

            case YetiPose.Dead:
                yetiRenderer.sprite = currentCostume.dead;
                break;

            case YetiPose.Idle1:
                yetiRenderer.sprite = currentCostume.idle1;
                break;

            case YetiPose.Idle2:
                yetiRenderer.sprite = currentCostume.idle2;
                break;
        }

        Invoke(nameof(ResetSprite), yetiPunchInterval);
    }

    public void SetSprite(string sprite) {
        switch(sprite)
        {
            case "left":
                SetSprite(PunchSide.Left);
                break;

            case "both":
                SetSprite(YetiPose.Both);
                break;

            case "right":
                SetSprite(PunchSide.Right);
                break;

            case "dead":
                SetSprite(YetiPose.Dead);
                break;

            case "idle1":
                SetSprite(YetiPose.Idle1);
                break;

            case "idle2":
                SetSprite(YetiPose.Idle2);
                break;

            default:
                Debug.LogWarning("Unknown yeti sprite: " + sprite);
                break;
        }
    }

    public void ResetSprite()
    {
        yetiRenderer.sprite = currentCostume.both;
        goldOutlineRenderer.sprite = yetiGold_bothUp;
    }
}
