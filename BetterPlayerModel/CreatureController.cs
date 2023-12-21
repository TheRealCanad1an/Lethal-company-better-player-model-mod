using UnityEngine;
using GameNetcodeStuff;
using UnityEngine.Rendering.HighDefinition;

namespace BetterPlayerModel
{
	public class LethalCreature
	{
		public class CreatureController : MonoBehaviour
		{
			GameObject playerObject = null;
			SkinnedMeshRenderer thisPlayerModel = null;
			public SkinnedMeshRenderer myPlayerModel = null;
			Transform badge = null;
			Transform badge2 = null;

			//make a little shorter -- DONE
			//Make head rotate with other cosmectics -- DONE
			//move player to work with cosmectices -- DONE
			//make badges work -- DONE
			//host doesnt see new models

			//when a player dies and respawn it doesnt show model -- DONE

			//Better whight paint that removes the whight from the neck on the head bone and tweaks the sides

			/*private void Awake()
            {
				if () thisPlayerModel.enabled = true;
			}*/

			private void Start()
			{
				((Component)this).gameObject.GetComponentInChildren<LODGroup>().enabled = false;
				SkinnedMeshRenderer[] componentsInChildren = ((Component)this).gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				SkinnedMeshRenderer[] array = componentsInChildren;
				foreach (SkinnedMeshRenderer val in array)
				{
					((Renderer)val).enabled = false;
				}

				playerObject = Object.Instantiate<GameObject>(BetterModelBase.playerModel);
				playerObject.transform.localScale = Vector3.one * .16f;
				Transform val3 = ((Component)this).gameObject.transform.Find("ScavengerModel").Find("metarig");
				Transform parent = val3.Find("spine").Find("spine.001");
				playerObject.transform.SetParent(val3);
				playerObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				playerObject.transform.localEulerAngles = Vector3.zero;
				thisPlayerModel = ((Component)this).gameObject.GetComponent<PlayerControllerB>().thisPlayerModel;
				Shader shader = ((Renderer)thisPlayerModel).material.shader;
				SkinnedMeshRenderer componentInChildren = playerObject.GetComponentInChildren<SkinnedMeshRenderer>();
				((Renderer)componentInChildren).materials[0].shader = shader;
				((Renderer)componentInChildren).materials[0].SetTexture("_BaseColorMap", BetterModelBase.textures[0]);
				((Renderer)componentInChildren).materials[0].SetFloat("_Smoothness", 0.3f);
				((Renderer)componentInChildren).materials[1] = ((Renderer)componentInChildren).materials[0];
				((Renderer)componentInChildren).materials[1].shader = shader;
				((Renderer)componentInChildren).materials[1].SetTexture("_BaseColorMap", BetterModelBase.textures[1]);
				((Renderer)componentInChildren).materials[1].SetFloat("_Smoothness", 0.3f);
				((Renderer)componentInChildren).materials[2] = ((Renderer)componentInChildren).materials[0];
				((Renderer)componentInChildren).materials[2].shader = shader;
				((Renderer)componentInChildren).materials[2].SetTexture("_BaseColorMap", BetterModelBase.textures[2]);
				((Renderer)componentInChildren).materials[2].SetFloat("_Smoothness", 0.3f);
				HDMaterial.ValidateMaterial(((Renderer)componentInChildren).materials[0]);
				HDMaterial.ValidateMaterial(((Renderer)componentInChildren).materials[1]);
				HDMaterial.ValidateMaterial(((Renderer)componentInChildren).materials[2]);
				Animator componentInChildren2 = playerObject.GetComponentInChildren<Animator>();
				componentInChildren2.runtimeAnimatorController = BetterModelBase.animationController;
				//IKController iKController = val2.AddComponent<IKController>();
				Transform val4 = val3.Find("spine").Find("thigh.L");
				Transform val5 = val3.Find("spine").Find("thigh.R");
				Transform val6 = val4.Find("shin.L");
				Transform val7 = val5.Find("shin.R");
				Transform val8 = val6.Find("foot.L");
				Transform val9 = val7.Find("foot.R");
				Transform val10 = val3.Find("spine").Find("spine.001").Find("spine.002").Find("spine.003");
				Transform val11 = val10.Find("shoulder.L");
				Transform val12 = val10.Find("shoulder.R");
				Transform val13 = val11.Find("arm.L_upper");
				Transform val14 = val12.Find("arm.R_upper");
				Transform val15 = val13.Find("arm.L_lower");
				Transform val16 = val14.Find("arm.R_lower");
				Transform val17 = val15.Find("hand.L");
				Transform val18 = val16.Find("hand.R");
				/*GameObject val19 = new GameObject("IK Offset");
				val19.transform.SetParent(val8, false);
				val19.transform.localPosition = new Vector3(0f, -0.1f, 0f);
				GameObject val20 = new GameObject("IK Offset");
				val20.transform.SetParent(val9, false);
				val20.transform.localPosition = new Vector3(0f, -0.1f, 0f);
				GameObject val21 = new GameObject("IK Offset");
				val21.transform.SetParent(val17, false);
				val21.transform.localPosition = new Vector3(0f, 0f, 0f);
				GameObject val22 = new GameObject("IK Offset");
				val22.transform.SetParent(val18, false);
				val22.transform.localPosition = new Vector3(0f, 0f, 0f);*/
				//iKController.leftLegTarget = val19.transform;
				//iKController.rightLegTarget = val20.transform;
				//iKController.leftHandTarget = val21.transform;
				//iKController.rightHandTarget = val22.transform;
				//iKController.ikActive = true;
				/*Transform[] rootBoneChildren = val3.GetComponentsInChildren<Transform>();
				foreach (Transform child in rootBoneChildren)
				{
					if (child.name == "BetaBadge")
                    {
						badge = child;
						break;
					}
				}
				foreach (Transform child in rootBoneChildren)
				{
					if (child.name == "Badge")
					{
						badge2 = child;
						break;
					}
				}*/

				//BetterModelBase.nls.LogInfo("RealPlayerModel: " + thisPlayerModel);
				myPlayerModel = playerObject.transform.GetComponentInChildren<SkinnedMeshRenderer>();
			}

