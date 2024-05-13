/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Events;

namespace AccidentalPicasso.UI.Palette
{
    /// <summary>
    /// Displays palette at the non-dominant hand with a specified offset
    /// </summary>
    ///
    public class PalettePositionBehavior : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IHand))]
        private Object _leftHand;

        [SerializeField, Interface(typeof(IHand))]
        private Object _rightHand;
        
        [SerializeField]
        private GameObject _leftPalmRef;
        [SerializeField]
        private GameObject _rightPalmRef;


        [SerializeField]
        private Vector3 _leftAnchorPoint = new Vector3(0f, 0.01f, 0.12f);

        [SerializeField]
        private Vector3 _rightAnchorPoint = new Vector3(0.0652603358f, -0.011439844f, -0.00455812784f);

        private IHand LeftHand { get; set; }
        private IHand RightHand { get; set; }


        protected virtual void Awake()
        {
            LeftHand = _leftHand as IHand;
            RightHand = _rightHand as IHand;
        }

        private void Update()
        {
            var anchor = LeftHand.IsDominantHand ? _rightAnchorPoint : _leftAnchorPoint;
            var hand = LeftHand.IsDominantHand ? RightHand : LeftHand;
            var palmRef = LeftHand.IsDominantHand ? _rightPalmRef : _leftPalmRef;

            var anchorPose = new Pose(anchor, Quaternion.identity).GetTransformedBy(palmRef.transform.GetPose());
            var directionToCamera = (Camera.main.transform.position - anchorPose.position).normalized;
            this.transform.SetPositionAndRotation(anchorPose.position, Quaternion.LookRotation(directionToCamera * 0.01f));

            Pose wristPose;
            if (hand.GetJointPose(HandJointId.HandWristRoot, out wristPose))
            {
                var handToCamera = (Camera.main.transform.position - palmRef.transform.position).normalized;
                var handNormal = palmRef.transform.up;
                var angleToCamera = Vector3.Angle(handNormal, directionToCamera);
                if(angleToCamera <= 60.0f)
                {
                    GetComponent<PaletteBehavior>().ToggleMenu(true);
                    GetComponent<VoteBehavior>().ToggleMenu(true);
                } else
                {
                    GetComponent<PaletteBehavior>().ToggleMenu(false);
                    GetComponent<VoteBehavior>().ToggleMenu(false);
                }

            }

        }

        public IHand GetDominantHand()
        {
            return LeftHand.IsDominantHand ? LeftHand : RightHand;
        }
    }
}
