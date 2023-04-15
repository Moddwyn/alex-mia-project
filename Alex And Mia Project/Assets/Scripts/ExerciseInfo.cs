using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu()]
public class ExerciseInfo : ScriptableObject
{
    public string exerciseName;
    public string trainingDesc;
    public string gameDisc;
    public VideoClip trainingVid;
    public VideoClip gameVid;
}