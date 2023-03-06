using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class CharacterControl : NetworkBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D characterRB;
    public GameObject characterGO;
    public GameObject massBorder;
    public GameObject bow;
    public GameObject pivot;
    public GameObject center;
    public GameObject arrowPrefab;
    public bool isHoldingL = true;
    public PlayerController.Controller playerController;
    public Bow.PlayerBow playerBow;
    public Arrow.PlayerArrow playerArrow;
    public SpriteRenderer _renderer;
    private float jumpForce = 10;
    private float moveForce = 2;
    GameObject[] players;
    Vector3 moveDirection;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Player0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> losses = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public int los;

    private int rotationDirection;

    public override void OnNetworkSpawn()
    {
        playerName.text = networkPlayerName.Value.ToString();
        NameClientRpc();
    }

    void HandleMovement()
    {
        if (IsLocalPlayer)
        {
            los = losses.Value / 2;
            networkPlayerName.Value = "Player" + (OwnerClientId + 1) + ": " + los.ToString();
            playerName.text = networkPlayerName.Value.ToString();
            NameClientRpc();

            if (pivot.transform.rotation.z >= -180 && pivot.transform.rotation.z < 180)
            {
                rotationDirection = -1;
            }
            else
            {
                rotationDirection = 1;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                pivot.transform.RotateAround(transform.position, new Vector3(0, 0, -1 * rotationDirection), 300 * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                pivot.transform.RotateAround(transform.position, new Vector3(0, 0, 1 * rotationDirection), 300 * Time.deltaTime);
                //Debug.Log("rotating RIGHT");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                characterRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                characterRB.AddForce(Vector2.left * moveForce, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                characterRB.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        los = losses.Value / 2;
        if (IsServer && IsLocalPlayer)
        {
            HandleMovement();
        } else
        {
            if (IsLocalPlayer)
            {
                NameServerRpc();

                if (pivot.transform.rotation.z >= -180 && pivot.transform.rotation.z < 180)
                {
                    NormalizeLeftServerRpc();
                }
                else
                {
                    NormalizeRightServerRpc();
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    ArrowLeftServerRpc();
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    ArrowRightServerRpc();
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    UpServerRpc();
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    LeftServerRpc();
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    RightServerRpc();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            WallServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ArrowLeftServerRpc()
    {
        pivot.transform.RotateAround(transform.position, new Vector3(0, 0, -1 * rotationDirection), 300 * Time.deltaTime);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ArrowRightServerRpc()
    {
        pivot.transform.RotateAround(transform.position, new Vector3(0, 0, 1 * rotationDirection), 300 * Time.deltaTime);
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpServerRpc()
    {
        characterRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    [ServerRpc(RequireOwnership = false)]
    private void LeftServerRpc()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        characterRB.AddForce(Vector2.left * moveForce, ForceMode2D.Force);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RightServerRpc()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        characterRB.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
    }

    [ServerRpc(RequireOwnership = false)]
    private void NormalizeLeftServerRpc()
    {
        rotationDirection = -1;
    }

    [ServerRpc(RequireOwnership = false)]
    private void NormalizeRightServerRpc()
    {
        rotationDirection = 1;
    }

    [ServerRpc(RequireOwnership = false)]
    private void WallServerRpc()
    {
        WallClientRpc();
        losses.Value += 1;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    [ClientRpc]
    private void WallClientRpc()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        networkPlayerName.Value = "Player" + (OwnerClientId + 1) + ": " + losses.Value.ToString();
        playerName.text = networkPlayerName.Value.ToString();
    }

    [ServerRpc]
    private void NameServerRpc()
    {
        los = losses.Value / 2;
        networkPlayerName.Value = "Player" + (OwnerClientId + 1) + ": " + los.ToString();
        playerName.text = networkPlayerName.Value.ToString();
        NameClientRpc();
    }

    [ClientRpc]
    private void NameClientRpc()
    {
        playerName.text = networkPlayerName.Value.ToString();
    }
}
