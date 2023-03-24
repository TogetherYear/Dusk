using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFeetIK : MonoBehaviour
{
    #region Feet

    public Animator at;

    private Vector3 leftFootPosition;

    private Vector3 rightFootPosition;

    private Vector3 leftFootIKPosition;

    private Vector3 rightFootIKPosition;

    private Quaternion leftFootIKRotation;

    private Quaternion rightFootIKRotation;

    private float lastLeftFootPositionY;

    private float lastRightFootPositionY;

    private float lastPelvisPositionY;

    public bool enableFeetIK = true;

    [Range(0.0f, 2.0f)]
    public float heightFromGroundRaycast = 1.14f;

    [Range(0.0f, 2.0f)]
    public float raycastDistance = 1.5f;

    public LayerMask environmentLayer;

    public float pelvisOffset = 0.0f;

    [Range(0.0f, 1.0f)]
    public float pelvisUpAndDownSpeed = 0.28f;

    [Range(0.0f, 1.0f)]
    public float footToIKPositionSpeed = 0.5f;

    public bool useAnimCurve = true;

    public string leftFootAnimVariableName = "LeftFootIKCurve";

    public string rightFootAnimVariableName = "RightFootIKCurve";

    public bool useProIKFeature = false;

    public bool showSolverDebug = true;

    public Color debugLineColor = Color.red;

    #endregion

    #region Hands

    private bool isOrigin = true;
    private float originBodyPositionY;

    #endregion

    private void SetOriginValues()
    {
        if (isOrigin)
        {
            isOrigin = false;
            Vector3 temp = transform.InverseTransformPoint(at.bodyPosition);
            originBodyPositionY = temp.y;
        }
    }

    private void FixedUpdate()
    {
        if (enableFeetIK)
        {
            AdjustFootTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFootTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);
            FootPositionSolver(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation);
            FootPositionSolver(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        SetOriginValues();
        if (enableFeetIK)
        {
            MovePelvisHeight();
            at.SetIKPositionWeight(AvatarIKGoal.RightFoot, useAnimCurve ? at.GetFloat(rightFootAnimVariableName) : 1.0f);
            at.SetIKPositionWeight(AvatarIKGoal.LeftFoot, useAnimCurve ? at.GetFloat(leftFootAnimVariableName) : 1.0f);

            if (useProIKFeature)
            {
                at.SetIKRotationWeight(AvatarIKGoal.RightFoot, useAnimCurve ? at.GetFloat(rightFootAnimVariableName) : 1.0f);
                at.SetIKRotationWeight(AvatarIKGoal.LeftFoot, useAnimCurve ? at.GetFloat(leftFootAnimVariableName) : 1.0f);
            }

            MoveFootToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);
            MoveFootToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
        }
        MoveHandsHeight(AvatarIKGoal.LeftHand);
        MoveHandsHeight(AvatarIKGoal.RightHand);
    }

    private void MoveFootToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
    {
        Vector3 targetIKPosition = at.GetIKPosition(foot);
        if (positionIKHolder != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, footToIKPositionSpeed);
            targetIKPosition.y += yVariable;
            lastFootPositionY = yVariable;

            targetIKPosition = transform.TransformPoint(targetIKPosition);

            at.SetIKRotation(foot, rotationIKHolder);
        }
        at.SetIKPosition(foot, targetIKPosition);
    }

    private void MovePelvisHeight()
    {
        if (rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastPelvisPositionY == 0)
        {
            lastPelvisPositionY = at.bodyPosition.y;
            return;
        }
        float leftFootOffsetHeight = leftFootIKPosition.y - transform.position.y;
        float rightFootOffsetHeight = rightFootIKPosition.y - transform.position.y;

        float totalOffset = leftFootOffsetHeight < rightFootOffsetHeight ? leftFootOffsetHeight : rightFootOffsetHeight;
        Vector3 newPelvisPosition = at.bodyPosition + Vector3.up * totalOffset;

        newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);
        at.bodyPosition = newPelvisPosition;
        lastPelvisPositionY = at.bodyPosition.y;
    }

    private void FootPositionSolver(Vector3 fromSkyPosition, ref Vector3 footIKPosition, ref Quaternion footIKRotation)
    {
        RaycastHit footHitOut;
        if (showSolverDebug)
        {
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDistance + heightFromGroundRaycast), debugLineColor);
        }
        if (Physics.Raycast(fromSkyPosition, Vector3.down, out footHitOut, raycastDistance + heightFromGroundRaycast, environmentLayer))
        {
            footIKPosition = fromSkyPosition;
            footIKPosition.y = footHitOut.point.y + pelvisOffset;
            footIKRotation = Quaternion.FromToRotation(Vector3.up, footHitOut.normal) * transform.rotation;
        }
        else
        {
            footIKPosition = Vector3.zero;
        }
    }

    private void AdjustFootTarget(ref Vector3 footPosition, HumanBodyBones foot)
    {
        footPosition = at.GetBoneTransform(foot).position;
        footPosition.y = transform.position.y + heightFromGroundRaycast;
    }

    private void MoveHandsHeight(AvatarIKGoal hand)
    {
        Vector3 targetIKPosition = at.GetIKPosition(hand);
        Vector3 targetBodyPosition = transform.InverseTransformPoint(at.bodyPosition);
        at.SetIKPositionWeight(hand, 1.0f);
        at.SetIKPosition(hand, new Vector3(targetIKPosition.x, targetIKPosition.y - (originBodyPositionY - targetBodyPosition.y), targetIKPosition.z));
    }
}
