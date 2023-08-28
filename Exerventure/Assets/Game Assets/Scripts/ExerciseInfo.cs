using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Exercise Info", menuName = "Exerventure/Exercise Info", order = 0)]
public class ExerciseInfo : ScriptableObject
{
    public string title;

    [InfoBox("The game summary containing lore and quick explanation of the exercise")]
    [ResizableTextArea]
    public string gameSummary;

    [InfoBox("Game instructions tells the player how to do the exercise and play the game")]
    [ResizableTextArea]
    public string gameInstructions;
    
    [HorizontalLine]
    public VideoClip animationVideo;
    public VideoClip gamePreviewVideo;

    [HorizontalLine]
    public string recordScoreHeader = "Highest Score";
    public string scoreSaveKey;
    public int sceneIndex;
}
