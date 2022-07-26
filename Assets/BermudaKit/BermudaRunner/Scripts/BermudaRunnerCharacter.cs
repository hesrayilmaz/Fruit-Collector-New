using Bermuda.Runner;
using Bermuda.Animation;
using PathCreation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ali.Helper;
using DG.Tweening;
using System.Collections;

namespace Bermuda.Runner
{
    public class BermudaRunnerCharacter : MonoBehaviour
    {
        [SerializeField] private Transform _localMover;
        [SerializeField] private Transform _localMoverTarget;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private SimpleAnimancer _animancer;
        [SerializeField] private PlayerSwerve _playerSwerve;
        [SerializeField] private EndOfLevelUI _levelPanel;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [Space]
        [SerializeField] private string _idleAnimName = "Idle";
        [SerializeField] private float _idleAnimSpeed = 1f;
        [SerializeField] private string _runAnimName = "Standard Run";
        [SerializeField] private float _runAnimSpeed = 2f;
        [SerializeField] private string _jumpAnimName = "Jumping";
        [SerializeField] private float _jumpAnimSpeed = 2f;
        [SerializeField] private string _slideAnimName = "Female Action Pose";
        [SerializeField] private float _slideAnimSpeed = 2f;
        [SerializeField] private string _speedAnimName = "Speed Pose";
        [SerializeField] private float _speedAnimSpeed = 2f;
        [Space]
        [SerializeField] private float _startDistance = 5f;
        [SerializeField] private float _forwardSpeed = 1f;
        [SerializeField] private float _strafeSpeed = 1f;
        [SerializeField] private float _strafeLerpSpeed = 1f;
        [SerializeField] private float _clampLocalX = 2f;
        [SerializeField] private float _rotateSpeed = 100f;
        [SerializeField] private float _rotateAngle = 100f;
        [Space]
        [SerializeField] private float _dodgeBackDistance = 2f;
        [SerializeField] private float _dodgeBackDuration = 2f;
        [Space]
        [SerializeField] private bool _enabled = true;
        [SerializeField] private bool _rotateEnabled = true;

        private Vector3 _oldPosition;
        private float _distance = 0;
        private bool _running = false;
        private bool _canSwerve = true;
        private bool _dodgingBack = false;
        private Tweener _forwardSpeedTweeen;

        void Awake()
        {
            _playerSwerve.OnSwerve += PlayerSwerve_OnSwerve;
            _distance = _startDistance;
            _oldPosition = _localMoverTarget.localPosition;
            _levelPanel.Init();
        }

        public void Init()
        {
            _pathCreator = FindObjectOfType<PathCreator>();
        }

        void UpdateRotation()
        {
            if (!_enabled)
            {
                return;
            }

            if (!_rotateEnabled)
            {
                return;
            }

            Vector3 direction = _localMoverTarget.localPosition - _oldPosition;
            direction.z += 0.6f;
            _animancer.GetAnimatorTransform().forward = Vector3.Lerp(_animancer.GetAnimatorTransform().forward, direction.normalized, _rotateSpeed * Time.deltaTime);
        }

        public void SetSwerve(bool value)
        {
            _canSwerve = value;
        }

        public void SetRotateEnabled(bool value)
        {
            _rotateEnabled = value;

        }

        public void SetEnabled(bool value)
        {
            _enabled = value;
        }

        private void PlayerSwerve_OnSwerve(Vector2 direction)
        {
            if (_running && _canSwerve)
            {
                _localMoverTarget.localPosition = _localMoverTarget.localPosition + Vector3.right * direction.x * _strafeSpeed * Time.deltaTime;
                ClampLocalPosition();
            }
        }

        void ClampLocalPosition()
        {
            Vector3 pos = _localMoverTarget.localPosition;
            pos.x = Mathf.Clamp(pos.x, -_clampLocalX, _clampLocalX);
            _localMoverTarget.localPosition = pos;

        }

        void Update()
        {
            MoveForward();
            FollowLocalMoverTarget();
            UpdateRotation();
            UpdatePath();
            _oldPosition = _localMover.localPosition;
        }

        public void StartToRun()
        {
            if (_enabled)
            {
                _running = true;
                RunAnimation();
            }
        }

