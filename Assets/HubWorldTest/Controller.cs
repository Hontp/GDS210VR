using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Controller : MonoBehaviour
{
    private Hand hand;
    public SteamVR_Action_Vector2 trackPadPos;


    // Use this for initialization
    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
    }

    public Vector2 getTrackPadPos()
    {
       // SteamVR_Action_Vector2 trackpadPos = SteamVR_Actions._default.touchPos.; //SteamVR_Input._default.inActions.touchPos;
        return trackPadPos[hand.handType].delta;
    }

    public bool getPinch()
    {
        return SteamVR_Actions.default_GrabPinch[hand.handType].state;//SteamVR_Input._default.//.inActions.GrabPinch.GetState(hand.handType);
    }

    public bool getPinchDown()
    {
        return SteamVR_Actions.default_GrabPinch[hand.handType].stateDown;//return SteamVR_Input._default.inActions.GrabPinch.GetStateDown(hand.handType);
    }

    public bool getPinchUp()
    {
        return SteamVR_Actions.default_GrabPinch[hand.handType].stateUp; //return SteamVR_Input._default.inActions.GrabPinch.GetStateUp(hand.handType);
    }

    public bool getGrip()
    {
        return SteamVR_Actions.default_GrabGrip[hand.handType].state; //return SteamVR_Input._default.inActions.GrabGrip.GetState(hand.handType);
    }

    public bool getGrip_Down()
    {
        return SteamVR_Actions.default_GrabGrip[hand.handType].stateDown; //return SteamVR_Input._default.inActions.GrabGrip.GetStateDown(hand.handType);
    }

    public bool getGrip_Up()
    {
        return SteamVR_Actions.default_GrabGrip[hand.handType].stateUp; //return SteamVR_Input._default.inActions.GrabGrip.GetStateUp(hand.handType);
    }

    //public bool getMenu()
    //{
    //    return SteamVR_Actions.default_menuButton[hand.handType].state;  //return SteamVR_Input._default.inActions.MenuButton.GetState(hand.handType);
    //}

    //public bool getMenu_Down()
    //{
    //    return SteamVR_Actions.default_menuButton[hand.handType].stateDown; //return SteamVR_Input._default.inActions.MenuButton.GetStateDown(hand.handType);
    //}

    //public bool getMenu_Up()
    //{
    //    return SteamVR_Actions.default_menuButton[hand.handType].stateUp; //return SteamVR_Input._default.inActions.MenuButton.GetStateUp(hand.handType);
    //}

    public bool getTouchPad()
    {
        return SteamVR_Actions.default_Teleport[hand.handType].state; //return SteamVR_Input._default.inActions.Teleport.GetState(hand.handType);
    }

    public bool getTouchPad_Down()
    {
        return SteamVR_Actions.default_Teleport[hand.handType].stateDown; //return SteamVR_Input._default.inActions.Teleport.GetStateDown(hand.handType);
    }

    public bool getTouchPad_Up()
    {
        return SteamVR_Actions.default_Teleport[hand.handType].stateUp; //return SteamVR_Input._default.inActions.Teleport.GetStateUp(hand.handType);
    }

    public Vector3 getControllerPosition()
    {
        SteamVR_Action_Pose[] poseActions = SteamVR_Actions._default.poseActions;
        if (poseActions.Length > 0)
        {
            return poseActions[0].GetLocalPosition(hand.handType);
        }
        return new Vector3(0, 0, 0);
    }

    public Quaternion getControllerRotation()
    {
        SteamVR_Action_Pose[] poseActions = SteamVR_Actions._default.poseActions;
        if (poseActions.Length > 0)
        {
            return poseActions[0].GetLocalRotation(hand.handType);
        }
        return Quaternion.identity;
    }
}