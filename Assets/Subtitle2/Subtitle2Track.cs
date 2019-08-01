using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackColor(0.8207547f, 0.367791f, 0.5062218f)]
[TrackClipType(typeof(Subtitle2Clip))]
[TrackBindingType(typeof(TimelineLabel))]
public class Subtitle2Track : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<Subtitle2MixerBehaviour>.Create(graph, inputCount);
    }

    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        var trackBinding = ((TimelineLabel)director.GetGenericBinding(this)).label.gameObject;
        if (trackBinding == null)
            return;

        driver.AddFromName<Text>(trackBinding.gameObject, "m_Text");
#endif
        base.GatherProperties(director, driver);
    }
}