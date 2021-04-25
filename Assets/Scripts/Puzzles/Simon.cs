using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Puzzles
{
    internal enum ButtonColor
    {
        BEGIN, // System Color
        RED,
        BLUE,
        YELLOW,
        GREEN
    }

    internal enum Word
    {
        YOURS,
        YOURE,
        LEAD0,
        LEED0
    }

    internal class WordSpec {
        public Word Name;
        public Dictionary<ButtonColor, ButtonColor> Mapping;
    }

    public class Simon : MonoBehaviour
    {
        [SerializeField] private TextAsset _configFile;
        // From the specification:
        // "Button at the first position is the start button"
        // "Other buttons go in order from left to right"
        [SerializeField] private List<Button3D> _buttons;
        [SerializeField] private TMP_Text _displayText;
        private AudioSource _audioSource;
        [Tooltip("Sounds")]
        [SerializeField] private AudioClip _buttonBlinkSound;
        [SerializeField] private AudioClip _puzzleSuccessSound;
        [SerializeField] private AudioClip _puzzleFailSound;

        private IList<WordSpec> _words;

        private WordSpec _serialSpec; 
        private bool _isPlaying;
        private bool _isDemonstrating;
        private int _numberOfStages;
        private bool _isSolved;

        private IList<ButtonColor> _buffer;
        private IList<ButtonColor> _inputBuffer;

        public UnityAction OnSolve;

        private void Start ()
        {
            _audioSource = GetComponent<AudioSource>();

            ResetState();
            SubscribeToPresses();
        }

        /// <summary>
        /// High Level method for playing
        /// internal sounds clips.
        /// </summary>
        private void PlayInternalSound (AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        /// <summary>
        /// Resets state of the component
        /// by clearing all buffers and
        /// defaulting all dynamic values.
        /// </summary>
        private void ResetState ()
        {
            _words = GetWordSpecs();
            _serialSpec = RandomizeSpec();

            _isPlaying = false;
            _isDemonstrating = false;
            _numberOfStages = UnityEngine.Random.Range(2, 6);
            _isSolved = false;

            _buffer = new List<ButtonColor>();
            _inputBuffer = new List<ButtonColor>();

            _displayText.text = _serialSpec.Name.ToString();
        }

        /// <summary>
        /// Subscribes to the passed interactable buttons.
        /// </summary>
        private void SubscribeToPresses ()
        {
            for (int ma = 0; ma < _buttons.Count; ++ma)
            {
                var button = _buttons[ma];
                var buttonColor = (ButtonColor) ma;

                button.OnInteract += () => StartCoroutine(HandleButtonPress(button, buttonColor));
            }
        }

        /// <returns>
        /// Returns a random spec from the list.
        /// </returns>
        private WordSpec RandomizeSpec ()
        {
            var specIndex = UnityEngine.Random.Range(0, _words.Count);
            return _words[specIndex];
        }

        /// <summary>
        /// Parses the configuration file and returns
        /// a list of word specs.
        /// </summary>
        /// <returns>
        /// List of the defined word specs.
        /// </returns>
        private IList<WordSpec> GetWordSpecs ()
        {
            string fileJson = _configFile.ToString();
            return JsonConvert.DeserializeObject<IList<WordSpec>>(fileJson);
        }

        /// <summary>
        /// Registers a button press.
        /// The function may fire game start.
        /// </summary>
        private IEnumerator HandleButtonPress (Button3D button, ButtonColor color)
        {
            if(_isDemonstrating || _isSolved) yield break;

            if (!_isPlaying)
            {
                if(ButtonColor.BEGIN == color)
                {
                    StartCoroutine(StartPuzzle());
                }

                yield break;
            }

            yield return StartCoroutine(button.Blink());
            yield return new WaitForSeconds(.25f);
            RegisterButtonPress(color);
        }

        /// <summary>
        /// Pushes pressed color event to the input buffer
        /// and validates if it's correct.
        /// </summary>
        /// <param name="color">
        /// Color of the button that needs to be registered.
        /// </param>
        private void RegisterButtonPress (ButtonColor color)
        {
            var currentStage = _inputBuffer.Count;
            var translatedColor = _serialSpec.Mapping[_buffer[currentStage]];

            // Failed
            if (color != translatedColor)
            {
                StartCoroutine(FailPuzzle());
                return;
            }

            _inputBuffer.Add(color);

            // Puzzle Solved
            if(currentStage == _numberOfStages)
            {
                SolvePuzzle();
                return;
            }

            PlayInternalSound(_buttonBlinkSound);

            // Next Step
            if (_inputBuffer.Count == _buffer.Count)
            {
                FireSequencer();
            }
        }

        /// <summary>
        /// Sets puzzle state to isPlaying
        /// and fires the sequencer.
        /// </summary>
        private IEnumerator StartPuzzle ()
        {
            PlayInternalSound(_buttonBlinkSound);
            StartCoroutine(_buttons[0].Blink());

            _isPlaying = true;

            yield return new WaitForSeconds(.75f);
            FireSequencer();
        }

        /// <summary>
        /// Resets puzzle with the failing sound.
        /// </summary>
        private IEnumerator FailPuzzle ()
        {
            PlayInternalSound(_puzzleFailSound);
            StartCoroutine(_buttons[0].Blink());

            _displayText.text = "_FAIL";
            yield return new WaitForSeconds(1.5f);
            ResetState();
        }

        /// <summary>
        /// Sets puzzle state to solved.
        /// Changes display text and locks the input.
        /// </summary>
        private void SolvePuzzle ()
        {
            _isSolved = true;
            OnSolve?.Invoke();

            PlayInternalSound(_puzzleSuccessSound);
            StartCoroutine(_buttons[0].Blink());

            _displayText.text = "_OK";
        }

        /// <summary>
        /// Resets the buffer and fires
        /// the logic algorithm.
        /// </summary>
        private void FireSequencer ()
        {
            ClearInputBuffer();
            GenerateStep();
            StartCoroutine(DemonstrateSteps());
        }

        /// <summary>
        /// Clears input buffer.
        /// </summary>
        private void ClearInputBuffer ()
        {
            _inputBuffer.Clear();
        }

        /// <summary>
        /// Asynchronously flashes all
        /// recorded buttons from the buffer.
        /// Blocks any user input.
        /// </summary>
        private IEnumerator DemonstrateSteps ()
        {
            _isDemonstrating = true;
            yield return new WaitForSeconds(.1f);

            foreach (ButtonColor color in _buffer)
            {
                Button3D button = _buttons[(int) color];

                PlayInternalSound(_buttonBlinkSound);
                yield return StartCoroutine(button.Blink());
            }

            _isDemonstrating = false;
        }

        /// <summary>
        /// Generates a new button color
        /// and pushes it into the buffer.
        /// </summary>
        private void GenerateStep ()
        {
            var colors = Enum.GetNames(typeof(ButtonColor));
            var colorName = colors[UnityEngine.Random.Range(1, colors.Length)];

            ButtonColor.TryParse(colorName, out ButtonColor color);
            _buffer.Add(color);
        }
    }
}
