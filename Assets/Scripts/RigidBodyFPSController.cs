using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class RigidBodyFPSController : MonoBehaviour {
    [System.Serializable]
    public class MoveSetting
    {
        public float forwardSpeed = 8.0f;
        public float backSpeed = 4.0f;
        public float sideSpeed = 4.0f;
        public float runMultiplier = 2.0f;
        public KeyCode runKey = KeyCode.LeftShift;
        public float jumpForce = 30.0f;
        public AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 1.0f));
        public float currentTargetSpeed = 8.0f;

        public bool _running;
        public void UpdateDesiredTargetSpeed(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                return;
            }
            if (input.x > 0 || input.x <0)
            {
                currentTargetSpeed = sideSpeed;
            }
            if (input.y < 0)
            {
                currentTargetSpeed = backSpeed;
            }
            if (input.y > 0)
            {
                currentTargetSpeed = forwardSpeed;
            }

            if (Input.GetKey(runKey))
            {
                currentTargetSpeed *= runMultiplier;
                _running = true;
            }
            else
            {
                _running = false;
            }

        }
        public bool Running
        {
            get { return _running; }
        }
    }


    public class AdvancedSettings
    {
        public float groundCheckDistanse = 0.01f;
        public float stickToGroundHelperDistance = 0.5f;
        public float slowDownRate = 20.0f;
        public bool airControll;
        [Tooltip("set it to 0.1 or more if you get stuck in wall")]
        public float shellOffset;
    }
    public Camera cam;
    public MoveSetting movesetting = new MoveSetting();
    public MouseManager mousemanager = new MouseManager();
    public AdvancedSettings advancedSettings = new AdvancedSettings();

    private Rigidbody rb;
    private CapsuleCollider capsueleCol;
    private float rotationY;
    private Vector3 groundHit;
    private bool jump, oldGround, jumping, isGrounded;

    public Vector3 Velocity
    {
        get { return rb.velocity; }
    }

    public bool Grounded
    {
        get { return isGrounded; }
    }
    public bool Jumping
    {
        get { return jumping; }
    }

    public bool Running
    {
        get { return movesetting.Running; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsueleCol = GetComponent<CapsuleCollider>();
        mousemanager.Init(transform, cam.transform);
    }

    void Update()
    {
        RotateView();
        if (Input.GetKeyDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        Vector2 input = GetInput();
        if ((Math.Abs(input.x) > float.Epsilon || Math.Abs(input.y) > float.Epsilon) && (advancedSettings.airControll || isGrounded))
        {
            Vector3 move = cam.transform.forward * input.y + cam.transform.right * input.x;
            move = Vector3.ProjectOnPlane(move, groundHit).normalized;

            move.x = move.x * movesetting.currentTargetSpeed;
            move.y = move.y * movesetting.currentTargetSpeed;
            move.z = move.z * movesetting.currentTargetSpeed;
            if (rb.velocity.sqrMagnitude <
                (movesetting.currentTargetSpeed * movesetting.currentTargetSpeed))
            {
                rb.AddForce(move * SlopeMultiplier(), ForceMode.Impulse);
            }
        }

            if (isGrounded)
            {
                rb.drag = 5.0f;
                if (jump)
                {
                    rb.drag = 0.0f;
                    rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
                    rb.AddForce(new Vector3(0.0f, movesetting.jumpForce, 0.0f), ForceMode.Impulse);
                    jumping = true;
                }

                if (jumping && Math.Abs(input.x) < float.Epsilon && Math.Abs(input.y) < float.Epsilon && rb.velocity.magnitude < 1.0f)
                {
                    rb.Sleep();
                }
            }
            else
            {
                rb.drag = 0.0f;
                if(oldGround && !jumping)
                {
                    StickToGroundHelper();
                }
            }
            jump = false;
    }

    private float SlopeMultiplier()
    {
        float angle = Vector3.Angle(groundHit, Vector3.up);
        return movesetting.slopeCurveModifier.Evaluate(angle);
    }

    private void StickToGroundHelper()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, capsueleCol.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hit,
            ((capsueleCol.height / 2.0f)-capsueleCol.radius)+
            advancedSettings.stickToGroundHelperDistance,Physics.AllLayers,
            QueryTriggerInteraction.Ignore
            ))
        {
            if (Math.Abs(Vector3.Angle(hit.normal,Vector3.up)) < 85f)
            {
                rb.velocity = Vector3.ProjectOnPlane(rb.velocity, hit.normal);
            }
        }
    }

    private Vector2 GetInput()
    {
        Vector2 _input = new Vector2()
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
        movesetting.UpdateDesiredTargetSpeed(_input);
        return _input;
    }

    private void RotateView()
    {
        if (Math.Abs(Time.timeScale)<float.Epsilon)
        {
            return;
        }
        float oldRotation = transform.eulerAngles.y;

        mousemanager.LookRotation(transform, cam.transform);

        if (isGrounded || advancedSettings.airControll)
        {
            Quaternion velRot = Quaternion.AngleAxis(transform.eulerAngles.y - oldRotation, Vector3.up);
            rb.velocity = velRot * rb.velocity;
        }
    }

    private void GroundCheck()
    {
        oldGround = isGrounded;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, capsueleCol.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hit,
            ((capsueleCol.height / 2.0f) - capsueleCol.radius) + advancedSettings.groundCheckDistanse, Physics.AllLayers,
            QueryTriggerInteraction.Ignore
            ))
        {
            isGrounded = true;
            groundHit = hit.normal;
        }
        else
        {
            isGrounded = false;
            groundHit = Vector3.up;
        }
        if(oldGround && isGrounded && jumping)
        {
            jumping = false;
        }
    }
}
