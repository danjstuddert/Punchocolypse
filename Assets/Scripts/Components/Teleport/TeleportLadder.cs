using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportLadder : TeleportMarkerBase
{
    [Header("Teleport Positions")]
    [SerializeField] private Transform ladderTop;
    [SerializeField] private Transform ladderBottom;

    [Header("Level Loading")]
    [SerializeField] private bool loadLevel;
    [SerializeField] private string levelToLoad;

    private Player player;

    private bool highlighted = false;

    private void Awake()
    {
        player = Player.Instance;
    }

    public override void Highlight(bool highlight)
    {
        if (!locked)
        {
            highlighted = highlight;
        }
    }

    public void TeleportPlayer()
    {
        Teleport.instance.PresendPlayer(this);

        if (player.trackingOriginTransform.position.SqrDistanceTo(ladderTop.position) >
            player.trackingOriginTransform.position.SqrDistanceTo(ladderBottom.position))
        {
            player.trackingOriginTransform.position = ladderTop.position;
            Teleport.instance.SendPlayer(this);
        }
        else
        {
            player.trackingOriginTransform.position = ladderBottom.position;
            Teleport.instance.SendPlayer(this);
        }

    }

    public override void TeleportPlayer(Vector3 pointedAtPosition)
    {
        Teleport.instance.PresendPlayer(this);

        if (loadLevel)
        {
            if (string.IsNullOrEmpty(levelToLoad))
            {
                Debug.LogError(string.Format("Tried to load level from {0} but no level specified", name));
                return;
            }

            GameController.Instance.LoadLevel(levelToLoad);
            return;
        }

        if (player.trackingOriginTransform.position.SqrDistanceTo(ladderTop.position) >
            player.trackingOriginTransform.position.SqrDistanceTo(ladderBottom.position))
        {
            player.trackingOriginTransform.position = ladderTop.position;
            Teleport.instance.SendPlayer(this);
        }
        else
        {
            player.trackingOriginTransform.position = ladderBottom.position;
            Teleport.instance.SendPlayer(this);
        }

    }

    public override void SetAlpha(float tintAlpha, float alphaPercent)
    {

    }

    public override bool ShouldActivate(Vector3 playerPosition)
    {
        return true;
    }

    public override bool ShouldMovePlayer()
    {
        return false;
    }

    public override void UpdateVisuals()
    {
        Debug.Log("Do the visual things");
    }
}
