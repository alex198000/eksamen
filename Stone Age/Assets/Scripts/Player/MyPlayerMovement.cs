using UnityEngine;

namespace Levels
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class MyPlayerMovement : MonoBehaviour
    {
        [Header("Player Property")]
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _playerJumpForce;
        [SerializeField] private GameObject _animObject;
        [SerializeField] private GameObject _player;
        [SerializeField] private Animator _animator;
        private Rigidbody2D _rb;
        private float _currentPlayerSpeed;
        private bool _groundCheck;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            SuperMushroomScript.OnPlayerWin += PlayerWin;
        }

        private void OnDisable()
        {
            SuperMushroomScript.OnPlayerWin -= PlayerWin;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_currentPlayerSpeed * Time.deltaTime, _rb.velocity.y);
            _animator.SetFloat("Speed", Mathf.Abs(_currentPlayerSpeed));
        }
        public void RightMove()
        {
            _currentPlayerSpeed = _playerSpeed;
            //transform.Rotate(0f, 0f, 0f);
            _player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        public void LeftMove()
        {
            _currentPlayerSpeed = -_playerSpeed;
            // transform.Rotate(0f, 180f, 0f);
            _player.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        public void StopMove()
        {
            _currentPlayerSpeed = 0f;
        }
        public void Jump()
        {
            if (_groundCheck)
            {
                _animator.SetTrigger("Jump");
                _rb.velocity = new Vector2(_rb.velocity.x, _playerJumpForce);
                _groundCheck = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _groundCheck = true;
        }

        private void PlayerWin()
        {
            gameObject.SetActive(false);
        }
    }
}