			void UpdateTransforms(Transform thisTransform, Transform transform, bool setPos, bool setRot, Vector3 offSet, Vector3 rotOffSet, bool worldPos, bool worldRot)
            {
				if (setPos)
                {
					if (worldPos) thisTransform.position = transform.position + offSet;
					else thisTransform.localPosition = transform.localPosition + offSet;
				}
				if (setRot)
                {
					if (worldRot) thisTransform.eulerAngles = transform.eulerAngles + rotOffSet;
					else thisTransform.localEulerAngles = transform.localEulerAngles + rotOffSet;
				}
			}

			private void LateUpdate()
			{
				if ((Object)(object)playerObject != (Object)null)
				{
					playerObject.transform.localPosition = Vector3.zero;
					Transform val = ((Component)this).gameObject.transform.Find("ScavengerModel").Find("metarig");
					Transform val2 = val.Find("spine");//.Find("spine.001").Find("spine.002").Find("spine.003");
					Transform val3 = val2.Find("spine.001");//.Find("spine.001").Find("spine.002").Find("spine.003");
					Transform val4 = val3.Find("spine.002");//.Find("spine.001").Find("spine.002").Find("spine.003");
					Transform val5 = val4.Find("spine.003");//.Find("spine.001").Find("spine.002").Find("spine.003");
					Transform val40 = val5.Find("spine.004");//.Find("spine.001").Find("spine.002").Find("spine.003");
					Transform val6 = val2.Find("thigh.L");
					Transform val7 = val2.Find("thigh.R");
					Transform val8 = val6.Find("shin.L");
					Transform val9 = val7.Find("shin.R");
					Transform val10 = val8.Find("foot.L");
					Transform val11 = val9.Find("foot.R");
					Transform val12 = val5.Find("shoulder.L");
					Transform val13 = val5.Find("shoulder.R");
					Transform val14 = val12.Find("arm.L_upper");
					Transform val15 = val13.Find("arm.R_upper");
					Transform val16 = val14.Find("arm.L_lower");
					Transform val17 = val15.Find("arm.R_lower");
					Transform val18 = val16.Find("hand.L");
					Transform val19 = val17.Find("hand.R");
					Transform val20 = val18.Find("finger2.L");
					Transform val21 = val19.Find("finger2.R");
					Transform val22 = val20.Find("finger2.L.001");
					Transform val23 = val21.Find("finger2.R.001");
					Transform val24 = val18.Find("finger3.L");
					Transform val25 = val19.Find("finger3.R");
					Transform val26 = val24.Find("finger3.L.001");
					Transform val27 = val25.Find("finger3.R.001");
					Transform val28 = val18.Find("finger4.L");
					Transform val29 = val19.Find("finger4.R");
					Transform val30 = val28.Find("finger4.L.001");
					Transform val31 = val29.Find("finger4.R.001");
					Transform val32 = val18.Find("finger5.L");
					Transform val33 = val19.Find("finger5.R");
					Transform val34 = val32.Find("finger5.L.001");
					Transform val35 = val33.Find("finger5.R.001");
					Transform val36 = val18.Find("finger1.L");
					Transform val37 = val19.Find("finger1.R");
					Transform val38 = val36.Find("finger1.L.001");
					Transform val39 = val37.Find("finger1.R.001");

					Transform newPlayer = playerObject.transform;

					Transform handL = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").Find("arm.L_upper").Find("arm.L_lower").Find("hand.L");
					Transform handR = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").Find("arm.R_upper").Find("arm.R_lower").Find("hand.R");

					float itemOffSet = -.05f;

					newPlayer.Find("Armature").localEulerAngles = new Vector3(90, 0, 0);
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine"), val2, true, true, (Vector3.up * .19f) + val2.forward * itemOffSet, new Vector3(90, 0, 0), true, true);
					newPlayer.Find("Armature").Find("spine").position = val2.position + (Vector3.up * .19f) + val2.forward * itemOffSet;
					newPlayer.Find("Armature").Find("spine").localEulerAngles = val2.localEulerAngles + new Vector3(90, 0, 0);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").localEulerAngles = val3.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001"), val3, true, true, val3.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").localEulerAngles = val4.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002"), val40, true, true, val40.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("spine.003").rotation = val40.rotation;//val5.rotation
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("spine.003"), val40, true, true, val40.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("spine.003").position = val40.position + val40.forward * itemOffSet;//val5.rotation
					//oringaly off newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("spine.003").Find("spine.004").rotation = val40.rotation;
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L"), val12, true, true, val12.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R"), val13, true, true, val13.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").localEulerAngles = val12.localEulerAngles;
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").localEulerAngles = val13.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").Find("arm.L_upper"), val14, true, true, val14.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").Find("arm.R_upper"), val15, true, true, val15.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").Find("arm.L_upper").localEulerAngles = val14.localEulerAngles;
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").Find("arm.R_upper").localEulerAngles = val15.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").Find("arm.L_upper").Find("arm.L_lower"), val16, true, true, val16.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").Find("arm.R_upper").Find("arm.R_lower"), val17, true, true, val17.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.L").Find("arm.L_upper").Find("arm.L_lower").localEulerAngles = val16.localEulerAngles;
					newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("shoulder.R").Find("arm.R_upper").Find("arm.R_lower").localEulerAngles = val17.localEulerAngles;
					//UpdateTransforms(handL, val18, true, true, val18.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR, val19, true, true, val19.forward * itemOffSet, Vector3.zero, true, true);
					handL.localEulerAngles = val18.localEulerAngles;
					handR.localEulerAngles = val19.localEulerAngles;
					//UpdateTransforms(handL.Find("finger2.L"), val20, true, true, val20.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger2.R"), val21, true, true, val21.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger2.L").localEulerAngles = val20.localEulerAngles;
					handR.Find("finger2.R").localEulerAngles = val21.localEulerAngles;
					//UpdateTransforms(handL.Find("finger2.L").Find("finger2.L.001"), val22, true, true, val22.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger2.R").Find("finger2.R.001"), val23, true, true, val23.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger2.L").Find("finger2.L.001").localEulerAngles = val22.localEulerAngles;
					handR.Find("finger2.R").Find("finger2.R.001").localEulerAngles = val23.localEulerAngles;
					//UpdateTransforms(handL.Find("finger3.L"), val24, true, true, val24.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger3.R"), val25, true, true, val25.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger3.L").localEulerAngles = val24.localEulerAngles;
					handR.Find("finger3.R").localEulerAngles = val25.localEulerAngles;
					//UpdateTransforms(handL.Find("finger3.L").Find("finger3.L.001"), val26, true, true, val26.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger3.R").Find("finger3.R.001"), val27, true, true, val27.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger3.L").Find("finger3.L.001").localEulerAngles = val26.localEulerAngles;
					handR.Find("finger3.R").Find("finger3.R.001").localEulerAngles = val27.localEulerAngles;
					//UpdateTransforms(handL.Find("finger4.L"), val28, true, true, val28.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger4.R"), val29, true, true, val29.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger4.L").localEulerAngles = val28.localEulerAngles;
					handR.Find("finger4.R").localEulerAngles = val29.localEulerAngles;
					//UpdateTransforms(handL.Find("finger4.L").Find("finger4.L.001"), val30, true, true, val30.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger4.R").Find("finger4.R.001"), val31, true, true, val31.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger4.L").Find("finger4.L.001").localEulerAngles = val30.localEulerAngles;
					handR.Find("finger4.R").Find("finger4.R.001").localEulerAngles = val31.localEulerAngles;
					//UpdateTransforms(handL.Find("finger5.L"), val32, true, true, val32.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger5.R"), val33, true, true, val33.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger5.L").localEulerAngles = val32.localEulerAngles;
					handR.Find("finger5.R").localEulerAngles = val33.localEulerAngles;
					//UpdateTransforms(handL.Find("finger5.L").Find("finger5.L.001"), val34, true, true, val34.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger5.R").Find("finger5.R.001"), val35, true, true, val35.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger5.L").Find("finger5.L.001").localEulerAngles = val34.localEulerAngles;
					handR.Find("finger5.R").Find("finger5.R.001").localEulerAngles = val35.localEulerAngles;
					//UpdateTransforms(handL.Find("finger1.L"), val36, true, true, val36.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger1.R"), val37, true, true, val37.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger1.L").localEulerAngles = val36.localEulerAngles;
					handR.Find("finger1.R").localEulerAngles = val37.localEulerAngles;
					//UpdateTransforms(handL.Find("finger1.L").Find("finger1.L.001"), val38, true, true, val38.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(handR.Find("finger1.R").Find("finger1.R.001"), val39, true, true, val39.forward * itemOffSet, Vector3.zero, true, true);
					handL.Find("finger1.L").Find("finger1.L.001").localEulerAngles = val38.localEulerAngles;
					handR.Find("finger1.R").Find("finger1.R.001").localEulerAngles = val39.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.L"), val6, true, true, (Vector3.up * .15f) - val6.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.R"), val7, true, true, (Vector3.up * .15f) - val7.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("thigh.L").rotation = val6.rotation;
					newPlayer.Find("Armature").Find("thigh.R").rotation = val7.rotation;
					newPlayer.Find("Armature").Find("thigh.L").position = val6.position + (Vector3.up * .15f) - val6.forward * itemOffSet;
					newPlayer.Find("Armature").Find("thigh.R").position = val7.position + (Vector3.up * .15f) - val7.forward * itemOffSet;
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.L").Find("shin.L"), val8, true, true, val8.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.R").Find("shin.R"), val9, true, true, val9.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("thigh.L").Find("shin.L").localEulerAngles = val8.localEulerAngles;
					newPlayer.Find("Armature").Find("thigh.R").Find("shin.R").localEulerAngles = val9.localEulerAngles;
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.L").Find("shin.L").Find("foot.L"), val10, true, true, val10.forward * itemOffSet, Vector3.zero, true, true);
					//UpdateTransforms(newPlayer.Find("Armature").Find("thigh.R").Find("shin.R").Find("foot.R"), val11, true, true, val11.forward * itemOffSet, Vector3.zero, true, true);
					newPlayer.Find("Armature").Find("thigh.L").Find("shin.L").Find("foot.L").localEulerAngles = val10.localEulerAngles;
					newPlayer.Find("Armature").Find("thigh.R").Find("shin.R").Find("foot.R").localEulerAngles = val11.localEulerAngles;

					//badge.position = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("Badge").position;
					//badge.rotation = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("Badge").rotation * Quaternion.Euler(180, 90, 0);
					//badge2.position = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("Badge").position + new Vector3(0, .1f, 0);
					//badge2.eulerAngles = newPlayer.Find("Armature").Find("spine").Find("spine.001").Find("spine.002").Find("Badge").eulerAngles + new Vector3(180, 90, 0);
				}
			}
		}
	}
	public class IKController : MonoBehaviour
	{
		protected Animator animator;

		public bool ikActive = false;

		public Transform leftLegTarget = null;

		public Transform rightLegTarget = null;

		public Transform leftHandTarget = null;

		public Transform rightHandTarget = null;

		private void Start()
		{
			animator = ((Component)this).GetComponent<Animator>();
		}

		private void OnAnimatorIK()
		{
			/*if (!Object.op_Implicit((Object)(object)animator))
			{
				return;
			}*/
			if (ikActive)
			{
				if ((Object)(object)leftLegTarget != (Object)null)
				{
					animator.SetIKPositionWeight((AvatarIKGoal)0, 1f);
					animator.SetIKPosition((AvatarIKGoal)0, leftLegTarget.position);
					animator.SetIKRotation((AvatarIKGoal)0, leftLegTarget.rotation);
				}
				if ((Object)(object)rightLegTarget != (Object)null)
				{
					animator.SetIKPositionWeight((AvatarIKGoal)1, 1f);
					animator.SetIKPosition((AvatarIKGoal)1, rightLegTarget.position);
					animator.SetIKRotation((AvatarIKGoal)1, rightLegTarget.rotation);
				}
				if ((Object)(object)leftHandTarget != (Object)null)
				{
					animator.SetIKPositionWeight((AvatarIKGoal)2, 1f);
					animator.SetIKPosition((AvatarIKGoal)2, leftHandTarget.position);
					animator.SetIKRotation((AvatarIKGoal)2, leftHandTarget.rotation);
				}
				if ((Object)(object)rightHandTarget != (Object)null)
				{
					animator.SetIKPositionWeight((AvatarIKGoal)3, 1f);
					animator.SetIKPosition((AvatarIKGoal)3, rightHandTarget.position);
					animator.SetIKRotation((AvatarIKGoal)3, rightHandTarget.rotation);
				}
			}
			else
			{
				animator.SetIKPositionWeight((AvatarIKGoal)0, 0f);
				animator.SetIKRotationWeight((AvatarIKGoal)0, 0f);
				animator.SetIKPositionWeight((AvatarIKGoal)1, 0f);
				animator.SetIKRotationWeight((AvatarIKGoal)1, 0f);
				animator.SetIKPositionWeight((AvatarIKGoal)2, 0f);
				animator.SetIKRotationWeight((AvatarIKGoal)2, 0f);
				animator.SetIKPositionWeight((AvatarIKGoal)3, 0f);
				animator.SetIKRotationWeight((AvatarIKGoal)3, 0f);
			}
		}
	}
}