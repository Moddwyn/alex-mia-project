using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu()]
public class ExerciseInfo : ScriptableObject
{
    public string exerciseName;
    public string gameName;
    public string gameDisc;
    public VideoClip gameVid;
}