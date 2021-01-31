using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections.Generic;
using TMPro;

[TrackColor(1f, 0.4287458f, 0.3915094f)]
[TrackClipType(typeof(BeforeStartClip))]
[TrackBindingType(typeof(TMP_Text))]
public class BeforeStartTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<BeforeStartMixerBehaviour>.Create (graph, inputCount);
    }

    // Please note this assumes only one component of type TMP_Text on the same gameobject.
    public override void GatherProperties (PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        TMP_Text trackBinding = director.GetGenericBinding(this) as TMP_Text;
        if (trackBinding == null)
            return;

        // These field names are procedurally generated estimations based on the associated property names.
        // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
        // In this case you will need to find the correct backing field name.
        // The suggested way of finding the field name is to:
        // 1. Make sure your scene is serialized to text.
        // 2. Search the text for the track binding component type.
        // 3. Look through the field names until you see one that looks correct.
        driver.AddFromName<TMP_Text>(trackBinding.gameObject, "m_Text");
#endif
        base.GatherProperties (director, driver);
    }
}
