using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongParameters : MonoBehaviour
{
    public int cardCount;
    public float handMixMaxAngle;
    public float cardsMixDragForce;
    public float cardsMixCentripetalForce;
    public float cardsMixCentripetalMaxDistance;
    public float cardsMixFollowingForce;
    public float cardsMixFollowingMinDistance;
    public float cardsBuildDragForce;
    public float cardsBuildMoveSpeed;
    public float cardsBuildRotationSpeed;
    public float cardsBuildHeight;
    public float cardsBuildWeight;
    public float builtWallMoveSpeed;


    public static int CardCount;
    public static float HandMixMaxAngle;
    public static float CardsMixDragForce;
    public static float CardsMixCentripetalForce;
    public static float CardsMixCentripetalMaxDistance;
    public static float CardsMixFollowingForce;
    public static float CardsMixFollowingMinDistance;
    public static float CardsBuildDragForce;
    public static float CardsBuildMoveSpeed;
    public static float CardsBuildRotationSpeed;
    public static float CardsBuildHeight;
    public static float CardsBuildWeight;
    public static float BuiltWallMoveSpeed;
    private void OnEnable()
    {
        CardCount = cardCount;
        HandMixMaxAngle = handMixMaxAngle;
        CardsMixDragForce = cardsMixDragForce;
        CardsMixCentripetalForce = cardsMixCentripetalForce;
        CardsMixCentripetalMaxDistance = cardsMixCentripetalMaxDistance;
        CardsMixFollowingForce = cardsMixFollowingForce;
        CardsMixFollowingMinDistance = cardsMixFollowingMinDistance;
        CardsBuildDragForce = cardsBuildDragForce;
        CardsBuildMoveSpeed = cardsBuildMoveSpeed;
        CardsBuildRotationSpeed = cardsBuildRotationSpeed;
        CardsBuildHeight = cardsBuildHeight;
        CardsBuildWeight = cardsBuildWeight;
        BuiltWallMoveSpeed = builtWallMoveSpeed;
    }
}
