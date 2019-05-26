using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class Vuforia_Target_Detected :  MonoBehaviour, ITrackableEventHandler
    {
    public UnityEvent OnDetected;
    public UnityEvent OnLost;

    private TrackableBehaviour mTrackableBehaviour;

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                                      TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                // Play audio when target is found
                OnDetected.Invoke();
            }
            else
            {
                // Stop audio when target is lost
                OnLost.Invoke();
            }
        }
    }
