using UnityEngine.Playables;

public class Subtitle2MixerBehaviour : PlayableBehaviour
{
    const string defaultText = "";

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var m_TrackBinding = playerData as TimelineLabel;

        if (m_TrackBinding == null)
            return;

        int inputCount = playable.GetInputCount();

        string message = defaultText;

        int start = 0;
        for (int i = start; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0)
            {
                var inputPlayable = (ScriptPlayable<Subtitle2Behaviour>)playable.GetInput(i);
                message = m_TrackBinding.message.texts[i];
                break;
            }
            else
            {
                continue;
            }
        }

        m_TrackBinding.label.text = message;
    }
}