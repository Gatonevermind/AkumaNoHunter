using System.Collections;
using UnityEngine;

public class LoadingControl : MonoBehaviour
{
    public Transform rhino;
    public Transform fruit_1;
    public Transform fruit_2;
    public Transform fruit_3;
    public Transform fruit_4;
    public Transform fruit_5;

	public Transform final;

    public GameObject fruit1;
    public GameObject fruit2;
    public GameObject fruit3;
    public GameObject fruit4;
    public GameObject fruit5;

    public int fruitEated;

    //public AnimationClip run;
    //public AnimationClip eat;
    //public AnimationClip stay;

    public float counterEat;
    public float counterMove;

    public string levelName;
    private AsyncOperation async;

    private Animator animator;

	public float countTransition;
	public bool activePlayMode;

    private void Start()
    {
		activePlayMode = false;

		animator = GetComponent<Animator> ();
        fruitEated = 0;
        counterEat = 0;
        StartLoading();
        animator.SetBool("eating", false);
    }

    private void Update()
    {
        if (fruitEated == 0)
        {
            transform.position = Lerp(transform.position, fruit_1.position, 0.05f);

            if (Vector3.Distance(transform.position, fruit_1.position) < 1.5f)
            {
                counterEat += Time.deltaTime;
                animator.SetBool("eating", true);
				if (counterEat >= 0.25f)
				{
					animator.SetBool("eating", false);
					fruit1.SetActive(false);
				}
			
                if (counterEat >= 1.0f)
                {
                    fruitEated += 1;
                    counterEat = 0;
                }
            }
        }
        else if (fruitEated == 1)
        {
            transform.position = Lerp(transform.position, fruit_2.position, 0.05f);

			if (Vector3.Distance(transform.position, fruit_2.position) < 1.5f)
            {
                counterEat += Time.deltaTime;
                animator.SetBool("eating", true);
				if (counterEat >= 0.25f)
				{
					animator.SetBool("eating", false);
					fruit2.SetActive(false);
				}
                if (counterEat >= 1)
                {
                    fruitEated += 1;
                    counterEat = 0;
                }
            }
        }
        else if (fruitEated == 2)
        {
			transform.position = Lerp(transform.position, fruit_3.position, 0.05f);
			
			if (Vector3.Distance(transform.position, fruit_3.position) < 1.5f)
			{
				counterEat += Time.deltaTime;
				animator.SetBool("eating", true);
				if (counterEat >= 0.25f)
				{
					animator.SetBool("eating", false);
					fruit3.SetActive(false);
				}
				if (counterEat >= 1)
				{
					fruitEated += 1;
					counterEat = 0;
				}
			}
        }
        else if (fruitEated == 3)
        {
			transform.position = Lerp(transform.position, fruit_4.position, 0.05f);
			
			if (Vector3.Distance(transform.position, fruit_4.position) < 1.5f)
			{
				counterEat += Time.deltaTime;
				animator.SetBool("eating", true);
				if (counterEat >= 0.25f)
				{
					animator.SetBool("eating", false);
					fruit4.SetActive(false);
				}
				if (counterEat >= 1)
				{
					fruitEated += 1;
					counterEat = 0;
				}
			}
        }
		else if (fruitEated == 4)
		{
			transform.position = Lerp(transform.position, fruit_5.position, 0.05f);
			
			if (Vector3.Distance(transform.position, fruit_5.position) < 1.3f)
			{
				counterEat += Time.deltaTime;
				animator.SetBool("eating", true);
				if (counterEat >= 0.25f)
				{
					animator.SetBool("eating", false);
					fruit5.SetActive(false);
				}
				if (counterEat >= 1)
				{
					fruitEated += 1;
					counterEat = 0;
				}
			}
		}
		else if (fruitEated == 5)
		{
			transform.position = Lerp(transform.position, final.position, 0.02f);
			activePlayMode = true;

			if (Vector3.Distance(transform.position, final.position) < 1)
			{
				ActivateScene();
			}

			if (activePlayMode) {
				Debug.Log("Play Mode ACTIVATED");
				float fadeTime = GameObject.Find ("GameMaster").GetComponent<fader> ().BeginFade (1);
				//yield return new WaitForSeconds (fadeTime);
				countTransition -= Time.deltaTime;
			}
		}
    }

    public static Vector3 Lerp(Vector3 start, Vector3 finish, float percentage)
    {
        //Make sure percentage is in the range [0.0, 1.0]
        percentage = Mathf.Clamp01(percentage);

        //(finish-start) is the Vector3 drawn between 'start' and 'finish'
        Vector3 startToFinish = finish - start;

        //Multiply it by percentage and set its origin to 'start'
        return start + startToFinish * percentage;
    }

    public void StartLoading()
    {
        StartCoroutine("load");
    }

    private IEnumerator load()
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
            "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
        Debug.Log("Activate");
    }
}