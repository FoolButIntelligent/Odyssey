using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{
   [Header("Camera Settings")]
   public Player player;
   public float maxDistance = 15f;
   public float initialAngle = 20f;
   public float heightOffset = 1f;
   protected CinemachineVirtualCamera m_camera;
   protected Cinemachine3rdPersonFollow m_cameraBody;
   protected CinemachineBrain m_brain;
   protected Transform m_target;
   protected float m_cameraDistance;
   protected float m_cameraTargetYaw;
   protected float m_cameraTargetPitch;
   protected Vector3 m_cameraTargetPosition;
   
   protected string k_targetName = "Player Follower Camera Target";
   
   protected void Start()
   {
      InitializeComponents();
      InitializeFollower();
      InitializeCamera();
   }

   public virtual void Reset()
   {
      m_cameraDistance = maxDistance;
      m_cameraTargetPitch = initialAngle;
      m_cameraTargetYaw = player.transform.rotation.eulerAngles.y;
      m_cameraTargetPosition = player.unsizePosition + Vector3.up * heightOffset;
   }

   protected virtual void MoveTarget()
   {
      m_target.position = m_cameraTargetPosition;
      m_target.rotation = Quaternion.Euler(m_cameraTargetPitch, m_cameraTargetYaw, 0f);
   }
   
   private void InitializeCamera()
   {
      m_camera.Follow = m_target.transform;
      m_camera.LookAt = player.transform;
   }

   private void InitializeFollower()
   {
      m_target = new GameObject(k_targetName).transform;
      m_target.position = player.transform.position;
   }

   protected virtual void InitializeComponents()
   {
      if (!player)
      {
         player = FindObjectOfType<Player>();
      }

      m_camera = GetComponent<CinemachineVirtualCamera>();
      m_cameraBody = m_camera.AddCinemachineComponent<Cinemachine3rdPersonFollow>();
      m_brain = Camera.main.GetComponent<CinemachineBrain>();
   }
}
