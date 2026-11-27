using UnityEngine;

public class Tap : MonoBehaviour
{
    public bool playing;
    public Animator anim;
    public ParticleSystem particles;

    public void ManageTap()
    {
        AkUnitySoundEngine.PostEvent("Play_AttackClink", this.gameObject);
        if (playing != true)
        {
            anim.SetTrigger("switch");
            particles.Play();
            AkUnitySoundEngine.SetSwitch("Tap", "On", this.gameObject);
            AkUnitySoundEngine.PostEvent("Play_TapContainer", this.gameObject);
            playing = true;
        }
        else if (playing == true)
        {
            anim.SetTrigger("switch");
            particles.Stop();
            AkUnitySoundEngine.SetSwitch("Tap", "Off", this.gameObject);
            playing = false;
        }
    }
}