        public void PlayAnimation(string animName, float animSpeed)
        {
            _animancer.PlayAnimation(animName);
            _animancer.SetStateSpeed(animSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Jump")
                StartCoroutine(JumpProcess());
            else if (other.gameObject.tag == "Slide")
            {
                foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                    trail.emitting = true;
                foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
                    particle.Play();
                StartCoroutine(SlideProcess());
            }
            else if (other.gameObject.tag == "Speed")
            {
                StartCoroutine(SpeedProcess());
            }
            else if (other.gameObject.tag == "LevelOverGround")
            {
                _running = false;
                IdleAnimation();
                _levelPanel.ShowPanel();
            }
            else if (other.gameObject.tag == "GameOverGround")
            {
                _running = false;
                IdleAnimation();
                _gameOverPanel.ShowPanel();
            }
        }

        public float GetForwardSpeed()
        {
            return _forwardSpeed;
        }

        public void SetForwardSpeed(float value)
        {
            if (_forwardSpeedTweeen != null)
            {
                _forwardSpeedTweeen.Kill();
            }
            _forwardSpeed = value;
        }

        public void SetForwardSpeed(float value, float duration)
        {
            if (_forwardSpeedTweeen != null)
            {
                _forwardSpeedTweeen.Kill();
            }
            _forwardSpeedTweeen = DOTween.To(() => _forwardSpeed, x => _forwardSpeed = x, value, duration);
        }

        public void SetLocalRotation(Vector3 eulerAngles)
        {
            _animancer.transform.localEulerAngles = eulerAngles;
        }

        public void IdleAnimation()
        {
            PlayAnimation(_idleAnimName, _idleAnimSpeed);
        }

        public void RunAnimation()
        {
            PlayAnimation(_runAnimName, _runAnimSpeed);
        }

        public void JumpAnimation()
        {
            PlayAnimation(_jumpAnimName, _jumpAnimSpeed);
        }

        public void SlideAnimation()
        {
            PlayAnimation(_slideAnimName, _slideAnimSpeed);
        }
        public void SpeedAnimation()
        {
            PlayAnimation(_speedAnimName, _speedAnimSpeed);
        }

        public float GetHorizontalRatio()
        {
            return GameUtility.GetRatioFromValue(_localMover.localPosition.x, -_clampLocalX, _clampLocalX);
        }

        public Transform GetLocalMover()
        {
            return _localMover;
        }

        public Transform GetLocalMoverTarget()
        {
            return _localMoverTarget;
        }

        void MoveForward()
        {
            if (_enabled && _running && !_dodgingBack)
            {
                _distance += _forwardSpeed * Time.deltaTime;
            }
        }

        void FollowLocalMoverTarget()
        {
            if (!_canSwerve)
            {
                return;
            }
            Vector3 nextPos = new Vector3(_localMoverTarget.localPosition.x, _localMover.localPosition.y, _localMover.localPosition.z); ;
            _localMover.localPosition = Vector3.Lerp(_localMover.localPosition, nextPos, _strafeLerpSpeed * Time.deltaTime);
        }

        void UpdatePath()
        {
            if (_enabled)
            {
                transform.position = _pathCreator.path.GetPointAtDistance(_distance);
                transform.eulerAngles = _pathCreator.path.GetRotationAtDistance(_distance).eulerAngles + new Vector3(0f, 0f, 90f);
            }
        }

        public Transform GetAnimancerTransform()
        {
            return _animancer.transform;
        }

        public bool IsDodgingBack()
        {
            return _dodgingBack;
        }

        public void DodgeBack()
        {
            StartCoroutine(DodgeBackProcess());
        }

        public SimpleAnimancer GetAnimancer() {
            return _animancer;
        }

        public void SetAnimancer(SimpleAnimancer anim)
        {
            _animancer = anim;
        }

        public void SetRun(bool isRunning)
        {
            _running = isRunning;
        }
        IEnumerator DodgeBackProcess()
        {
            _canSwerve = false;
            _dodgingBack = true;
            _animancer.PlayAnimation("Dodging Back");
            transform.eulerAngles = Vector3.zero;
            yield return DOTween.To(() => _distance, x => _distance = x, _distance - _dodgeBackDistance, _dodgeBackDuration).WaitForCompletion() ;
            _animancer.PlayAnimation(_runAnimName);
            _dodgingBack = false;
            _canSwerve = true;
        }

        IEnumerator JumpProcess()
        {
            JumpAnimation();
            yield return new WaitForSeconds(0.7f);
            RunAnimation();
        }
        IEnumerator SlideProcess()
        {
            SlideAnimation();
            yield return new WaitForSeconds(0.6f);
            foreach (TrailRenderer trail in GetComponentsInChildren<TrailRenderer>())
                trail.emitting = false;
            foreach (ParticleSystem particle in GetComponentsInChildren<ParticleSystem>())
                particle.Pause();
            RunAnimation();
        }
        IEnumerator SpeedProcess()
        {
            _forwardSpeed = 20;
            _runAnimSpeed = 3.5f;
            RunAnimation();
            yield return new WaitForSeconds(0.6f);
            _forwardSpeed = 5;
            _runAnimSpeed = 1.5f;
            RunAnimation();
        }
    }
}