using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class BeforeStartClip : PlayableAsset, ITimelineClipAsset
{
    public BeforeStartBehaviour template = new BeforeStartBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BeforeStartBehaviour>.Create (graph, template);
        return playable;
    }
}
