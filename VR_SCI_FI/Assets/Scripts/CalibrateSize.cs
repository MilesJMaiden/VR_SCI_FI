﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateSize : MonoBehaviour
{
    public Transform upperArmBoneRight, lowerArmBoneRight;
    public Transform upperArmBoneLeft, lowerArmBoneLeft;
    public float scalePct = 0.05f;
    private float scaleHeight, scaleArms;

    public void GrowHeight ()
    {
        scaleHeight = this.transform.localScale.y + scalePct;
        this.gameObject.transform.localScale = new Vector3(scaleHeight, scaleHeight, scaleHeight);
    }

    public void ShrinkHeight()
    {
        scaleHeight = this.transform.localScale.y - scalePct;
        this.gameObject.transform.localScale = new Vector3(scaleHeight, scaleHeight, scaleHeight);
    }

    public void GrowArms()
    {
        scaleArms = lowerArmBoneLeft.localScale.y + scalePct;
        lowerArmBoneLeft.localScale = upperArmBoneLeft.localScale = lowerArmBoneRight.localScale = upperArmBoneRight.localScale =
            new Vector3(scaleArms, scaleArms, scaleArms);
    }

    public void ShrinkArms()
    {
        scaleArms = lowerArmBoneLeft.localScale.y - scalePct;
        lowerArmBoneLeft.localScale = upperArmBoneLeft.localScale = lowerArmBoneRight.localScale = upperArmBoneRight.localScale =
            new Vector3(scaleArms, scaleArms, scaleArms);
    }

}
